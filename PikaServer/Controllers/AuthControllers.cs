using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Controllers;

public class AuthControllers : ApiV1ControllerBase
{
	private readonly IHDBankAuthService _authService;

	public AuthControllers(IHDBankAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("oauth2/token")]
	[AllowAnonymous]
	public async Task<IActionResult> OAuth2(CancellationToken cancellationToken = default)
	{
		return Ok(await _authService.OAuth2Async(cancellationToken));
	}
}
