using System.ComponentModel.DataAnnotations;

namespace Bakery.Inventory.DomainApi
{
    public class BaseEntity<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}
