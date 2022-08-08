using Hiper.Domain.Enums;
using Hiper.Shared.ValueObjects;

namespace Hiper.Domain.ValueObjects
{
  public class Document : ValueObject
  {
    protected Document() { }
    public Document(string number, EDocumentType type)
    {
      Number = number;
      Type = type;
    }
    public string Number { get; private set; }
    public EDocumentType Type { get; private set; }
  }
}