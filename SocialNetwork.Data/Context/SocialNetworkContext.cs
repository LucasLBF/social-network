using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data.Context.Mapping;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.Data.Context
{
    public class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new EnterpriseUsersMap());
            modelBuilder.ApplyConfiguration(new PersonalUsersMap());
            modelBuilder.ApplyConfiguration(new ProfilesMap());
            modelBuilder.ApplyConfiguration(new CommunitiesMap());
            modelBuilder.ApplyConfiguration(new UserCommunitiesMap());
            modelBuilder.ApplyConfiguration(new PostsMap());
            modelBuilder.ApplyConfiguration(new CommentsMap());
            modelBuilder.ApplyConfiguration(new MediaMap());
            modelBuilder.ApplyConfiguration(new TopicsMap());
            modelBuilder.ApplyConfiguration(new PostTopicsMap());
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<EnterpriseUser> EnterpriseUsers => Set<EnterpriseUser>();
        public DbSet<PersonalUser> PersonalUsers => Set<PersonalUser>();
        public DbSet<Profile> Profiles => Set<Profile>();
        public DbSet<Community> Communities => Set<Community>();
        public DbSet<UserCommunity> UserCommunities => Set<UserCommunity>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Media> Media => Set<Media>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<PostTopic> PostTopics => Set<PostTopic>();
    }
}
