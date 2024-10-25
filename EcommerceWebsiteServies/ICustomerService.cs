using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebsiteServies
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<string> RegisterCustomer(CustomerDTO customerDTO);
        Task<string> LoginCustomer(LoginCustomerDTO LoginCustomerDTO);
        Task<string> UpdateCustomer(CustomerDTO customerDTO);
        Task<string> DeleteCustomer(int id);

    }
}
