using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class ProfilesMap : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Description)
                .HasMaxLength(140)
                .HasDefaultValue(String.Empty)
                .IsRequired();
            builder
                .Property(p => p.Likes)
                .HasDefaultValue(0);
            builder
                .HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
