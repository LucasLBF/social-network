using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;
using SocialNetwork.Data.Enums;

namespace SocialNetwork.Data.Context.Mapping
{
    public class PersonalUsersMap : IEntityTypeConfiguration<PersonalUser>
    {
        public void Configure(EntityTypeBuilder<PersonalUser> builder)
        {
            builder
                .HasKey(p => p.Id);
            builder
                .Property(p => p.Status)
                .HasDefaultValue(Status.Online)
                .IsRequired();
            builder
                .Property(p => p.Genre)
                .IsRequired();
            builder
                .Property(p => p.Age)
                .IsRequired();
            builder
                .Property(p => p.Visibility)
                .HasDefaultValue(Visibility.Public)
                .IsRequired();
            builder
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<PersonalUser>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
