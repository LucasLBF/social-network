using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class EnterpriseUsersMap : IEntityTypeConfiguration<EnterpriseUser>
    {
        public void Configure(EntityTypeBuilder<EnterpriseUser> builder)
        {
            builder
                .HasKey(e => e.Id);
            builder
                .Property(e => e.Category)
                .IsRequired();
            builder
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<EnterpriseUser>(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
