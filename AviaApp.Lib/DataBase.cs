using System.Data;
using Dapper;
using Npgsql;

namespace AviaApp.Lib;

public class DataBase : ICrud<Schedule>
{
    private NpgsqlConnection _db;

    public DataBase()
    {
        var connectionString = DbConfig.Import().ToString();
        _db = new NpgsqlConnection(connectionString);
    }


    public bool Insert(Schedule obj)
    {
        _db.Open();
        
        var sql =
            $"INSERT INTO table_schedules(flight, departure, arrival) VALUES ('{obj.Flight}', '{obj.Departure}', '{obj.Arrival}')";
        var result = _db.Execute(sql);
        _db.Close();

        return result > 0;
    }

    public bool Update(Schedule obj)
    {
        _db.Open();

        var sql = $"UPDATE table_schedules SET flight = '{obj.Flight}', departure = '{obj.Departure:G}', arrival = '{obj.Arrival:G}' WHERE id = {obj.Id}";
        var result = _db.Execute(sql);
        _db.Close();

        return result > 0;
    }

    public bool Delete(Schedule obj)
    {
        _db.Open();

        var sql = $"DELETE FROM table_schedules WHERE id = {obj.Id}";
        var result = _db.Execute(sql);
        _db.Close();

        return result > 0;
    }

    public IEnumerable<Schedule>? GetAll()
    {
        _db.Open();
        
        var sql = "SELECT * FROM table_schedules";

        var schedules = _db.Query<Schedule>(sql);
        
        _db.Close();

        return schedules;
    }

    public Schedule? GetById(int id)
    {
        _db.Open();

        var sql = $"SELECT * FROM table_schedules WHERE id = {id}";
        var schedule = _db.QuerySingleOrDefault<Schedule>(sql);
        
        _db.Close();

        return schedule;
    }

    public void Drop()
    {
        _db.Open();

        var sql = "DROP TABLE table_schedules";
        var command = new NpgsqlCommand(sql, _db);
        command.ExecuteNonQuery();
        
        _db.Close();
    }

    public void Create()
    {
        _db.Open();

        var sql = """
                  CREATE TABLE table_schedules (
                      id SERIAL NOT NULL PRIMARY KEY,
                      flight TEXT NOT NULL,
                      departure TIMESTAMP WITH TIME ZONE NOT NULL,
                      arrival TIMESTAMP WITH TIME ZONE NOT NULL
                  );
                  """;
        var command = new NpgsqlCommand(sql, _db);
        command.ExecuteNonQuery();
        
        _db.Close();
    }
}