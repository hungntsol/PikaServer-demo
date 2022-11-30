using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PikaServer.Domain.Entities;

public abstract class RootEntityBase
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[Required]
	public int Id { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime ModifiedAt { get; set; }
}
