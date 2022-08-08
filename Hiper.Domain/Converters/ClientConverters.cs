using Hiper.Domain.DTO.Client;
using Hiper.Domain.Entities;
using Hiper.Domain.Enums;

namespace Hiper.Domain.Converters
{
    public static class ClientConverters
    {
        public static Client ConvertDtoToModel(ClientDTO clientDto)
        {
            if (clientDto != null)
                return new Client(new Domain.ValueObjects.Name(clientDto.FirstName, clientDto.LastName),
                new ValueObjects.Document(clientDto.Document, EDocumentType.CPF),
                new ValueObjects.Email(clientDto.Email),
                new ValueObjects.Address(clientDto.Street, clientDto.Number, clientDto.Neighborhood, clientDto.City, clientDto.State, clientDto.Country, clientDto.ZipCode),
                clientDto.Birthday, (EGender)clientDto.Gender, (clientDto.Id > 0 ? clientDto.Id : null));
            else
                return null;
        }

        public static ClientDTO ConvertModelToDto(Client client)
        {
            if (client != null)
                return new ClientDTO
                {
                    Id = client.Id,
                    FirstName = client.Name.FirstName,
                    LastName = client.Name.LastName,
                    Document = client.Document.Number,
                    Email = client.Email.Address,
                    Street = client.Address.Street,
                    Number = client.Address.Number,
                    Neighborhood = client.Address.Neighborhood,
                    City = client.Address.City,
                    State = client.Address.State,
                    Country = client.Address.Country,
                    ZipCode = client.Address.ZipCode,
                    Birthday = client.Birthday,
                    Gender = (byte)client.Gender
                };
            else
                return null;
        }
    }
}
