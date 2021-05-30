using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    public class PaymentService : IPaymentService {
        public Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId) {
            throw new NotImplementedException();
        }
    }
}
