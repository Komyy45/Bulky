using Bulky.Core.Entities;

namespace Bulky.Core.Models.Identity;

public record RegisterDto(string Email, string UserName, string Password, string Name, string Role);