using System;
using System.Collections.Generic;

namespace PizzaStore.Data
{
    public partial class PizzaPie
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public bool? Onion { get; set; }
        public bool? Pepper { get; set; }
        public bool? Pineapple { get; set; }
        public bool? Ham { get; set; }
        public bool? Chicken { get; set; }
        public bool? Sausage { get; set; }
        public bool? Bbqchicken { get; set; }
        public bool? Pepperoni { get; set; }

        public Orders Order { get; set; }
    }
}
