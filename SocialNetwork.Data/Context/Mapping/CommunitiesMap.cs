using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class CommunitiesMap : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder
                .Property(c => c.Title)
                .HasMaxLength(50)
                .IsRequired();
            builder
                .HasOne(c => c.Creator)
                .WithMany(u => u.CreatedCommunities)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
