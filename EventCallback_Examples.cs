using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_ManageProgramFlow_ConsoleApp1
{
   class EventCallback_Examples
   {
      public static void DoTest()
      {
         // 
         Pub p = new Pub();
         try
         {
            p.OnChange += new Observor_Log("1").RespondChange;
            p.OnChange += new Observor_Log("2").RespondChange;
            p.OnChange += new Observor_Log("3").RespondChange;

            //p.OnChange += (sender, e) => Console.WriteLine("1: event raised:{0}", e.Value);
            //p.OnChange += (sender, e) => { throw new Exception("2: event exception!!!"); };
            //p.OnChange += (sender, e) => Console.WriteLine("3: event raised:{0}", e.Value);

            p.Raise(1);
            p.Raise(2);
            p.Raise(0);
            p.Raise(10);
            p.Raise(-100);
         }
         catch (AggregateException ex)
         {
            Console.WriteLine("exceptions caught:{0}", ex.InnerExceptions.Count);
         }
      }
   }

   public class MyArgs : EventArgs
   {
      public MyArgs(int value)
      {
         Value = value;
      }

      public int Value { get; set; }
   }

   public class Pub
   {
      private event EventHandler<MyArgs> onChange = delegate { };

      public event EventHandler<MyArgs> OnChange
      {
         add { lock (onChange) { onChange += value; } }
         remove { lock (onChange) { onChange -= value; } }
      }

      public void Raise(int value)
      {
         //onChange(this, new MyArgs(value));
         var exceptions = new List<Exception>();
         foreach (Delegate handler in onChange.GetInvocationList())
         {
            try
            {
               handler.DynamicInvoke(this, new MyArgs(value));
            }
            catch (Exception ex)
            {
               exceptions.Add(ex);
            }
         }

         if (exceptions.Any())
         {
            throw new AggregateException(exceptions);
         }
      }
   }

   public class Observor_Log
   {
      public Observor_Log(string name)
      {
         _name = name;
      }

      private string _name = string.Empty;
      public string Name
      {
         get { return _name; }
         set { _name = value; }
      }

      public void RespondChange(object sender, MyArgs data)
      {
         switch (data.Value)
         {
            case 0:
               throw new Exception(string.Format("{0}: event exception!!!", Name));
            default:
               Console.WriteLine("{0}: event raised:{1}", Name, data.Value);
               break;
         }
      }
   }
}
