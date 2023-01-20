using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class FollowsMap : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder
                .HasKey(f => new { f.FollowerId, f.FollowedId });
            builder
                .HasOne(f => f.Follower)
                .WithMany(u => u.Follows)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.ClientCascade);
            builder
                .HasOne(f => f.Followed)
                .WithMany(u => u.FollowedBy)
                .HasForeignKey(f => f.FollowedId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
