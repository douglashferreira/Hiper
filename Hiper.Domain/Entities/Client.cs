using Hiper.Domain.Enums;
using Hiper.Domain.ValueObjects;
using Hiper.Shared.Entities;

namespace Hiper.Domain.Entities
{
    public class Client : Entity
    {
        protected Client() { }
        public Client(Name name, Document document, Email email, Address address, DateTime birthday, EGender gender, int? id = null)
        {
            /*if (id == null)
            {
                Random rnd = new Random();
                id = rnd.Next(int.MaxValue);
            }*/
            Id = id ?? 0;
            Gender = gender;
            Birthday = birthday;
            Name = name;
            Document = document;
            Email = email;
            Address = address;
            Birthday = birthday;
        }

        public EGender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
    }
}