using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class UsersMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(u => u.Id);
            builder
                .Property(u => u.FirstName)
                .IsRequired();
            builder
                .Property(u => u.Password)
                .HasMaxLength(32)
                .IsRequired();
            builder
                .Property(u => u.Email)
                .IsRequired();
            builder
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);
            builder
                .HasMany(u => u.Followers)
                .WithMany(u => u.Following)
                .UsingEntity<Follow>(
                configureRight: right => right
                .HasOne(j => j.Follower)
                .WithMany()
                .HasForeignKey(j => j.FollowerId),
                configureLeft: left => left
                .HasOne(j => j.Followed)
                .WithMany()
                .HasForeignKey(j => j.FollowedId),
                j => j.ToTable("Follows"));

            builder
                .HasMany(u => u.Communities)
                .WithMany(c => c.Users)
                .UsingEntity<UserCommunity>();
        }
    }
}
