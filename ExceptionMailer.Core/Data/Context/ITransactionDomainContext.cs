using System.Linq;
using FamineDto.Models;

namespace ExceptionMailer.Core.Data.Context
{
    public interface ITransactionDomainContext
    {
        IQueryable<EchoWebServiceRequest> EchoWebServiceRequests { get; }
    }
}