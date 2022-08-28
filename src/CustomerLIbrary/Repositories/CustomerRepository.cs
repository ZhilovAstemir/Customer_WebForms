﻿using CustomerLIbrary.Entities;
using CustomerLIbrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CustomerLIbrary.Repositories
{
    public class CustomerRepository : BaseRepository, IRepository<Customer>
    {
        public void Create(Customer entity)
        {
            using (var connection = GetConnection()) 
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO [CustomerLib_Bekov].[dbo].[Customer] (FirstName,LastName,PhoneNumber,Email,TotalPurchasesAmount) VALUES (@FirstName,@LastName,@PhoneNumber,@Email,@TotalPurchasesAmount)", connection);

                var firstNameParam = new SqlParameter("@FirstName", SqlDbType.Char, 50) { Value = entity.FirstName };

                var lastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = entity.LastName };

                var phoneNumberParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 12) { Value = entity.PhoneNumber };

                var emailParam = new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = entity.Email };

                var totalPurchasesAmountParam = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money) { Value = entity.TotalPurchasesAmount };

                command.Parameters.Add(firstNameParam);
                command.Parameters.Add(lastNameParam);
                command.Parameters.Add(phoneNumberParam);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(totalPurchasesAmountParam);

                command.ExecuteNonQuery();
            }
                
        }

        

        public Customer Read(string entityCode)
        {
            using (var connection = GetConnection()) {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [CustomerLib_Bekov].[dbo].[Customer] WHERE CustomerId = @CustomerId", connection);

                var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int) { Value = entityCode };

                command.Parameters.Add(customerIdParam);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerId = Convert.ToInt32(reader["CustomerId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Email = reader["Email"].ToString(),
                            TotalPurchasesAmount = Convert.ToDecimal(reader["TotalPurchasesAmount"])
                        };
                    }
                    return null;
                }
            }
        }

        public void Update(Customer entity)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = new SqlCommand("UPDATE [CustomerLib_Bekov].[dbo].[Customer] SET FirstName = @FirstName, LastName = @LastName, PhoneNumber = @PhoneNumber, Email = @Email, TotalPurchasesAmount = @TotalPurchasesAmount  WHERE CustomerId = @CustomerId", connection);

                var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int) { Value = entity.CustomerId };

                var firstNameParam = new SqlParameter("@FirstName", SqlDbType.Char, 50) { Value = entity.FirstName };

                var lastNameParam = new SqlParameter("@LastName", SqlDbType.NVarChar, 50) { Value = entity.LastName };

                var phoneNumberParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 12) { Value = entity.PhoneNumber };

                var emailParam = new SqlParameter("@Email", SqlDbType.NVarChar, 255) { Value = entity.Email };

                var totalPurchasesAmountParam = new SqlParameter("@TotalPurchasesAmount", SqlDbType.Money) { Value = entity.TotalPurchasesAmount };

                command.Parameters.Add(customerIdParam);
                command.Parameters.Add(firstNameParam);
                command.Parameters.Add(lastNameParam);
                command.Parameters.Add(phoneNumberParam);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(totalPurchasesAmountParam);

                command.ExecuteNonQuery();
            }

        }

        public void Delete(string entityCode)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand("DELETE [CustomerLib_Bekov].[dbo].[Customer] WHERE CustomerId = @CustomerId", connection);

                var customerIdParam = new SqlParameter("@CustomerId", SqlDbType.Int) { Value = entityCode };

                command.Parameters.Add(customerIdParam);

                command.ExecuteNonQuery();
            }
        }

        public List<Customer> GetAll(string customerId)
        {
            var customers = new List<Customer>();
            using (var connection = GetConnection())
            {
                connection.Open();

                var command = new SqlCommand("SELECT * FROM [CustomerLib_Bekov].[dbo].[Customer]", connection);               

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = Convert.ToInt32(reader["CustomerId"]),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Email = reader["Email"].ToString(),
                            TotalPurchasesAmount = Convert.ToDecimal(reader["TotalPurchasesAmount"])
                        });
                    }                   
                }
            }
            return customers;
        }
    }
}
