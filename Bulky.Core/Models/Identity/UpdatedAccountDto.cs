using Bulky.Core.Entities;

namespace Bulky.Core.Models.Identity;

public record UpdatedAccountDto(string UserName, string Email, string Name, Address Address);