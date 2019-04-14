using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StateMachineSample.Lib;

namespace StateMachineSample.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Messenger.OnMessageReceived += (message) =>
            {
                Console.WriteLine(message);
            };

            var model = new AirConditioner();

            var stm = new ModelStateMachine(model);

            var exit = false;

            while(exit == false)
            {
                stm.Update();

                Print(model);

                Console.Write(">");

                var command = Console.ReadLine();

                switch (command)
                {
                    case "start":
                        stm.SendTrigger(SwitchStartTrigger.Instance);
                        break;
                    case "stop":
                        stm.SendTrigger(SwitchStopTrigger.Instance);
                        break;
                    case "cool":
                        stm.SendTrigger(SwitchCoolTrigger.Instance);
                        break;
                    case "heat":
                        stm.SendTrigger(SwitchHeatTrigger.Instance);
                        break;
                    case "dry":
                        stm.SendTrigger(SwitchDryTrigger.Instance);
                        break;
                    case "clean":
                        stm.SendTrigger(SwitchCleanTrigger.Instance);
                        break;
                    case "up":
                        model.Up();
                        break;
                    case "down":
                        model.Down();
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        break;
                }

            }
        }

        static void Print(AirConditioner model)
        {
            Console.Write($"Target Temp : {model.TargetTemperature}[deg] | ");
            Console.Write($"Temp : {model.Temperature}[deg] | ");
            Console.Write($"Humidity : {model.Humidity}[%]");
            Console.WriteLine();
        }
    }
}
