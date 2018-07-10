using System;
using System.Collections.Generic;

namespace PizzaStore.Data
{
    public partial class Orders
    {
        public Orders()
        {
            PizzaPie = new HashSet<PizzaPie>();
        }

        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int StoreNumber { get; set; }
        public int TotalPizzas { get; set; }
        public decimal? Price { get; set; }
        public DateTime? TimeOfOrder { get; set; }

        public Locations StoreNumberNavigation { get; set; }
        public Users User { get; set; }
        public ICollection<PizzaPie> PizzaPie { get; set; }
    }
}
