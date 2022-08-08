using CsvHelper.Configuration.Attributes;

namespace Hiper.Domain.DTO.Client
{
    public class ClientDTO
    {
        [Ignore]
        public int Id { get; set; }

        [Name("Birthday")]
        public DateTime Birthday { get; set; }

        [Name("Gender")]
        public byte Gender { get; set; }

        [Name("FirstName")]
        public string FirstName { get; set; }

        [Name("LastName")]
        public string LastName { get; set; }

        [Name("Document")]
        public string Document { get; set; }


        [Name("Email")]
        public string Email { get; set; }

        [Name("Street")]
        public string Street { get; set; }

        [Name("Number")]
        public string Number { get; set; }

        [Name("Neighborhood")]
        public string Neighborhood { get; set; }

        [Name("City")]
        public string City { get; set; }

        [Name("State")]
        public string State { get; set; }

        [Name("Country")]
        public string Country { get; set; }

        [Name("ZipCode")]
        public string ZipCode { get; set; }


    }
}

