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

	public AuthController(IHdBankAuthService hdBankAuthService, IHdBankCredentialManager hdBankCredentialManager,
		IHdBankBasicFeature hdBankBasicFeature)
	{
		_hdBankAuthService = hdBankAuthService;
		_hdBankCredentialManager = hdBankCredentialManager;
		_hdBankBasicFeature = hdBankBasicFeature;
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
		return Ok(await _hdBankAuthService.LoginAccountAsync(
			new Account { Username = request.Username },
			request.Password));
	}

	[HttpPost("change_password")]
	[AllowAnonymous]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
	{
		return Ok(await _hdBankAuthService.ChangePassword(request.Username, request.OldPassword, request.NewPassword));
	}
}
