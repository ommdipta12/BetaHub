using BetaHub.Auth.Model.Entities.Auth;
using Microsoft.EntityFrameworkCore;

namespace BetaHub.Auth.Service.Infrastructure.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
		{

		}

		public DbSet<UserRegistration> Users { get; set; }

		/*protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<RoleMaster>(builder =>
			{
				builder.ToTable("TblRoleMaster")
					.HasKey(r => r.RoleId); // This sets RoleId as the primary key

				// Get all enum values of UserRoles
				var Roles = Enum.GetValues(typeof(UserRoles))
								.Cast<UserRoles>()
								.Select(role => new RoleMaster
								{
									RoleId = (int)role,  // Assuming you want to start RoleId from 1
									Role = role.ToString()  // Convert enum value to string
								});

				// Insert the enum values into the RoleMaster table
				builder.HasData(Roles);
			});

			modelBuilder.Entity<UserRegistration>(builder =>
			{
				builder.ToTable("TblUserRegistration")
					.HasKey(u => u.UserId); // This sets UserId as the primary key

				builder.HasData(new UserRegistration
				{
					UserId = 1,
					RoleId = 1,
					UserName = "superadmin",
					Name = "SUPERADMIN",
					Email = "superadmin@betahub.com",
					Mobile = "9999999999",
					Password = "",
					LoginType = "pwd",
					DeletedFlag = false,
					CreatedOn = DateTime.Now,
					CreatedBy = 1
				});
			});
		}*/
	}
}
