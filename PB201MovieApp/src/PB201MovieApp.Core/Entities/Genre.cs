namespace PB201MovieApp.Core.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Movie> Movies { get; set; }
}
