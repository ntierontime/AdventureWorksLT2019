namespace Framework.Models
{
    public interface IGetIdentifier<TIdentifier>
        where TIdentifier : class
    {
        TIdentifier GetIdentifier();
    }
}

