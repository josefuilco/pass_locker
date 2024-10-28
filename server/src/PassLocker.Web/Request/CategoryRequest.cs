using System.ComponentModel.DataAnnotations;

namespace PassLocker.Web.Request;

public record CategoryRequest(
	[Required]
	[MinLength(1)]
	[MaxLength(25)]
	string Name
);