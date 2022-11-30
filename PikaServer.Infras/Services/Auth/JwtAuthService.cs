using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PikaServer.Domain.Entities;
using PikaServer.Infras.AppSettings;
using PikaServer.Infras.Services.Interfaces;

namespace PikaServer.Infras.Services.Auth;

public class JwtAuthService : IJwtAuthService
{
	private readonly JwtAuthSetting _jwtSetting;
	private readonly ILogger<JwtAuthService> _logger;

	public JwtAuthService(ILogger<JwtAuthService> logger, IOptions<JwtAuthSetting> jwtSettingOptions)
	{
		_logger = logger;
		_jwtSetting = jwtSettingOptions.Value;
	}

	public string CreateJwtAccessToken(Account account)
	{
		// credential & using SHA256
		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));
		var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);


		// describe token
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(CreateClaims(account)),
			Expires = DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSetting.ExpirationInMinutes)),
			SigningCredentials = credentials
		};

		// create token
		var tokenHandler = new JwtSecurityTokenHandler();
		var jwt = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
		var token = tokenHandler.WriteToken(jwt);

		return token;
	}

	public IEnumerable<Claim> ValidateJwtToken(string jwtToken)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		if (tokenHandler.ReadToken(jwtToken) is not JwtSecurityToken)
		{
			return ArraySegment<Claim>.Empty;
		}

		var symmetricKey = Encoding.UTF8.GetBytes(_jwtSetting.Secret);

		var validationParameters = new TokenValidationParameters
		{
			RequireExpirationTime = true,
			ValidateIssuer = false,
			ValidateAudience = false,
			IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
		};

		var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out var securityToken);

		return principal.Claims.ToList();
	}

	private IList<Claim> CreateClaims(Account account)
	{
		var logRequestInfo = "{" + $"{account.Id} {account.Email} {account.Role}" + "}";
		_logger.LogInformation("Request for access token -- {Info}", logRequestInfo);

		return new List<Claim>
		{
			new(ClaimTypes.NameIdentifier, account.Id.ToString()),
			new(ClaimTypes.Email, account.Email),
			new(ClaimTypes.Role, account.Role.ToString())
		};
	}
}
