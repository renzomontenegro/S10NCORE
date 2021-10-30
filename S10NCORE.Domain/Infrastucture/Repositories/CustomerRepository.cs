﻿using Microsoft.EntityFrameworkCore;
using S10NCORE.Domain.Core.Entities;
using S10NCORE.Domain.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10NCORE.Domain.Infrastucture.Repositories
{
    class CustomerRepository
    {
        private readonly SalesContext _context;

        public CustomerRepository(SalesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            //using var data = new SalesContext();
            return await _context.Customer.ToListAsync();
        }
        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customer.Where(x => x.Id == id).FirstOrDefaultAsync();

        }

        public async Task Insert(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(Customer customer)
        {
            var customerNow = await _context.Customer.FindAsync(customer.Id);
            customerNow.FirstName = customer.FirstName;
            customerNow.LastName = customer.LastName;
            customerNow.Country = customer.Country;
            customerNow.City = customer.City;
            customerNow.Phone = customer.Phone;

            int countRows = await _context.SaveChangesAsync();
            return (countRows > 0);

        } 

        public async Task<bool> Delete(int id)
        {
            var customerNow = await _context.Customer.FindAsync();
            if (customerNow == null)
                return false;
            _context.Customer.Remove(customerNow);
            int countRows = await _context.SaveChangesAsync();
            return (countRows > 0);
        }

    }
}
