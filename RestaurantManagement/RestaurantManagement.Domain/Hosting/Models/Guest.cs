using RestaurantManagement.Domain.Common;
using System;

namespace RestaurantManagement.Domain.Hosting.Models
{
    public class Guest:ValueObject
    {
        internal Guest(string firstName, string lastName, string phoneNumber) 
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