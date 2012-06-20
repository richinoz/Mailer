using System.Collections.Generic;
using System.Threading;
using ExceptionMailer.Infrastructure;

namespace ExceptionMailer.Core.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IList<IPlugIn> _allPlugIns;

        public CommandProcessor()
        {
            _allPlugIns = StructureMap.ObjectFactory.GetAllInstances<IPlugIn>();
        }

        public void Execute()
        {
            foreach (var plugIn in _allPlugIns)
            {
                var threadDelegate = new ThreadStart(plugIn.Execute);
                var newThread = new Thread(threadDelegate);
                newThread.Start();
            }
        }
    }
}