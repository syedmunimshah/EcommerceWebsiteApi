﻿using EcommerceWebsiteDbConnection;
using EcommerceWebsiteServies;
using EcommerceWebsiteServies.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWebsite.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
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

            await _customerService.RegisterCustomer(customerDTO);
            return Ok("Customer added successfully");
        }


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
    }
}
