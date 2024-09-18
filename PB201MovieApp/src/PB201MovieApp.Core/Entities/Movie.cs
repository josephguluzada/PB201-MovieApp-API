namespace PB201MovieApp.Core.Entities;

public class Movie : BaseEntity
{
    public int GenreId { get; set; }
    public string Title { get; set; }
    public string Desc { get; set; }
    public Genre Genre { get; set; }

    public ICollection<MovieImage> MovieImages { get; set; }
    public ICollection<Comment> Comments { get; set; }
}
