using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class MediaMap : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder
                .HasKey(m => m.Id);
            builder
                .Property(m => m.Url)
                .IsRequired();
            builder
                .Property(m => m.MediaType)
                .IsRequired();
        }
    }
}
