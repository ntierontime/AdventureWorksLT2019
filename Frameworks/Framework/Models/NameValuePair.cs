namespace Framework.Models
{
    public class NameValuePair
    {
        public string? Name { get; set; }
        public string Value { get; set; } = null!;
        public bool Selected { get; set; } = false;
    }
}

