using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PikaServer.Api.Schemas;
using PikaServer.Domain.Entities;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Api.Controllers;

public class AuthController : ApiV1ControllerBase
{
	private readonly IHdBankAuthService _hdBankAuthService;
	private readonly IHdBankBasicFeature _hdBankBasicFeature;
	private readonly IHdBankCredentialManager _hdBankCredentialManager;
	private readonly IJwtAuthService _jwtAuthService;

	public AuthController(IHdBankAuthService hdBankAuthService, IHdBankCredentialManager hdBankCredentialManager,
		IHdBankBasicFeature hdBankBasicFeature, IJwtAuthService jwtAuthService)
	{
		_hdBankAuthService = hdBankAuthService;
		_hdBankCredentialManager = hdBankCredentialManager;
		_hdBankBasicFeature = hdBankBasicFeature;
		_jwtAuthService = jwtAuthService;
	}

	[HttpPost("oauth2/token")]
	[AllowAnonymous]
	public async Task<IActionResult> OAuth2(CancellationToken cancellationToken = default)
	{
		return Ok(await _hdBankAuthService.OAuth2Async(cancellationToken));
	}

	[HttpPost("register")]
	[AllowAnonymous]
	public async Task<IActionResult> Register(
		[FromBody] RegisterRequest request)
	{
		return Ok(await _hdBankAuthService.RegisterAccountAsync(
			new Account(request.Username, request.FullName, request.Email, request.IdentityNumber, request.Phone,
				request.Role),
			request.Password));
	}

	[HttpPost("login")]
	[AllowAnonymous]
	public async Task<IActionResult> Login([FromBody] LoginRequest request)
	{
		var loginResult = await _hdBankAuthService.LoginAccountAsync(
			new Account { Username = request.Username },
			request.Password);

		if (loginResult.IsSuccess)
		{
			// fake user in db, need to get account from db and pass as param
			var fakeUser = new Account(1, loginResult.AccountNo!, "", "test@email.com", "", "", request.Username,
				AccountRole.Normal);
			return Ok(new
			{
				AccessToken = _jwtAuthService.CreateJwtAccessToken(fakeUser)
			});
		}

		return BadRequest("Fail login");
	}

	[HttpPost("change_password")]
	[AllowAnonymous]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
	{
		return Ok(await _hdBankAuthService.ChangePassword(request.Username, request.OldPassword, request.NewPassword));
	}

	[HttpGet("test_jwt")]
	[Authorize]
	public IActionResult TestAuthJwt()
	{
		return Ok();
	}
}
