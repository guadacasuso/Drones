using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading;

namespace SimulatedDrone
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "{HubUri}";
        static string deviceKey = "{DeviceKey}";

  
        static void Main(string[] args)
        {
            Console.WriteLine("Simulated Drone\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey));

            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessagesAsync()
        {
            double avgWindSpeed = 10; // m/s
            Random rand = new Random();

            while (true)
            {
                double currentWindSpeed = avgWindSpeed + rand.NextDouble() * 4 - 2;
                double currentAltitude =  rand.NextDouble() * 4 - 2;
                string GPSCurrent = "$GPAPB,A,A,0.10,R,N,V,V,011,M,DEST,011,M,011,M*3C";

                var telemetryDataPoint = new
                {
                    droneId = "DJI-Inspire 1" + deviceKey,
                    windSpeed = currentWindSpeed,
                    altitute = currentAltitude,
                    GPS = GPSCurrent
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending Telemetry : {1}", DateTime.Now, messageString);



                Thread.Sleep(3500);
            }
        }

    }
}
