using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _1_ManageProgramFlow_ConsoleApp1
{
   class Interlock_Example
   {
      public static void DoTest()
      {
         int n = 0;
         int result = -100;
         result = Interlocked.Increment(ref n);
         result = Interlocked.Decrement(ref n);
         result = Interlocked.Add(ref n, 1001);
         result = Interlocked.Exchange(ref n, 200);
         result = Interlocked.CompareExchange(ref n, 333, 200);
         result = Interlocked.CompareExchange(ref n, 444, 200);
         result = Interlocked.CompareExchange(ref n, 555, 200);

         n = 1;
         Interlocked.CompareExchange(ref n, 2, 1);
         
      }
   }
}
