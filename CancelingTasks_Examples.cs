using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_ManageProgramFlow_ConsoleApp1
{
   class CancelingTasks_Examples
   {
      public static void DoTest()
      {
         CancellationTokenSource cancelSource = new CancellationTokenSource();
         CancellationToken token = cancelSource.Token;

         token.Register(() =>
            {
               Console.WriteLine();
               Console.WriteLine("Cancel event is called...");
               Console.WriteLine();
            });

         Task task = Task.Run(() =>
            {
               while (!token.IsCancellationRequested)
               {
                  Console.Write("*");
                  Thread.Sleep(500);
               }
            }, token);

         Console.WriteLine("Press enter to stop the task");
         Console.ReadLine();
         cancelSource.Cancel();

         Console.WriteLine("Press enter to end the application");
         Console.ReadLine();
      }
   }
}
