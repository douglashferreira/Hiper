using Hiper.Shared.ValueObjects;

namespace Hiper.Domain.ValueObjects
{
  public class Name : ValueObject
  {
    protected Name() { }
    public Name(string firstName, string lastName)
    {
      FirstName = firstName;
      LastName = lastName;
    }

    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }

    public override string ToString()
    {
      return $"{FirstName} {LastName}";
    }
  }
}