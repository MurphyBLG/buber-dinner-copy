namespace BuberDinner.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj) => 
        obj is ValueObject valueObject && GetEqualityComponents()
                .SequenceEqual(valueObject.GetEqualityComponents());
    
    public static bool operator ==(ValueObject left, ValueObject right)
        => Equals(left, right);
    
    public static bool operator !=(ValueObject left, ValueObject right)
        => !Equals(left, right);

    public override int GetHashCode()
        => GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);

    public bool Equals(ValueObject? other)
        => Equals((object?)other);
}
