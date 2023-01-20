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
                .WithMany(p => p.Topics)
                .HasForeignKey(pt => pt.PostId);
            builder
                .HasOne(pt => pt.Topic)
                .WithMany(p => p.Posts)
                .HasForeignKey(pt => pt.TopicId);
        }
    }
}
