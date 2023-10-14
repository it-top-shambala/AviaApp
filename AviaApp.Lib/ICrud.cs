namespace AviaApp.Lib;

public interface ICrud<T>
{
    public bool Insert(T obj);
    public bool Update(T obj);
    public bool Delete(T obj);

    public IEnumerable<T>? GetAll();
    public T? GetById(int id);
}