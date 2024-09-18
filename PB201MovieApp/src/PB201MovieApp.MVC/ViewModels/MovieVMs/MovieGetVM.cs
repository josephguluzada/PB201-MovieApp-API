using PB201MovieApp.MVC.ViewModels.CommentVMs;
using PB201MovieApp.MVC.ViewModels.MovieImageVM;

namespace PB201MovieApp.MVC.ViewModels.MovieVMs
{
    public record MovieGetVM(int Id, string Title, string Desc, bool IsDeleted, DateTime CreatedDate, DateTime ModifiedDate, int GenreId, string GenreName, ICollection<MovieImageGetVM> MovieImages, ICollection<CommentGetVM> Comments);
    
}
