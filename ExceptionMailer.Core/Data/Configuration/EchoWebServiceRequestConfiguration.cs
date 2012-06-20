using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using FamineDto.Models;

namespace EchoWebServiceProcessor.Core.Data.Configuration
{
    public class EchoWebServiceRequestConfiguration : EntityTypeConfiguration<EchoWebServiceRequest>
    {
        public EchoWebServiceRequestConfiguration()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Ignore(x => x.IsDeleted);
            Property(x => x.Model).IsRequired().IsMaxLength();
            Property(x => x.StatusId).HasColumnName("Status").IsRequired();
            Property(x => x.EchoServiceCallId).HasColumnName("EchoServiceCall").IsRequired();
            Property(x => x.DateModified).IsOptional();
            Property(x => x.DateAdded).IsRequired();
            Property(x => x.FailureMessage).IsOptional().IsMaxLength();
            Property(x => x.Attempts).IsOptional();
            Property(x => x.TransactionId).IsRequired();

            ToTable("TxProcEchoWebServiceRequests");
        }
    }
}