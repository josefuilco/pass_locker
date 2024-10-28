using System.ComponentModel.DataAnnotations;

namespace PassLocker.Web.Request;

public record TagRequest(
	[Required]
	[MinLength(1)]
	[MaxLength(25)]
	string Name,
	[Required]
	[MinLength(1)]
	[MaxLength(150)]
	string Description
);