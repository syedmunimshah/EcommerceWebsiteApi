using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly TestController testController;
        public CustomerController(ICustomerService customerService, TestController testController)
        {
            _customerService = customerService;
            this.testController = testController;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> GetAllCustomer() {
        
            return await _customerService.GetAllCustomer();
            
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerService.RegisterCustomer(customerDTO);


            //Step 1: 
            //ISI PROJECT KE ANDAR TUM EK NEW VIEW cONTROLLER BANALO 
            //Step 2:
            //IS API KO TUM US CONTROLLER MAI CALL KRWAO

            //testController.Index(customer);

            return Ok(customer);
        }

        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> LoginCustomer(LoginCustomerDTO LoginCustomerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await _customerService.LoginCustomer(LoginCustomerDTO);
                return Ok(new {Token=token});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _customerService.UpdateCustomer(customerDTO);
                if (result.Contains("not found"))
                {
                    return NotFound(result);  
                }

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
               var result = await _customerService.DeleteCustomer(id);
                if (result.Contains("not found")) 
                {
                    return NotFound(result);
                }
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> TokenSaveDatabase(string tokenValue, int UserId) {

            try
            {
               var Token = await _customerService.TokenSaveDatabase(tokenValue,UserId);
                return Ok(Token);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
