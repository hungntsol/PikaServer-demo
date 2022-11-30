namespace PikaServer.Domain.Entities;

public class Account : RootEntityBase
{
	public Account()
	{
	}

	public Account(string username, string fullName, string email, string identityNumber, string phone,
		AccountRole role) : this()
	{
		FullName = fullName;
		Email = email;
		IdentityNumber = identityNumber;
		Phone = phone;
		Username = username;
		Role = role;
	}

	public Account(int id, string hdBankUserId, string fullName, string email,
		string identityNumber, string phone, string username, AccountRole role) : this(fullName, email, identityNumber,
		phone, username, role)
	{
		Id = id;
		HdBankUserId = hdBankUserId;
	}

	public string HdBankUserId { get; set; }
	public string FullName { get; set; }
	public string Username { get; set; }
	public string Email { get; set; }
	public string IdentityNumber { get; set; }
	public string Phone { get; set; }
	public AccountRole Role { get; set; }
}

public enum AccountRole
{
	Normal,
	Official
}
