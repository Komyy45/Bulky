using Bulky.Core.Domain.Entities;

namespace Bulky.Core.Application.Models.Identity;

public record UpdatedAccountDto(string UserName, string Email, string Name, Address Address);