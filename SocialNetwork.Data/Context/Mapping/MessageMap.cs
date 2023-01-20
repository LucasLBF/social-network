using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context.Mapping
{
    public class MessageMap<T> : IEntityTypeConfiguration<T> where T : Message
    {
        private readonly int MAX_CHARACTERS = 140;
        private readonly int DEFAULT_LIKES = 0;
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .HasKey(m => m.Id);
            builder
                .Property(m => m.Text)
                .HasMaxLength(MAX_CHARACTERS)
                .IsRequired();
            builder
                .Property(m => m.Likes)
                .HasDefaultValue(DEFAULT_LIKES);
        }
    }
}
