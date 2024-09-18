namespace PB201MovieApp.MVC.ViewModels.GenreVMs
{
    public record GenreGetVM(int Id, string Name, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate);
}
