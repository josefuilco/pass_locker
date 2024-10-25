namespace PassLocker.Web.Response;

public record ApiResponse(
	string Message,
	object Data
);