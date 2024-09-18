using PB201MovieApp.Core.Entities;
using PB201MovieApp.Core.Repositories;
using PB201MovieApp.DAL.Contexts;

namespace PB201MovieApp.DAL.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context) {}
}
