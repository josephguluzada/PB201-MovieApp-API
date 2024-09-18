namespace PB201MovieApp.MVC.ViewModels.MovieVMs
{
    public record MovieCreateVM(string Title, string Desc, bool isDeleted, int GenreId, IFormFile ImageFile);
    
}
