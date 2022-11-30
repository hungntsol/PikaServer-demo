using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PikaServer.Infras.Services.Credential;

namespace PikaServer.Api.Controllers;

public class TestController : ApiV1ControllerBase
{
	private readonly HdBankCredential _hdBankCredential;

	public TestController(HdBankCredential hdBankCredential)
	{
		_hdBankCredential = hdBankCredential;
	}

	[HttpGet("test_jwt")]
	[Authorize]
	public IActionResult TestAuthJwt()
	{
		return Ok();
	}

	[HttpGet("remove_cred")]
	public IActionResult RemoveCredential()
	{
		_hdBankCredential.SetIdToken("");
		_hdBankCredential.SetAccessToken("");
		return Ok();
	}
}
