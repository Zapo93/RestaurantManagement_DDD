using RestaurantManagement.Common.Domain.Models;
using System;

namespace RestaurantManagement.Hosting.Domain.Models
{
    public class Guest:ValueObject
    {
        public Guest(string firstName, string lastName, string phoneNumber) 
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            //TODO Make class phonenumber
            this.PhoneNumber = phoneNumber;
        }

        private Guest() { }

        public string FirstName { get;}
        public string LastName { get;}
        public string PhoneNumber { get;}
    }
}