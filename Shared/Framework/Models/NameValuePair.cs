namespace Framework.Models;

public class NameValuePair
{
    public string? Name { get; set; }
    public string Value { get; set; } = null!;
    public bool Selected { get; set; } = false;
}

public class NameValuePair<TValue>
{
    public string? Name { get; set; }
    public TValue Value { get; set; } = default!;
    public bool Selected { get; set; } = false;
}

