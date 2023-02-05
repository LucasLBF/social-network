using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class PostTopicsMap : IEntityTypeConfiguration<PostTopic>
    {
        public void Configure(EntityTypeBuilder<PostTopic> builder)
        {
            builder
                .HasKey(pt => new { pt.PostId, pt.TopicId });
            builder
                .HasOne(pt => pt.Post)
                .WithMany()
                .HasForeignKey(pt => pt.PostId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(pt => pt.Topic)
                .WithMany()
                .HasForeignKey(pt => pt.TopicId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
