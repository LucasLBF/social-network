using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class TopicsMap : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder
                .HasKey(t => t.Id);
            builder
                .Property(t => t.Name)
                .IsRequired();
        }
    }
}
