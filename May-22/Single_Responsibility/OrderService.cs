using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLIDdemo.Single_Responsibility
{
    public class OrderService
    {
        private readonly OrderValidator _validator = new();
        private readonly OrderRepository _repository = new();
        private readonly EmailService _emailService = new();

        public void PlaceOrder(Order order)
        {
            _validator.Validate(order);
            _repository.SaveOrder(order);
            _emailService.SendEmailConfirmation(order.CustomerEmail);
        }
        

    }
}
