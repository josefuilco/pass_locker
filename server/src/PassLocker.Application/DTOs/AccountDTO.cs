namespace PassLocker.Application.DTOs;

public record AccountDTO(
	int Id,
	string Name,
	string Password,
	string PlatformImage,
	string PlatformName,
	string Tag
);