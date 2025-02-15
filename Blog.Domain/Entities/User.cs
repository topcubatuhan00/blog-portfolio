using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Entities;

public class User : IdentityUser
{
    public string? FullName { get; set; }
}
