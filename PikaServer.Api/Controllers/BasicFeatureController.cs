using Microsoft.AspNetCore.Mvc;
using PikaServer.Api.Schemas;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Api.Controllers;

public class BasicFeatureController : ApiV1ControllerBase
{
	private readonly IHdBankBasicFeature _hdBankBasicFeature;

	public BasicFeatureController(IHdBankBasicFeature hdBankBasicFeature)
	{
		_hdBankBasicFeature = hdBankBasicFeature;
	}

	[HttpGet("balance/{accountNo}")]
	public async Task<IActionResult> GetBalance([FromRoute] string accountNo,
		CancellationToken cancellationToken = default)
	{
		return Ok(await _hdBankBasicFeature.GetBalanceAsync(accountNo, cancellationToken));
	}

	[HttpPost("transfer")]
	public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
	{
		return Ok(await _hdBankBasicFeature.TransferAsync(request.Amount, request.Description, request.FromAccountNo,
			request.ToAccountNo));
	}
}
