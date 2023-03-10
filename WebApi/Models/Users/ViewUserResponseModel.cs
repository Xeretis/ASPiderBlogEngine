namespace WebApi.Models.Users;

public class ViewUserResponseModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string? ProfilePictureUrl { get; set; }

    public IEnumerable<ViewUserPostResponseModel> Posts { get; set; }
}

public class ViewUserPostResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ThumbnailUrl { get; set; }
}