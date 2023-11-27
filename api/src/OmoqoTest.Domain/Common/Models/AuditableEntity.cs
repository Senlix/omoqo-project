
using System.ComponentModel.DataAnnotations.Schema;
using ErrorOr;

namespace OmoqoTest.Domain.Common.Models
{
    public abstract class AuditableEntity<TId> : Entity<TId>
        where TId : notnull
    {
        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }
        
        [NotMapped]
        public List<Error> ErrorsList { get; set; } = [];
        [NotMapped]
        public bool IsValid => ErrorsList.Count == 0;
    }
}

