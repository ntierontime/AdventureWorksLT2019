namespace Framework.Models
{
    public class MultiItemsCUDRequest<TIdentifier, TItem>
        where TItem : class
    {
        public List<TIdentifier>? DeleteItems { get; set; }
        /// <summary>
        /// include New Items and Updated Items whenever developer couldn't tell,
        /// </summary>
        public List<TItem>? MergeItems { get; set; }
        public List<TItem>? NewItems { get; set; }
        public List<TItem>? UpdateItems { get; set; }
    }
}

