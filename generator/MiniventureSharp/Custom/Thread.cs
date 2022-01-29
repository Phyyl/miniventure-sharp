using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public class Thread
    {
        private CSThread thread;
        public Action run;

        public Thread(Runnable runnable)
        {
            run = runnable.run;
        }

        public Thread()
        {
        }

        public void start()
        {
            run();
            //thread = new(new ThreadStart(run)) { IsBackground = true };
            //thread.Start();
        }

        public void stop()
        {
//#pragma warning disable SYSLIB0006 // Type or member is obsolete
//            thread?.Abort();
//#pragma warning restore SYSLIB0006 // Type or member is obsolete
//            thread = null;
        }

        public static void sleep(int n)
        {
            CSThread.Sleep(n);
        }
    }
}
