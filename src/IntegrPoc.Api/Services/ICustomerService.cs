using IntegrPoc.Api.Models;
using System;

namespace IntegrPoc.Api.Services
{
    public interface ICustomerService
    {
        CustomerViewModel Get(Guid id);
        void Register(CustomerViewModel model);
        void Update(CustomerViewModel model);
        void Remove(Guid id);
    }
}
