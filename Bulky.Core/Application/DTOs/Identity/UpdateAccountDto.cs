using Bulky.Core.Domain.Entities;

namespace Bulky.Core.Application.Models.Identity;

public record UpdateAccountDto(string Id, string Name, string UserName, Address Address);