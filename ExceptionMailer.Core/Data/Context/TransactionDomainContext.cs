using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EchoWebServiceProcessor.Core.Data.Configuration;
using FamineDto.Models;
using System.Data;

namespace ExceptionMailer.Core.Data.Context
{
    /// <summary>
    /// Context for Data Access across the project. 
    /// </summary>
    public class TransactionDomainContext : DbContext, ITransactionDomainContext
    {
        public TransactionDomainContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }
        //Root aggregates
        /// <summary>
        /// Use IDomainContext.Transactions to access object from upper layers
        /// </summary>
        public virtual IDbSet<EchoWebServiceRequest> EchoWebServiceRequests { get; set; }

        IQueryable<EchoWebServiceRequest> ITransactionDomainContext.EchoWebServiceRequests
        {
            get { return EchoWebServiceRequests.AsQueryable(); }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new EchoWebServiceRequestConfiguration());
        }
    }
}