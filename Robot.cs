using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IoTRobotWorldUDPServer
{
    internal class RobotRemoteStatus
    {
        public string n { get; set; }
        public string s { get; set; }
        public string c { get; set; }
        public string le { get; set; }
        public string re { get; set; }
        public string az { get; set; }
        public string b { get; set; }
        public string d0 { get; set; }
        public string d1 { get; set; }
        public string d2 { get; set; }
        public string d3 { get; set; }
        public string d4 { get; set; }
        public string d5 { get; set; }
        public string d6 { get; set; }
        public string d7 { get; set; }
    }

    internal class Robot
    {
        public const int STATUS_START = 1;
        public const int STATUS_AREA2 = 2;
        public const int STATUS_AREA2_PLATFORM1 = 4;
        public const int STATUS_AREA2_TAKE_THING = 5;
        public const int STATUS_AREA2_PLATFORM2 = 6;
        public const int STATUS_AREA2_PUT_BLUE_THING = 7;
        public const int STATUS_AREA2_PLATFORM3 = 8;
        public const int STATUS_AREA2_PUT_RED_THING = 9;
        public const int STATUS_FINISH = 99;
        public const int STATUS_FINISHED = 100;
        public Dictionary<int, string> STATUS_NAMES = new Dictionary<int, string>() { 
            { STATUS_START, "На старте" },
            { STATUS_AREA2, "Переместился в область 2" },
            { STATUS_AREA2_PLATFORM1, "Переместился к платформе 1" },
            { STATUS_AREA2_TAKE_THING, "Взял деталь" },
            { STATUS_AREA2_PLATFORM2, "Переместился к платформе 2" },
            { STATUS_AREA2_PLATFORM3, "Переместился к платформе 3" },
            { STATUS_AREA2_PUT_BLUE_THING, "Положил синюю деталь" },
            { STATUS_AREA2_PUT_RED_THING, "Положил красную деталь" },
            { STATUS_FINISH, "Финиширую" },
            { STATUS_FINISHED, "На финише" },
        };
           
        UdpClient udpClient;
        IPEndPoint ipEndPoint;
        public int commandNumber {get; set;}

        public delegate void CustomLogEventHandler(string message);
        public event CustomLogEventHandler LogEventHandler;

        public delegate void CustomPutEventHandler(string message);
        public event CustomPutEventHandler PutEventHandler;

        public delegate void CustomStatusEventHandler(string message);
        public event CustomStatusEventHandler StatusEventHandler;

        public event EventHandler StopEventHandler;

        public RobotRemoteStatus RemoteStatus;

        public bool keepHorizontally = false;
        private int d2 = 0;
        private int d6 = 0;
        private int currentSpeed = 0;
        private int prevSpeed = 0;
        private int currentBalance = 0;
        private int prevBalance = 0;
        private System.Timers.Timer correctionTimer;
        private bool canCorrect = false;
        private int status;
        private double upMinValue = Int32.MaxValue;
        private bool upCalc = false;
        private bool upMove = false;
        private int upTarget = 90;
        string thing = "";
        int azTarget;
        bool azRotateStarted = false;
        int currentCellNumber = 0;
        bool cellNumberNeedZero = false;

        public void SetThing(string thing)
        {
            this.thing = thing;
            OnLog("Взял деталь " + thing);
        }

        public string GetThing()
        {
            return thing;
        }

        public void PutThing()
        {
            OnLog("Положил деталь " + thing);
            OnPut(thing);
            thing = null;            
        }

        public int GetStatus()
        {
            return status;
        }
        public void SetStatus(int status)
        {
            this.status = status;
            OnLog("Новый статус " + STATUS_NAMES[status]);
            OnStatus(STATUS_NAMES[status]);
        }

        public void OnStop()
        {
            EventHandler handler = StopEventHandler;
            if (null != handler) handler(this, EventArgs.Empty);
        }
        public void OnLog(string message)
        {
            LogEventHandler?.Invoke(message);
        }
        public void OnStatus(string status)
        {
            StatusEventHandler?.Invoke(status);
        }
        public void OnPut(string thing)
        {
            PutEventHandler?.Invoke(thing);
        }

        public Robot(UdpClient udpClient, IPEndPoint ipEndPoint)
        {
            this.udpClient = udpClient;
            this.ipEndPoint = ipEndPoint;
            correctionTimer = new System.Timers.Timer();
            correctionTimer.Interval = 1000;
            correctionTimer.Elapsed += CorrectionTimer_Elapsed;
            correctionTimer.Start();
        }

        private void CorrectionTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            canCorrect = true;
        }

        public void Stop()
        {
            currentSpeed = 0;
            keepHorizontally = false;
            SendCommand(0, 0);
        }

        public void Forward(int Force)
        {
            currentSpeed = Force;
            keepHorizontally = false;
            SendCommand(Math.Abs(Force), 0);
        }

        public void ForwardH(int Force)
        {
            //currentSpeed = Force;
            keepHorizontally = true;
            d2 = 0; d6 = 0;
            SendCommand(Math.Abs(Force), 0);
        }

        public void Back(int Force)
        {
            //currentSpeed = -Force;
            keepHorizontally = false;
            SendCommand(-Math.Abs(Force), 0);
        }

        public void BackH(int Force)
        {
            //currentSpeed = -Force;
            keepHorizontally = true;
            d2 = 0; d6 = 0;
            SendCommand(-Math.Abs(Force), 0);
        }

        public void Right(int Balance)
        {
            //currentSpeed = 0;
            keepHorizontally = false;
            SendCommand(0, -Math.Abs(Balance));
        }

        public bool RotateAz(int param)
        {
            var az = Int32.Parse(RemoteStatus.az);
            if (!azRotateStarted)
            {
                azTarget = az - param;
                if (azTarget < 0)
                {
                    azTarget = azTarget + 360;
                }
                if (azTarget >= 360)
                {
                    azTarget = azTarget - 360;
                }
                azRotateStarted = true;
            }
            if (azRotateStarted)
            {
                if (azTarget == az)
                {
                    azRotateStarted = false;
                    return true;
                }
                else
                {
                    var dir = 0;
                    if (param != 0)
                        dir = param / Math.Abs(param);
                    var balance = 0;
                    if (dir < 0)
                    {
                        balance = azTarget > az ? azTarget - az : (360 - az + azTarget);
                    } else
                    {
                        balance = azTarget > az ? (360 - azTarget + az) : az - azTarget;
                    }
                    balance = - dir * (Math.Abs(balance) / 2 + 1);
                    //currentSpeed = 0;
                    keepHorizontally = false;
                    SendCommand(0, balance);
                }
            }
            return false;
        }

        public void Left(int Balance)
        {
            //currentSpeed = 0;
            keepHorizontally = false;
            SendCommand(0, Math.Abs(Balance));
        }

        public bool Centralize()
        {
            if (RemoteStatus == null)
                return false;
            var d0 = Int32.Parse(RemoteStatus.d0);
            var d4 = Int32.Parse(RemoteStatus.d4);
            var d2 = Int32.Parse(RemoteStatus.d2);
            var d6 = Int32.Parse(RemoteStatus.d6);
            if (Math.Abs(d0 - d4) < 20)
            {
                return true;
            }
            var d0d4 = d0 - d4;
            var force = 0;
            if (d0d4 != 0)
            {
                var dir = d0d4 / Math.Abs(d0d4);
                force = Math.Min(100, d0d4 + 20 * dir);
            } else
            {
                force = 0;
            }
            //currentSpeed = force;
            keepHorizontally = true;
            SendCommand(force, 0);
            return false;
        }

        public bool Up()
        {
            if (RemoteStatus == null)
                return false;
            var balance = 0;
            var az = Int32.Parse(RemoteStatus.az);
            var targetAz = 80;
            if (!upCalc)
            {
                if (upMove)
                {
                    if (az == upTarget)
                    {
                        return true;
                    }
                    targetAz = upTarget;
                    balance = -Math.Min(Math.Abs(az - targetAz) + 1, 100);
                }
                else
                {
                    if (az != targetAz)
                    {
                        balance = -Math.Min(Math.Abs(az - targetAz) + 1, 100);
                    }
                    else
                    {
                        upCalc = true;
                    }
                }
            } else {
                balance = -3;
                var d0 = Int32.Parse(RemoteStatus.d0);
                var d4 = Int32.Parse(RemoteStatus.d4);
                var d2 = Int32.Parse(RemoteStatus.d2);
                var d6 = Int32.Parse(RemoteStatus.d6);
                double value = (double)(d0 + d4) / (double)(d2 + d6);
                if (value < upMinValue)
                {
                    upMinValue = value;
                    upTarget = az;
                    OnLog("upTarget: " + az);
                }
                if (az < 10)
                {
                    upCalc = false;
                    upMove = true;
                }
            }
            
            //currentSpeed = 0;
            keepHorizontally = false;
            SendCommand(0, balance);
            return false;
        }

        public bool Reach(int param)
        {
            if (RemoteStatus == null)
                return false;
            var d0 = Int32.Parse(RemoteStatus.d0);
            if (d0 == param)
            {
                return true;
            }
            var delta = d0 - param;
            var force = 0;
            var dir = delta / Math.Abs(delta);
            force = Math.Min(100, delta + 20 * dir);
            if (Math.Abs(force) > 30)
            {
                force = 100 * dir;
            }
            //currentSpeed = force;
            keepHorizontally = true;
            SendCommand(force, currentBalance);
            return false;
        }

        public bool ReachCell(int param, int cellNumber)
        {
            if (RemoteStatus == null)
                return false;
            var c = Int32.Parse(RemoteStatus.c);
            var b = Int32.Parse(RemoteStatus.b);
            if (c == param)
            {
                if (currentCellNumber < cellNumber)
                {
                    cellNumberNeedZero = true;
                }
                else
                {
                    OnLog("Достиг ячейки");
                    return true;
                }
            } else if (cellNumberNeedZero)
            {
                cellNumberNeedZero = false;
                currentCellNumber++;
            }
            var speed = 0;
            if (b == 1)
            {
                speed = 0;
                OnLog("Столкновение");
            }
            else
            {
                speed = 50;
            }
            keepHorizontally = true;
            SendCommand(speed, currentBalance);
            return false;
        }

        public void ResetCalcParams()
        {
            upMinValue = Int32.MaxValue;
            upCalc = false;
            upMove = false;
            upTarget = 90;
            azRotateStarted = false;
            currentCellNumber = 0;
            cellNumberNeedZero = false;
        }
        public void UpdateStatus(string status)
        {
            try
            {
                RemoteStatus = JsonSerializer.Deserialize<RobotRemoteStatus>(status);
                if (keepHorizontally && canCorrect)
                {
                    int current_d2 = Int32.Parse(RemoteStatus.d2);
                    int current_d6 = Int32.Parse(RemoteStatus.d6);
                    if (d2 > 0 || d6 > 0)
                    {
                        var delta = 3;
                        if (current_d2 > d2 && current_d6 < d6)
                        {
                            SendCommand(currentSpeed, currentSpeed > 0 ? -delta : delta);
                        }
                        else if (current_d2 < d2 && current_d6 > d6)
                        {
                            SendCommand(currentSpeed, currentSpeed > 0 ? delta : -delta);
                        }
                        else if (current_d2 == d2 && current_d6 == d6)
                        {
                            SendCommand(currentSpeed, 0);
                        }
                        canCorrect = false;
                    }
                    d2 = current_d2;
                    d6 = current_d6;
                }
            }
            catch (System.Text.Json.JsonException e)
            {
            }
        }

        private void SendCommand(int speed, int balance)
        {
            if (prevBalance != currentBalance || prevSpeed != currentSpeed)
            {
                string command = new Command(++commandNumber, speed, balance).getJson();
                SendUDPMessage(command);
                prevBalance = currentBalance;
                prevSpeed = currentSpeed;
            }
            currentBalance = balance;
            currentSpeed = speed;
        }

        private void SendUDPMessage(string s)
        {
            if (udpClient != null)
            {
                byte[] content = Encoding.ASCII.GetBytes(s + "\n");
                try
                {
                    int count = udpClient.Send(content, content.Length, ipEndPoint);
                    if (count > 0)
                    {
                       //OnLog("Message has been sent. " + s);
                    }
                }
                catch
                {
                    OnLog("Error occurs.");
                }

            }
        }

        internal void SetLastCommandNumber(int lastCommandNumber)
        {
            commandNumber = lastCommandNumber;
        }
    }
}
