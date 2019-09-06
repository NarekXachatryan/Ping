using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace ProjectForPing
{
    class ClassPing
    {
        [Obsolete]
        public static void Ping()
        {
            string ip = ConfigurationSettings.AppSettings.Get("Ip");
            string filePath = ConfigurationSettings.AppSettings.Get("LogFilePath");
            Ping p1 = new Ping();
            PingOptions pingOptions = new PingOptions();

            PingReply PR;

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (StreamWriter strWr = new StreamWriter(fs))
                {
                    while (true)
                    {
                        fs.Position = fs.Length + 1;
                        Thread.Sleep(1000);
                        PR = p1.Send(ip);
                        if (PR.Status == IPStatus.Success)
                        {

                            strWr.WriteLine($"{DateTime.Now}  Ip:{ip} - Ok");

                        }
                        else
                        {
                            strWr.WriteLine($"{DateTime.Now}  Ip:{ip} - Failed");
                        }
                        Console.WriteLine($"{DateTime.Now} - {PR.Status}");
                    }
                }

            }
        }
    }
}
