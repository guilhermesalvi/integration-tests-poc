using IntegrPoc.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrPoc.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<CustomerViewModel> _customers;

        public CustomerService()
        {
            _customers = new List<CustomerViewModel>
            {
                new CustomerViewModel { Id = new Guid("3fff9268-b022-4e4d-b1b5-f462c8240b25"), Name = "First Customer" },
                new CustomerViewModel { Id = new Guid("36fc7dcb-3f74-4234-afbd-37aff0e4f4b8"), Name = "Second Customer" },
                new CustomerViewModel { Id = new Guid("e19aaf1d-af56-4d3a-a1a4-262a5979f104"), Name = "Third Customer" }
            };
        }

        public CustomerViewModel Get(Guid id)
        {
            return _customers.FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Register(CustomerViewModel model)
        {
            _customers.Add(model);
        }

        public void Remove(Guid id)
        {
            var toRemove = _customers.FirstOrDefault(x => x.Id == id);
            _customers.Remove(toRemove);
        }

        public void Update(CustomerViewModel model)
        {
            Remove(model.Id);
            _customers.Add(model);
        }
    }
}
