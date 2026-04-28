using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IoTRobotWorldUDPServer
{
    internal class Production
    {
        public Robot robot;
        Timer stepsTimer;
        List<(string command, int duration, int param)> 
            stepsFromArea1ToArea2, 
            stepsToPlatform1, stepsToPlatform1FromPut, stepsToPlatform2, stepsToPlatform3,
            stepsToTakeThing, 
            stepsToPutBlueThing, stepsToPutRedThing,
            stepsToFinishFromTake, stepsToFinishFromPut;

        int currentCommand = 0;
        int currentStepsCommand = 0;
        List<(string command, int duration, int param)> currentSteps;
        int WaitingForStatus = 0;
        List<string[]> Things;
        int RedThingsCount = 0;
        int BlueThingsCount = 0;
        int repeatition = 0;
        string prevCommand;

        public Production(Robot robot)
        {
            this.robot = robot;
            stepsTimer = new Timer();
            stepsFromArea1ToArea2 = new List<(string command, int duration, int param)>
            {
                ("Stop", 500, 0),
                ("Forward", 10000, 0),
                ("RotateAz", 500, 90),
                ("Reach", 500, 800),
                ("RotateAz", 500, -90),
                ("Stop", 500, 0),
            };
            stepsToPlatform1 = new List<(string command, int duration, int param)>
            {
                ("Reach", 500, 100),
                ("Stop", 500, 0),
            };
            stepsToTakeThing = new List<(string command, int duration, int param)>
            {
                ("RotateAz", 500, 90),
                ("Forward", 2000, 0),
                ("ReachCell", 500, 1),
                ("RotateAz", 500, -90),
                ("Reach", 500, 27),
                ("Take", 500, 0),
                ("Stop", 2000, 0),
            };
            stepsToPlatform1FromPut = new List<(string command, int duration, int param)>
            {
                ("Reach", 1000, 28),
                ("BackH", 10000, 0),
                ("RotateAz", 500, -90),
                ("Reach", 500, 800),
                ("RotateAz", 500, -90),
                ("Reach", 500, 100),
            };
            stepsToPlatform2 = new List<(string command, int duration, int param)>
            {
                ("Reach", 1000, 28),
                ("BackH", 10000, 0),
                ("RotateAz", 500, 90),
                ("Reach", 500, 680),
                ("RotateAz", 500, 90),
                ("Reach", 500, 100),
                ("Stop", 500, 0),                
            };
            stepsToPlatform3 = new List<(string command, int duration, int param)>
            {
                ("Reach", 1000, 28),
                ("BackH", 10000, 0),
                ("RotateAz", 500, 90),
                ("Reach", 500, 680),
                ("RotateAz", 500, 90),
                ("Reach", 500, 100),
                ("Stop", 500, 0),
            };
            stepsToPutRedThing = new List<(string command, int duration, int param)>
            {
                ("RotateAz", 500, 90),
                ("Forward", 2000, 0),
                ("ReachCell", 500, 2),
                ("RotateAz", 500, -90),
                ("Reach", 500, 27),
                ("Put", 500, 0),
                ("Stop", 2000, 0),
            };
            stepsToPutBlueThing = new List<(string command, int duration, int param)>
            {
                ("RotateAz", 500, -90),
                ("Forward", 2000, 0),
                ("ReachCell", 500, 3),
                ("RotateAz", 500, 90),
                ("Reach", 500, 27),
                ("Put", 500, 0),
                ("Stop", 2000, 0),
            };
            stepsToFinishFromTake = new List<(string command, int duration, int param)>
            {
                ("Reach", 1000, 28),
                ("BackH", 10000, 0),
                ("RotateAz", 500, 90),
                ("Reach", 500, 27),
                ("RotateAz", 500, -90),
                ("Reach", 500, 27),
                ("Stop", 500, 0),
            };

            stepsToFinishFromPut = new List<(string command, int duration, int param)>
            {
                ("BackH", 10000, 0),
                ("RotateAz", 500, -90),
                ("Reach", 500, 27),
                ("RotateAz", 500, -90),
                ("Reach", 500, 27),
                ("Stop", 500, 0),
            };
        }

        public void SetThings(List<string[]> things)
        {
            Things = things;
        }

        public void Run()
        {
            int status = CheckStatus(robot.GetStatus());
            RunSteps(WhatsNext(status));
        }

        protected void RunSteps(List<(string command, int duratio, int paramn)> steps)
        {
            repeatition = 0;
            currentStepsCommand = 0;
            currentSteps = steps;
            stepsTimer.Interval = 100;
            stepsTimer.Elapsed += StepsTimer_Elapsed;
            stepsTimer.Start();
        }

        public void Stop()
        {
            stepsTimer.Stop();
            robot.Stop();
            robot.OnLog("Robot stopped");
        }

        private void StepsTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            stepsTimer.Stop();
            bool nextCommand = false;
            bool repeatSteps = false;
            if (currentStepsCommand < currentSteps.Count)
            {
                var (command, interval, param) = currentSteps[currentStepsCommand];
                if (prevCommand != command)
                {
                    robot.OnLog(command + " " + param);
                }
                prevCommand = command;
                switch (command)
                {
                    case "Forward":
                        robot.Forward(100);
                        nextCommand = true;
                        break;
                    case "ForwardH":
                        robot.ForwardH(100);
                        nextCommand = true;
                        break;
                    case "Back":
                        robot.Back(100);
                        nextCommand = true;
                        break;
                    case "BackH":
                        robot.BackH(100);
                        nextCommand = true;
                        break;
                    case "Left":
                        robot.Left(25);
                        nextCommand = true;
                        break;
                    case "Right":
                        robot.Right(25);
                        nextCommand = true;
                        break;
                    case "RotateAz":
                        nextCommand = robot.RotateAz(param);
                        break;
                    case "Stop":
                        robot.Stop();
                        nextCommand = true;
                        break;
                    case "Centralize":
                        nextCommand = robot.Centralize();
                        break;
                    case "Up":
                        nextCommand = robot.Up();
                        break;
                    case "Reach":
                        nextCommand = robot.Reach(param);
                        break;
                    case "ReachCell":
                        nextCommand = robot.ReachCell(param, GetReachCellNumber(param));
                        break;
                    case "Take":
                        if (!Take(repeatition))
                        {
                            repeatition++;
                            repeatSteps = repeatition < Things.Count;
                        }
                        nextCommand = true;
                        break;
                    case "Put":
                        if (robot.GetThing() == "К")
                        {
                            /*repeatSteps = (RedThingsCount > repeatition);
                            if (repeatSteps) 
                                repeatition++;*/
                            RedThingsCount++;
                        } else if (robot.GetThing() == "С")
                        {
                            /*repeatSteps = (BlueThingsCount > repeatition);
                            if (repeatSteps)
                                repeatition++;*/
                            BlueThingsCount++;
                        }
                        robot.PutThing();
                        nextCommand = true;
                        break;
                }
                if (nextCommand)
                {
                    robot.ResetCalcParams();
                    currentCommand++;
                    currentStepsCommand++;
                }

                if (repeatSteps)
                {
                    currentStepsCommand = 0;
                    stepsTimer.Interval = interval;
                    stepsTimer.Start();
                }
                else
                {

                    if (currentStepsCommand >= currentSteps.Count)
                    {
                        if (!repeatSteps)
                        {
                            robot.SetStatus(WaitingForStatus);
                            if (WaitingForStatus == Robot.STATUS_FINISHED)
                            {
                                robot.OnStop();
                            }
                            else
                            {
                                int status = CheckStatus(robot.GetStatus());
                                repeatition = 0;
                                currentStepsCommand = 0;
                                currentSteps = WhatsNext(status);
                                stepsTimer.Interval = interval;
                                stepsTimer.Start();
                            }
                        }
                    }
                    else
                    {
                        stepsTimer.Interval = interval;
                        stepsTimer.Start();
                    }
                }
            }
        }

        private int GetReachCellNumber(int platformType)
        {
            switch (platformType)
            {
                case 1: return 0;
                case 2: return RedThingsCount;
                case 3: return BlueThingsCount;
                default: return 0;
            }
        }

        private bool Take(int cell)
        {
            if (Things.ElementAt(cell).Length > 0)
            {
                var thing = Things.ElementAt(cell)[0];
                if ((RedThingsCount < 3 && thing == "К") || (BlueThingsCount < 3 && thing == "С"))
                {
                    robot.SetThing(thing);
                    var newArray = Things.ElementAt(cell).Skip(1).ToArray();
                    Things.RemoveAt(cell);
                    Things.Insert(cell, newArray);
                    return true;
                }
            }
            return false;
        }

        private List<(string command, int duration, int param)> WhatsNext(int status)
        {
            switch (status)
            {
                case Robot.STATUS_START:
                    WaitingForStatus = Robot.STATUS_AREA2;
                    return stepsFromArea1ToArea2;

                case Robot.STATUS_AREA2:
                    WaitingForStatus = Robot.STATUS_AREA2_PLATFORM1;
                    return stepsToPlatform1;

                case Robot.STATUS_AREA2_PLATFORM1:
                    WaitingForStatus = Robot.STATUS_AREA2_TAKE_THING;
                    return stepsToTakeThing;

                case Robot.STATUS_AREA2_TAKE_THING:
                    if (robot.GetThing() == "К")
                    {
                        WaitingForStatus = Robot.STATUS_AREA2_PLATFORM2;
                        return stepsToPlatform2;
                    } else if (robot.GetThing() == "С")
                    {
                        WaitingForStatus = Robot.STATUS_AREA2_PLATFORM3;
                        return stepsToPlatform3;
                    } else
                    {
                        WaitingForStatus = Robot.STATUS_FINISH;
                        return stepsToFinishFromTake;
                    }

                case Robot.STATUS_AREA2_PLATFORM2:
                    WaitingForStatus = Robot.STATUS_AREA2_PUT_RED_THING;
                    return stepsToPutRedThing;

                case Robot.STATUS_AREA2_PLATFORM3:
                    WaitingForStatus = Robot.STATUS_AREA2_PUT_BLUE_THING;
                    return stepsToPutBlueThing;

                case Robot.STATUS_AREA2_PUT_BLUE_THING:
                    if (BlueThingsCount == 3 && RedThingsCount == 3)
                    {
                        WaitingForStatus = Robot.STATUS_FINISH;
                        return stepsToFinishFromPut;
                    }
                    else
                    {
                        WaitingForStatus = Robot.STATUS_AREA2_PLATFORM1;
                        return stepsToPlatform1FromPut;
                    }

                case Robot.STATUS_AREA2_PUT_RED_THING:
                    if (BlueThingsCount == 3 && RedThingsCount == 3)
                    {
                        WaitingForStatus = Robot.STATUS_FINISH;
                        return stepsToFinishFromPut;
                    }
                    else
                    {
                        WaitingForStatus = Robot.STATUS_AREA2_PLATFORM1;
                        return stepsToPlatform1FromPut;
                    }

                case Robot.STATUS_FINISH:
                    WaitingForStatus = Robot.STATUS_FINISHED;
                    robot.SetStatus(Robot.STATUS_FINISHED);
                    return new List<(string command, int duration, int param)> { };

                default:
                    return new List<(string command, int duration, int param)> { };
            }
        }

        private int CheckStatus(int status)
        {
            //todo
            return status;
            /*
            switch (status)
            {
                case Robot.STATUS_START:
                    break;
                default:
            }*/
        }
    }
}
