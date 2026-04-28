using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IoTRobotWorldUDPServer.Robot;
using System.Timers;

namespace IoTRobotWorldUDPServer
{
    public partial class Form1 : Form
    {

        const int CMaxVisibleLogLines = 100;

        string UDPReceiveBuffer = "";

        string remoteAddress; // хост для отправки данных
        int remotePort; // порт для отправки данных
        int localPort; // локальный порт для прослушивания входящих подключений

        public delegate void ShowUDPMessage(string message);
        public ShowUDPMessage myDelegate;

        UdpClient udpClient; // = new UdpClient(11000);
        Thread thread;

        Robot robot;
        Production production;

        bool isStarted = false;
        int commandNumber = 0;
        int lastCommandNumber = 0;

        Color[] lights = { Color.Brown, Color.Red, Color.Yellow, Color.Green };
        int currentLightIndex = 0;
        System.Timers.Timer trafficLightTimer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Создадим делегата метода распечатки сообщения от удаленного сервера
            myDelegate = new ShowUDPMessage(ShowUDPMessageMethod);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopUDPClient();
        }

        private void PrintLog(string s)
        {
            // CMaxVisibleLogLines
            ReportListBox.Invoke((MethodInvoker)delegate {
                ReportListBox.Items.Add(s);
                while (ReportListBox.Items.Count > CMaxVisibleLogLines)
                {
                    ReportListBox.Items.RemoveAt(0);
                }
                ReportListBox.SelectedIndex = ReportListBox.Items.Count - 1;
                ReportListBox.SelectedIndex = -1;
            });
        }

        private void CheckStartStopUDPClient()
        {
            if (udpClient != null)
            {
                StartStopUDPClientButton.Text = "Отключиться";
                RemoteIPTextBox.Enabled = false;
                RemoteIPTextBox.BackColor = Color.LightGray;
                RemotePortTextBox.Enabled = false;
                RemotePortTextBox.BackColor = Color.LightGray;
                LocalIPTextBox.Enabled = false;
                LocalIPTextBox.BackColor = Color.LightGray;
                LocalPortTextBox.Enabled = false;
                LocalPortTextBox.BackColor = Color.LightGray;
                startButton.Enabled = true;
            }
            else
            {
                StartStopUDPClientButton.Text = "Подключиться";
                RemoteIPTextBox.Enabled = true;
                RemoteIPTextBox.BackColor = Color.White;
                RemotePortTextBox.Enabled = true;
                RemotePortTextBox.BackColor = Color.White;
                LocalIPTextBox.Enabled = true;
                LocalIPTextBox.BackColor = Color.White;
                LocalPortTextBox.Enabled = true;
                LocalPortTextBox.BackColor = Color.White;
                startButton.Enabled = false;
            }
        }

        private void StopUDPClient()
        {
            if ((thread != null) && (udpClient != null))
            { 
                thread.Abort();
                udpClient.Close();
                thread = null;
                udpClient = null;
            }
            PrintLog("UDPClient stopped");
            CheckStartStopUDPClient();
        }

        private void StartUDPClient()
        {
            if (thread != null)
            {
                thread.Abort();
            }
            if (udpClient != null)
            {
                udpClient.Close();
            }

            localPort = Int32.Parse(LocalPortTextBox.Text);
            try
            {
                udpClient = new UdpClient(localPort);
                thread = new Thread(new ThreadStart(ReceiveUDPMessage));
                thread.IsBackground = true;
                thread.Start();
                PrintLog("UDPClient started");
            }
            catch
            {
                PrintLog("UDPClient's start failed");
            }
            CheckStartStopUDPClient();
        }

        private void StartStopUDPClientButton_Click(object sender, EventArgs e)
        {
            if (udpClient == null)
            {
                StartUDPClient();
            }
            else
            {
                StopUDPClient();
            }
        }

        private void ShowUDPMessageMethod(string message)
        {
            //PrintLog("Remote >" + message);
            if (robot != null)
            {
                robot.UpdateStatus(message);
            }
        }

        private void ReceiveUDPMessage()
        {                      
            while (true)
            {
                try
                {

                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0); // port);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        string message = Encoding.ASCII.GetString(content);
                        this.Invoke(myDelegate, new object[] { message });
                    }
                }
                catch
                {
                    string errmessage = "RemoteHost lost";
                    this.Invoke(myDelegate, new object[] { errmessage });
                }
            }
        }

        private void SendUDPMessage(string s)
        {
            if (udpClient != null)
            {
                Int32 port = Int32.Parse(RemotePortTextBox.Text); 
                IPAddress ip = IPAddress.Parse(RemoteIPTextBox.Text.Trim());
                IPEndPoint ipEndPoint = new IPEndPoint(ip,port);
                byte[] content = Encoding.ASCII.GetBytes(s);
                try
                {
                    int count = udpClient.Send(content, content.Length, ipEndPoint);
                    if (count > 0)
                    {
                        PrintLog("Message has been sent.");
                    }
                }
                catch
                {
                    PrintLog("Error occurs.");
                }

            }
        }

        private void SendUDPMessageButton_Click(object sender, EventArgs e)
        {
            string s = UDPMessageTextBox.Text;
            if (AppendLFSymbolCheckBox.Checked) { s += "\n"; };
            SendUDPMessage(s);
        }

        private void RegularUDPSendCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RegularUDPSendCheckBox.Checked)
            {
                UDPRegularSenderTimer.Enabled = true;
            }
            else
            {
                UDPRegularSenderTimer.Enabled = false;
            }
        }

        private void UDPRegularSenderTimer_Tick(object sender, EventArgs e)
        {
            SendUDPMessage(UDPMessageTextBox.Text);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (isStarted)
                Stop();
            else
                StartTrafficLight();
        }

        private void Log(string message)
        {
            PrintLog(message);
        }

        private void StartTrafficLight()
        {
            trafficLightTimer = new System.Timers.Timer();
            trafficLightTimer.Interval = 100;
            trafficLightTimer.Elapsed += TrafficLightTimer_Elapsed;
            trafficLightTimer.Start();
            startButton.Enabled = false;
        }

        private void TrafficLightTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            trafficLight.Invoke((MethodInvoker)delegate
            {
                trafficLight.Refresh();
            });
            if (currentLightIndex < lights.Length-1)
            {
                currentLightIndex++;

            } else
            {
                startButton.Invoke((MethodInvoker)delegate
                {
                    startButton.Enabled = true;
                });
                Start();
                trafficLightTimer.Stop();
            }

        }

        private void Start()
        {
            startButton.Invoke((MethodInvoker)delegate
            {
                startButton.Text = "Стоп";
            });
            Int32 port = Int32.Parse(RemotePortTextBox.Text);
            IPAddress ip = IPAddress.Parse(RemoteIPTextBox.Text.Trim());
            IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
            robot = new Robot(udpClient, ipEndPoint);
            robot.SetLastCommandNumber(lastCommandNumber);
            robot.LogEventHandler += new CustomLogEventHandler(Log);
            robot.StatusEventHandler += new CustomStatusEventHandler(Robot_StatusEventHandler);
            robot.PutEventHandler += new CustomPutEventHandler(Robot_PutEventHandler);
            robot.StopEventHandler += new EventHandler(OnRobotStopped);
            robot.SetStatus(Robot.STATUS_START);
            production = new Production(robot);
            List<string[]> things = new List<string[]>();
            things.Add(textBoxCell1.Text.Split(','));
            things.Add(textBoxCell2.Text.Split(','));
            things.Add(textBoxCell3.Text.Split(','));
            production.SetThings(things);
            production.Run();
            textBoxPlatform2.Text = "";
            textBoxPlatform3.Text = "";       
            isStarted = true;
        }

        private void Robot_StatusEventHandler(string status)
        {
            textBoxStatus.Invoke((MethodInvoker)delegate { 
                textBoxStatus.Text = status;
            });
        }

        private void Robot_PutEventHandler(string thing)
        {
            if (thing == "К")
            {
                textBoxPlatform2.Invoke((MethodInvoker)delegate
                {
                    textBoxPlatform2.Text += " " + thing;
                });
            } else if (thing == "С")
            {
                textBoxPlatform3.Invoke((MethodInvoker)delegate
                {
                    textBoxPlatform3.Text += " " + thing;
                });
            }
        }

        private void OnRobotStopped(object s, EventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            lastCommandNumber = robot.commandNumber;
            production?.Stop();
            startButton.Invoke((MethodInvoker)delegate
            {
                startButton.Text = "Старт";
            });
            isStarted = false;
            currentLightIndex = 0;
            trafficLight.Invoke((MethodInvoker)delegate
            {
                trafficLight.Refresh();
            });
        }

        private void trafficLight_Paint(object sender, PaintEventArgs e)
        {
            trafficLightOn(lights[currentLightIndex], e.Graphics);           
        }

        private void trafficLightOn(Color color, Graphics graphics)
        {
            var r = trafficLight.Size.Height / 3;
            var d = r * 2;
            var half = trafficLight.Size.Height / 2;
            var delta = half - r;
            SolidBrush brush = new SolidBrush(color);
            Pen redPen = new Pen(Color.Red, 2f);
            Pen yellowPen = new Pen(Color.Yellow, 2f);
            Pen greenPen = new Pen(Color.Green, 2f);
            if (color == Color.Red) {
                graphics.FillEllipse(brush, delta, delta, d, d);
                graphics.DrawEllipse(yellowPen, half * 3 - r, delta, d, d);
                graphics.DrawEllipse(greenPen, half * 5 - r, delta, d, d);
            }
            else if (color == Color.Yellow)
            {
                graphics.DrawEllipse(redPen, delta, delta, d, d);
                graphics.FillEllipse(brush, half * 3 - r, delta, d, d);
                graphics.DrawEllipse(greenPen, half * 5 - r, delta, d, d);
            }
            else if (color == Color.Green)
            {
                graphics.DrawEllipse(redPen, delta, delta, d, d);
                graphics.DrawEllipse(yellowPen, half * 3 - r, delta, d, d);
                graphics.FillEllipse(brush, half * 5 - r, delta, d, d);
            }
            else
            {
                graphics.DrawEllipse(redPen, delta, delta, d, d);
                graphics.DrawEllipse(yellowPen, half * 3 - r, delta, d, d);
                graphics.DrawEllipse(greenPen, half * 5 - r, delta, d, d);
            }
                   
        }
    }
}
