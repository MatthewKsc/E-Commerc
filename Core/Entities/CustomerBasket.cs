using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BaskeItem> Items { get; set; } = new List<BaskeItem>();
        public int? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }

        public CustomerBasket() {

        }

        public CustomerBasket(string id) {
            Id = id;
        }
    }
}
