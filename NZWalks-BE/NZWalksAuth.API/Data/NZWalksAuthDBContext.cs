using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalksAuth.API.Data
{
    public class NZWalksAuthDBContext :  IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>()
            {

                new IdentityRole()
                {
                    Id="f56945a8-9438-4a6e-bade-d0090af9bfb6",
                    ConcurrencyStamp="f56945a8-9438-4a6e-bade-d0090af9bfb6",
                    Name="Reader",
                    NormalizedName="READER"

                },

                new IdentityRole()
                {
                    Id="0bc7e11a-791e-4585-9823-ec0e0dc6cf32",
                    ConcurrencyStamp="0bc7e11a-791e-4585-9823-ec0e0dc6cf32",
                    Name="Writer",
                    NormalizedName="WRITER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
