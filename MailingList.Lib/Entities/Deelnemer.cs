using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailingList.Lib.Entities
{
    public class Deelnemer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string Answer { get; set; }
    

    public Deelnemer() { }

    public Deelnemer(int _id, string _firstName, string _lastName, string _email, int _phone,
        string _street, int _streetNumber, string _city, int _postalCode, string _answer)
    {
        Id = _id;
        FirstName = _firstName;
        LastName = _lastName;
        Email = _email;
        Phone = _phone;
        Street = _street;
        StreetNumber = _streetNumber;
        City = _city;
        PostalCode = _postalCode;
        Answer = _answer;

    }

    public Deelnemer(int _id, string _firstName, string _lastName, string _email, int _phone,
        string _street, int _streetNumber, string _city, int _postalCode)
        {
            Id = _id;
            FirstName = _firstName;
            LastName = _lastName;
            Email = _email;
            Phone = _phone;
            Street = _street;
            StreetNumber = _streetNumber;
            City = _city;
            PostalCode = _postalCode;

        }

        public override string ToString()
        {
            string info = $"{FirstName} {LastName} {City}";
            return info;
        }
    }
}
