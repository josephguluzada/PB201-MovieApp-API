using Microsoft.AspNetCore.Identity;

namespace PB201MovieApp.Core.Entities;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }

    public ICollection<Comment> Comments { get; set; }
}
