using System.Configuration;
using System.Reflection;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Smtp;
using ExceptionMailer.Core.Commands;
using ExceptionMailer.Core.Data.Context;
using ExceptionMailer.Infrastructure;
using StructureMap.Configuration.DSL;

namespace ExceptionMailer.Core.Dependency
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            var path = System.AppDomain.CurrentDomain.BaseDirectory;

            Scan(scanner =>
                     {
                         scanner.TheCallingAssembly();
                         scanner.ExcludeType<ITransactionDomainContext>();
                         scanner.ExcludeType<IEmailSender>();
                         scanner.AssembliesFromPath(path);
                         scanner.AddAllTypesOf<IPlugIn>();
                         scanner.LookForRegistries();
                         scanner.WithDefaultConventions();
                     });

            For<ITransactionDomainContext>().HybridHttpOrThreadLocalScoped().Use(() => new TransactionDomainContext("FamineWebsite"));
            For<IEmailSender>().Use(() => new DefaultSmtpSender(ConfigurationManager.AppSettings["SmtpServer"]));

            //For<IApplicationConfiguration>().Use(
            //    () =>
            //    new DictionaryAdapterFactory().GetAdapter<IApplicationConfiguration>(ConfigurationManager.AppSettings));
        }
    }
}