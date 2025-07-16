using Microsoft.AspNetCore.Identity;

namespace Kervan.Core.Domain.Users.Entities;

public class AppUser : IdentityUser<Guid> // Id'lerimizin Guid olmasını istiyoruz.
{
    // Buraya gelecekte kullanıcıya özel ek özellikler ekleyebiliriz.
    // Örneğin:
    public string? FullName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}