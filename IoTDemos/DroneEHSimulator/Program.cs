using DroneEH.Common.Utility;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;


namespace DroneEH.Sender
{
class Program
{
  protected ManualResetEvent runCompleteEvent = new ManualResetEvent(false);
      protected CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
      private MyArgs a;
 
      public Program(MyArgs args)
       {
            a = args;
        }
        static void Main(string[] args)
       {
            MyArgs a = new MyArgs();
 
            try
            {
                a.ParseArgs(args);
           }
            catch (System.Exception e) 
            {
                Console.WriteLine(a.GetHelp());
                return;
            }
         
 
            Trace.TraceInformation(a.ToString());
 
            Program p = new Program(a);
                        
            var token = p.cancellationTokenSource.Token;
 
            Task.Factory.StartNew(() => p.Run(token, a));
            Task.Factory.StartNew(() => p.WaitForEnter());
 
            p.runCompleteEvent.WaitOne();            
           
       }
 
       protected void WaitForEnter()
        {
            Console.WriteLine("Press enter key to stop worker.");
            Console.ReadLine();
            cancellationTokenSource.Cancel();
        }
 
 
        private void Run(CancellationToken token, MyArgs a)
       {
 
            if (a.iterations == -1)
            {
                //Continuously iterate
                while (!token.IsCancellationRequested)
                {
                    SendMessages(a);
 
                    //Convert to milliseconds
                    Thread.Sleep(a.sleepSeconds* 1000);
               }
               runCompleteEvent.Set();
           }
           else
            {
                //Iterate a finite number of times, enabling the user
                //  to cancel the operation
                for (int i = 0; i<a.iterations; i++)
                {
                    if (!token.IsCancellationRequested)
                    {
                        SendMessages(a);
 
                        //Convert to milliseconds
                        Thread.Sleep(a.sleepSeconds* 1000);
                    }
                    else
                   {
                        break;
                    }
                }
                runCompleteEvent.Set();
            }                           
        }
 
        private static void SendMessages(MyArgs a)
        {
           var namespaceManager = EventHubManager.GetNamespaceManager();
            EventHubManager.CreateEventHubIfNotExists(a.eventHubName, a.numberOfPartitions, namespaceManager);
 
            Sender s = new Sender(a.eventHubName, a.numberOfDevices, a.numberOfMessages);
            Trace.TraceInformation("Sending events");
           s.SendEvents();
        }        
    }
}
