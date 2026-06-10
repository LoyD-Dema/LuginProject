namespace Utilities
{
    public interface IFactory<T>
    {
        T Create();
    }
}