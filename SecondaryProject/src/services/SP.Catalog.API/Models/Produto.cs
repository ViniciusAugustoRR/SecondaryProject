using SP.Core.DomainObjects;

namespace SP.Catalog.API.Models
{
    public class Produto : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Value{ get; set; }
        public DateTime DateRegistration { get; set; }
        public string Image { get; set; }
        public int AmmountStorage { get; set; }
    }

}
