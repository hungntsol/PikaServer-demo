namespace PikaServer.Api.Schemas;

public class LoginRequest
{
	public LoginRequest(string username, string password)
	{
		Username = username;
		Password = password;
	}

	public string Username { get; set; }
	public string Password { get; set; }
}
