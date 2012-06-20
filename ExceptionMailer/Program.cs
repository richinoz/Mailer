using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ExceptionMailer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            var server = new ExceptionMailer();
            server.Start(null);

            System.Threading.Thread.Sleep(100 * 60 * 1000);
            return;
#endif
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new ExceptionMailer() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
