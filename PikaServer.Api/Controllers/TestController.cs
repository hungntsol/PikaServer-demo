using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PikaServer.Domain.Entities;
using PikaServer.Infras.Services.Credential;
using PikaServer.Persistence.Internal.Abstracts;

namespace PikaServer.Api.Controllers;

public class TestController : ApiV1ControllerBase
{
	private readonly HdBankCredential _hdBankCredential;
	private readonly IUnitOfWork _unitOfWork;

	public TestController(HdBankCredential hdBankCredential, IUnitOfWork unitOfWork)
	{
		_hdBankCredential = hdBankCredential;
		_unitOfWork = unitOfWork;
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

	[HttpPost("new_account")]
	public async Task<IActionResult> CreateAccount([FromBody] Account account)
	{
		var newAcc = await _unitOfWork.Account.InsertAsync(account);
		await _unitOfWork.CommitAsync();
		return Ok(newAcc);
	}
}
