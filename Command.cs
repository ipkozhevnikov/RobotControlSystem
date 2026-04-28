using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IoTRobotWorldUDPServer
{
    internal class Command
    {
        public int CommandNumber = 1;
        public int Mode = 0;
        public int Force = 0;
        public int Balance = 0;
        public int T = 0;

        public Command(int CommandNumber, int Force, int Balance)
        {
            this.CommandNumber = CommandNumber;
            this.Force = Force;
            this.Balance = Balance;
        }

        public string getJson()
        {
            return JsonSerializer.Serialize(new {
                N = CommandNumber,
                M = Mode,
                F = Force,
                B = Balance,
                T = T
            });
        }
    }
}
