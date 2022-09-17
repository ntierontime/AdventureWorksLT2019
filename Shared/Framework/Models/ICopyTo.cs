namespace Framework.Models
{
    public interface ICopyTo<T>
        where T : class
    {
        void CopyTo(T destination);
    }
}

