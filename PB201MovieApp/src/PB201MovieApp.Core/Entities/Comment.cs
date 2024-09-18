namespace PB201MovieApp.Core.Entities;

public class Comment : BaseEntity
{
    public int MovieId { get; set; }
    public string AppUserId { get; set; }
    public string Content { get; set; }

    public Movie Movie { get; set; }
    public AppUser AppUser { get; set; }
}
