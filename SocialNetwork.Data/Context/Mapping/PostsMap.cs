using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class PostsMap : MessageMap<Post>
    {
        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder);
            builder
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);
            builder
                .HasOne(p => p.Media)
                .WithOne()
                .HasForeignKey<Post>(p => p.MediaId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
