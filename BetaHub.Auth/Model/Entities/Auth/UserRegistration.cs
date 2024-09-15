using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetaHub.Auth.Model.Entities.Auth
{
	[Table("TblUserRegistration")]
	public class UserRegistration
	{
		[Key]
		public int UserId { get; set; }
		public int RoleId { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Mobile { get; set; }
		public string Password { get; set; }
		public string LoginType { get; set; }
		public bool LockOutEnable { get; set; }
		public byte FailedCount { get; set; }
		public DateTime? UnlockTime { get; set; }
		public bool DeletedFlag { get; set; }
		public DateTime CreatedOn { get; set; }
		public int CreatedBy { get; set; }
		public DateTime? UpdatedOn { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? DeletedOn { get; set; }
		public int? DeletedBy { get; set; }
	}
}
