namespace BlogApp.Entity;

public enum TagColors
{
    primary, danger, warning, success, secondary, info
}
public class Tag
{
    public int TagId { get; set; }
    public string? Text { get; set; }
    public string? Url { get; set; }
    public TagColors? Color { get; set; }
    public List<Post> Posts { get; set; } = new List<Post>();   
}
