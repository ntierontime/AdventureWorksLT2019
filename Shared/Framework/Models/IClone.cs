namespace Framework.Models
{
    public interface IClone<T>
        where T : class
    {
        T Clone();
    }
}

