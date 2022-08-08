using Hiper.Shared.ValueObjects;

namespace Hiper.Domain.ValueObjects
{
  public class Email : ValueObject
  {
    protected Email() { }
    public Email(string address)
    {
      Address = address;
    }

    public string Address { get; set; }
  }
}