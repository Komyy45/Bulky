using Bulky.Core.Domain.Entities;

namespace Bulky.Core.Application.Models.Identity;

public record RegisterDto(string Email, string UserName, string Password, string Name, string Role);