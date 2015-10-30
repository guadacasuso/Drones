
using System;
using System.Text;

namespace DroneEH.Sender
{ 
  public class MyArgs
{
   public MyArgs()
        { }

 public string eventHubName { get; set; }
 
public int numberOfDevices { get; set; }

       public int numberOfMessages { get; set; }

 public int numberOfPartitions { get; set; }        

       public int sleepSeconds { get; set; }

        public int iterations { get; set; }

       public void ParseArgs(string[] args)
        {
            if (args.Length != 6)
           {
                throw new ArgumentException("Incorrect number of arguments. Expected 6 args <eventhubname> <NumberOfDevices> <NumberOfMessagesToSend> <NumberOfPartitions> <sleepSeconds> <iterations>", args.ToString());
            }
            else
            {
               eventHubName = args[0];
               numberOfDevices = Int32.Parse(args[1]);
               numberOfMessages = Int32.Parse(args[2]);
               numberOfPartitions = Int32.Parse(args[3]);
               sleepSeconds = int.Parse(args[4]);
               iterations = int.Parse(args[5]);
           }
        }

        internal string GetHelp()
       {
            StringBuilder sb = new StringBuilder();
           sb.AppendLine("Usage: Sender.exe EventHubName NumberOfDevices NumberOfMessagesToSend NumberOfPartitions SleepSeconds Iterations");
           sb.AppendLine();
           sb.AppendLine("Parameters:");
            sb.AppendLine("\tEventHubName:\t\tName of the Event Hub to send messages to");
            sb.AppendLine("\tNumberOfDevices:\t\tNumber of devices to simulate");
           sb.AppendLine("\tNumberOfMessagesToSend:\t\tNumber of messages to send");
            sb.AppendLine("\tNumberOfPartitions:\t\tNumber of Event Hub partitions");
            sb.AppendLine("\tSleepSeconds:\t\tNumber of seconds to sleep between iterations (0 to send as fast as possible)");
            sb.AppendLine("\tIterations:\t\tNumber of iterations (-1 to continuously send)");
            sb.AppendLine();
 
           return sb.ToString();
        }
 
        public override string ToString()
        {
           StringBuilder sb = new StringBuilder();
            sb.AppendLine("Event Hub name: " + eventHubName);
            sb.AppendLine("Number of devices: " + numberOfDevices);
            sb.AppendLine("Number of messages: " + numberOfMessages);
            sb.AppendLine("Number of partitions: " + numberOfPartitions);
            sb.AppendLine("Seconds to sleep between iterations: " + sleepSeconds);
            sb.AppendLine("Number of iterations: " + iterations);
 
            return sb.ToString();
        }
    }
}
