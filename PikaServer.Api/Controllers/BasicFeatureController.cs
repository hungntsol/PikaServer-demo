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

	[HttpPost("transaction_history")]
	public async Task<IActionResult> TransactionHistory([FromBody] TransactionHistoryRequest request)
	{
		return Ok(
			await _hdBankBasicFeature.GetTransactionHistAsync(request.AccountNo, request.FromDate, request.ToDate));
	}

	[HttpGet("get_payment/{sdId}")]
	public async Task<IActionResult> GetPaymentFee([FromRoute] string sdId)
	{
		return Ok(await _hdBankBasicFeature.GetFeePaymentAsync(sdId));
	}

	[HttpPost("fee_payment")]
	public async Task<IActionResult> Payment([FromBody] FeePaymentRequest request)
	{
		return Ok(await _hdBankBasicFeature.CheckoutFeeAsync(request.SdId, request.Amount, request.Description,
			request.FromAaccount));
	}
}
