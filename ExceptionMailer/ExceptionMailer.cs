using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using ExceptionMailer.Core.Commands;
using ExceptionMailer.Core.Dependency;
using StructureMap;


namespace ExceptionMailer
{
    public partial class ExceptionMailer : ServiceBase
    {
        public ExceptionMailer()
        {
            InitializeComponent();
        }

        public void Start(string[] args)
        {
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            ObjectFactory.Configure(x => x.Scan(y =>
            {
                y.AssemblyContainingType<CoreRegistry>();
                y.LookForRegistries();
            }));

            var commandProcessor = ObjectFactory.GetInstance<ICommandProcessor>();

            commandProcessor.Execute();

        }

        protected override void OnStop()
        {
        }
    }
}
