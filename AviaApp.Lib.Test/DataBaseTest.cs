namespace AviaApp.Lib.Test;

public class DataBaseTest
{
    private readonly DataBase _db;
    private Schedule _schedule;
    private List<Schedule> _expectedSchedules;

    public DataBaseTest()
    {
        _db = new DataBase();
        _schedule = new Schedule()
        {
            Id = 1,
            Flight = "U251",
            Departure = new DateTime(2023, 10, 14, 16, 15, 00, DateTimeKind.Utc),
            Arrival = new DateTime(2023, 10, 14, 20, 00, 00, DateTimeKind.Utc)
        };
        _expectedSchedules = new List<Schedule>() { _schedule };
    }

    private void InitDb()
    {
        _db.Drop();
        _db.Create();
        _db.Insert(_schedule);
    }
    
    [Fact]
    public void GetAll_Test()
    {
        InitDb();
        var actual = _db.GetAll();
        
        Assert.Equal(_expectedSchedules, actual);
    }

    [Fact]
    public void GetById_Test()
    {
        InitDb();
        var actual = _db.GetById(1);
        
        Assert.Equal(_schedule, actual);
    }

    [Fact]
    public void Insert_Test()
    {
        var schedule = _schedule;
        schedule.Id = 2;
        _expectedSchedules.Add(schedule);
        
        InitDb();
        var result = _db.Insert(_schedule);
        
        Assert.True(result);

        var actual = _db.GetAll();
        Assert.Equal(_expectedSchedules, actual);
    }
}