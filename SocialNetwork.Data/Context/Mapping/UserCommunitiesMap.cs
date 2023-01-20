using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class UserCommunitiesMap : IEntityTypeConfiguration<UserCommunity>
    {
        public void Configure(EntityTypeBuilder<UserCommunity> builder)
        {
            builder
                .HasKey(uc => new { uc.UserId, uc.CommunityId });
            builder
                .HasOne(uc => uc.User)
                .WithMany(u => u.Communities)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(uc => uc.Community)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.CommunityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
