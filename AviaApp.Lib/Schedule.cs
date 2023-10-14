namespace AviaApp.Lib;

public class Schedule : IEquatable<Schedule>
{
    public int Id { get; set; }
    public string Flight { get; set; }
    public DateTime Departure { get; set; }
    public DateTime Arrival { get; set; }

    public bool Equals(Schedule? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && Flight == other.Flight && Departure.Equals(other.Departure) && Arrival.Equals(other.Arrival);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Schedule)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Flight, Departure, Arrival);
    }
}