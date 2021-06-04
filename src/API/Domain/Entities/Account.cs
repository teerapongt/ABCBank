using System.ComponentModel.DataAnnotations;
using API.Domain.Entities.Common;

namespace API.Domain.Entities
{
    public class Account : AuditableEntity
    {
        public long Id { get; set; }
        public string IBAN { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}