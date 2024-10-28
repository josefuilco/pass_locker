using System.ComponentModel.DataAnnotations;

namespace PassLocker.Web.Request;

public record AccountGeneratedPasswordRequest(
	[Required]
	[MinLength(1)]
	[MaxLength(50)]
	string Name,
	[Required]
	int PlatformId,
	[Required]
	int TagId
);