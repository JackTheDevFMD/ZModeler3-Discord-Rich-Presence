using System;
using System.Diagnostics;

namespace DiscordRPC.ZModeler3
{
    class Program
    {
        private static int discordPipe = -1;
        private static string applicationName = "ZModeler3";
        private static bool running = false;
        private static DiscordRpcClient client = new DiscordRpcClient("1070070014666866728", pipe: discordPipe);

        //Main Loop
        static void Main(string[] args)
        {


            //Reads the arguments for the pipe
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-pipe":
                        discordPipe = int.Parse(args[++i]);
                        break;

                    default: break;
                }
            }

            var previousProcesses = Process.GetProcessesByName(applicationName);
            string updatedName = string.Empty;

            while (true)
            {
                var currentProcesses = Process.GetProcessesByName(applicationName);

                if (currentProcesses.Length > 0 && !running)
                {
                    ReadyTaskExample();
                    running = true;
                }
                else if (currentProcesses.Length == 0 && running)
                {
                    ReadyTaskExample();
                    running = false;
                }

                previousProcesses = currentProcesses;
                System.Threading.Thread.Sleep(1000);
            }
        }

        static void ReadyTaskExample()
        {
            if (!running)
            {
                try
                {
                    client.Initialize();

                }
                catch
                {
                    client = new DiscordRpcClient("1070070014666866728", pipe: discordPipe);
                    client.Initialize();
                }

                client.SetPresence(new RichPresence()
                {
                    Details = "Editing a Zmodeler 3 file",
                    State = "Being productive on a project.",
                    Timestamps = new Timestamps()
                    {
                        Start = DateTime.UtcNow
                    },
                    Buttons = new Button[]
                        {
                            new Button() { Label = "Free Mode Designs", Url = "https://discord.gg/fmd" },
                            new Button() { Label = "Free Mode Designs Website", Url = "https://freemodedesigns.shop" }
                        },
                    Assets = new Assets()
                    {
                        LargeImageKey = "zm3_logo_x64_",
                        LargeImageText = "https://www.zmodeler3.com",
                    }
                });

            }
            else
            {
                client.Dispose();
            }
        }
    }
}