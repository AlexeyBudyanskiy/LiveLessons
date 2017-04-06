namespace LiveLessons.BLL.Interfaces
{
    public interface IFilter<T>
    {
        T Execute(T input);
    }
}