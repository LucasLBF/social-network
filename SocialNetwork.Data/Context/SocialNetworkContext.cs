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
            modelBuilder.ApplyConfiguration(new FollowsMap());
            modelBuilder.ApplyConfiguration(new CommunitiesMap());
            modelBuilder.ApplyConfiguration(new UserCommunitiesMap());
            modelBuilder.ApplyConfiguration(new PostsMap());
            modelBuilder.ApplyConfiguration(new CommentsMap());
            modelBuilder.ApplyConfiguration(new MediaMap());
            modelBuilder.ApplyConfiguration(new TopicsMap());
            modelBuilder.ApplyConfiguration(new PostTopicsMap());
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<EnterpriseUser>? EnterpriseUsers { get; set; }
        public DbSet<PersonalUser>? PersonalUsers { get; set; }
        public DbSet<Profile>? Profiles { get; set; }
        public DbSet<Follow>? Follows { get; set; }
        public DbSet<Community>? Communities { get; set; }
        public DbSet<UserCommunity>? UserCommunities { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Media>? Media { get; set; }
        public DbSet<Topic>? Topics { get; set; }
        public DbSet<PostTopic>? PostTopics { get; set; }
    }
}
