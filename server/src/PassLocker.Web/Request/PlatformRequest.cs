using System.ComponentModel.DataAnnotations;

namespace PassLocker.Web.Request;

public record PlatformRequest(
	[Required]
	[MinLength(1)]
	[MaxLength(25)]
	string Name,
	string UrlImage,
	[Required]
	int CategoryId
);