using System.ComponentModel.DataAnnotations;

namespace PassLocker.Web.Request;

public record OwnerRequest(
	[Required]
	[MinLength(1)]
	[MaxLength(32)]
	string Password
);