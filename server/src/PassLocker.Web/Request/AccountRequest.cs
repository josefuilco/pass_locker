using System.ComponentModel.DataAnnotations;

namespace PassLocker.Web.Request;

public record AccountRequest(
	[Required]
	[MinLength(1)]
	[MaxLength(50)]
	string Name,
	[Required]
	[MinLength(1)]
	[MaxLength(32)]
	string Password,
	[Required]
	int PlatformId,
	[Required]
	int TagId
);