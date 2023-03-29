﻿using AdventureWorksApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AdventureWorksApi.Functions
{
    public class CustomerFunctions
    {
               
        public static IResult CreateCustomer(AdventureWorksLt2019Context context, Customer customer)
        {
            if (context.Addresses.Any(a => a.Rowguid == customer.Rowguid))
            {
                customer.Rowguid = Guid.NewGuid();
            }


            context.Customers.Add(customer);
            context.SaveChanges();

            return Results.Created($"/Customers", customer);
        }

        public static IResult ReadCustomer(AdventureWorksLt2019Context context, int? id)
        {
         
     
            if (id == null)
            {
                return Results.Ok(context.Customers.ToList());
            }

            Customer? customer = context.Customers.FirstOrDefault(a => a.CustomerId == id);

            if (customer == null)
            {
                return Results.BadRequest();
            }else
            {
                return Results.Ok(customer);
            }

        }

        public static IResult UpdateCustomer(AdventureWorksLt2019Context context, int id, Customer updateCustomer)
        {

            Customer customer = context.Customers.Find(id);
            if (customer == null)
            {
                customer = new Customer();

                context.Customers.Add(customer);
                context.SaveChanges();
                return Results.Ok("Customer is added successful");
            }
            else
            {
                customer.Title= updateCustomer.Title;
                customer.FirstName= updateCustomer.FirstName;
                customer.MiddleName = updateCustomer.LastName;
                customer.CompanyName = updateCustomer.CompanyName;
                customer.SalesPerson = updateCustomer.SalesPerson;
                customer.EmailAddress = updateCustomer.EmailAddress;
                customer.Phone = updateCustomer.Phone;
                customer.Rowguid = updateCustomer.Rowguid;

                return Results.Ok("Customer updated successfully");
            }


            /*if (id != customer.CustomerId)
            {
                return Results.BadRequest();
            }

            context.Entry(customer).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(context, id))
                {
                    return Results.NotFound();
                }
                else
                {
                    throw;
                }
            }

           return Results.NoContent();*/
        }
            
        public static IResult DeleteCustomer(AdventureWorksLt2019Context context, int id)
        {
            var customer = context.Customers.Find(id);

            if (customer == null)
            {
                return Results.NotFound();
            }

            context.Customers.Remove(customer);
            context.SaveChanges();

            return Results.Ok(customer);
        }

        private static bool CustomerExists(AdventureWorksLt2019Context context, int id)
        {
            return context.Customers.Any(e => e.CustomerId == id);
        }


    }
}
