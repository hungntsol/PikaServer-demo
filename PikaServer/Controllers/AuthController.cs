using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PikaServer.Api.Schemas;
using PikaServer.Domain.Entities;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Api.Controllers;

public class AuthController : ApiV1ControllerBase
{
	private readonly IHdBankApiFeature _hdBankApiFeature;
	private readonly IHdBankAuthService _hdBankAuthService;
	private readonly IHdBankCredentialManager _hdBankCredentialManager;

	public AuthController(IHdBankAuthService hdBankAuthService, IHdBankCredentialManager hdBankCredentialManager,
		IHdBankApiFeature hdBankApiFeature)
	{
		_hdBankAuthService = hdBankAuthService;
		_hdBankCredentialManager = hdBankCredentialManager;
		_hdBankApiFeature = hdBankApiFeature;
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
		return Ok(await _hdBankApiFeature.RegisterAccountAsync(
			new Account(request.FullName, request.Email, request.IdentityNumber, request.Phone),
			request.Password));
	}
}
