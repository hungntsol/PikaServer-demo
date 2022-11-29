namespace PikaServer.Domain.Entities;

public class Account
{
	public Account(string fullName, string email, string identityNumber, string phone)
	{
		FullName = fullName;
		Email = email;
		IdentityNumber = identityNumber;
		Phone = phone;
	}

	public Account(int id, string hdBankUserId, string fullName, string email,
		string identityNumber, string phone)
	{
		Id = id;
		HdBankUserId = hdBankUserId;
		FullName = fullName;
		Email = email;
		IdentityNumber = identityNumber;
		Phone = phone;
	}

	public int Id { get; set; }
	public string HdBankUserId { get; set; }
	public string FullName { get; set; }
	public string Email { get; set; }
	public string IdentityNumber { get; set; }
	public string Phone { get; set; }
}
