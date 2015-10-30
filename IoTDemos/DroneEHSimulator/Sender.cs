using DroneEH.Common.Contracts;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
 
 
namespace DroneEH.Sender
{
    public class Sender
    {        
        private string eventHubName;
        private int numberOfCopters;
        private int numberOfMessages;
 
 
        public Sender(string eventHubName, int numberOfCopters, int numberOfMessages)
        {
            this.eventHubName = eventHubName;
            this.numberOfCopters = numberOfCopters;
            this.numberOfMessages = numberOfMessages;            
        }
 
        public void SendEvents()
        {
            // Create EventHubClient
            EventHubClient client = EventHubClient.Create(this.eventHubName);
 
            try
            {
                List<Task> tasks = new List<Task>();
                
                // Send messages to Event Hub
                Trace.TraceInformation("Sending messages to Event Hub " + client.Path);
 
                Random random = new Random();
                for (int i = 0; i< this.numberOfMessages; ++i)
                {
                    // Create the device/temperature metric
                    MetricEvent info = new MetricEvent() { DroneId = random.Next(numberOfCopters),
                        Altitude = random.Next(100)
                        /*GPSSignal = true,
                        Airspeed = random.Next(200),
                        Battery= random.Next(100),
                        Distance= 1200,
                        Voltaje= random.Next(100),
                        Frecuency= random.NextDouble() */
                    };
                    var serializedString = JsonConvert.SerializeObject(info);
 
                    EventData data = new EventData(UTF8Encoding.UTF8.GetBytes(serializedString));
 
                   // Set user properties if needed
                    data.Properties.Add("Type", "Telemetry_" + DateTime.Now.ToLongTimeString());
                    OutputMessageInfo("SENDING: ", data, info);
 
                    // Send the metric to Event Hub
                    tasks.Add(client.SendAsync(data));
                }
               ;

                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception exp)
            {
               Trace.TraceError("Error on send: " + exp.Message);
            }
 
           client.CloseAsync().Wait();
        }

        static void OutputMessageInfo(string action, EventData data, MetricEvent info)
       {
           if (data == null)
            {
               return;
           }
            if (info != null)
           {
               Trace.TraceInformation("{0}: Drone {1}, Altitude {2}.", 
                   action, info.DroneId, info.Altitude);
            }
        }
   }
}

