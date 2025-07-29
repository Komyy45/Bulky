using Bulky.Core.Entities;

namespace Bulky.Core.Models.Identity;

public record UpdateAccountDto(string Id, string Name, string UserName, Address Address);