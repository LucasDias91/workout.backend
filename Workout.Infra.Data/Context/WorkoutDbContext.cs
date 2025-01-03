using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Workout.Domain.Entities.Session;
using Workout.Domain.Entities.Training;
using Workout.Domain.Entities.User;
using Workout.Infra.Data.Mappings;

namespace Workout.Infra.Data.Context
{

    public class WorkoutDbContext: DbContext
    {
        protected readonly IConfiguration Configuration;
        public WorkoutDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<Timeline> Timelines { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Session>(new SessionMap().Configure);
            modelBuilder.Entity<Training>(new TrainingMap().Configure);
            modelBuilder.Entity<Timeline>(new TimelineMap().Configure);

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("WorkoutDb");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
