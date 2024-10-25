namespace PassLocker.Web.Request;

public record AccountCreationRequest(
	string Name,
	string Password,
	int PlatformId,
	int TagId
);