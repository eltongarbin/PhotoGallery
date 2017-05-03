using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhotoGallery.Web.UI.Models;

namespace PhotoGallery.Web.UI.Infraestructure
{
    public class PhotoGalleryContext : DbContext
    {
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Error> Errors { get; set; }

        public PhotoGalleryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }

            #region Photo
            modelBuilder.Entity<Photo>()
                .Property(p => p.Title)
                .HasMaxLength(100);

            modelBuilder.Entity<Photo>()
                .Property(p => p.AlbumId)
                .IsRequired();
            #endregion

            #region Album
            modelBuilder.Entity<Album>()
                .Property(a => a.Title)
                .HasMaxLength(100);

            modelBuilder.Entity<Album>()
                .Property(a => a.Description)
                .HasMaxLength(500);

            modelBuilder.Entity<Album>()
                .HasMany(a => a.Photos)
                .WithOne(p => p.Album);
            #endregion

            #region User
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<User>()
                .Property(u => u.HashedPassword)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<User>()
                .Property(u => u.Salt)
                .IsRequired()
                .HasMaxLength(200);
            #endregion

            #region UserRole
            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.UserId)
                .IsRequired();

            modelBuilder.Entity<UserRole>()
                .Property(ur => ur.RoleId)
                .IsRequired();
            #endregion

            #region Role
            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);
            #endregion
        }
    }
}
