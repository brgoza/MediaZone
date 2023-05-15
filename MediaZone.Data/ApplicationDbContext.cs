using System.Reflection;
using MediaZone.Data.Entities;
using MediaZone.Data.Entities.Identity;
using MediaZone.Data.Entities.JoinEntities;
using MediaZone.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;

namespace MediaZone.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public virtual DbSet<Folder> Folders { get; set; }
        public virtual DbSet<Face> Faces { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Folder>()
                .HasMany(f => f.Images)
                .WithOne(i => i.Folder)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Image>()
                .HasOne(i => i.Folder)
                .WithMany(f => f.Images)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<SubjectTagJoinModel>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<ImageTagJoinModel>("IMG")
                .HasValue<FolderTagJoinModel>("FLDR");

            builder.Entity<SubjectTagJoinModel>()
                .HasKey(k => new { k.SubjectId, k.TagId });

           
                
            
                
            builder.Entity<AppUser>()
                .HasMany(u => u.OwnedFolders)
                .WithOne(f => f.Owner)
                .HasForeignKey(f => f.OwnerId);

           

            builder.Entity<Folder>()
              .HasMany(folder => folder.Subscribers)
              .WithMany(user => user.SubscribedFolders)
              .UsingEntity<FolderSubscription>();
            builder.Entity<Folder>()
                .HasMany(folder => folder.AllowedUsers)
                .WithMany(user => user.AllowedFolders)
                .UsingEntity<FolderShare>();
            builder.Entity<Folder>()
             .HasMany(folder => folder.ChildFolders)
             .WithOne(child => child.ParentFolder)
             .HasForeignKey(child => child.ParentFolderId);
            //builder.Entity<Folder>()
            //   .HasMany(ft => ft.Tags)
            //   .WithMany(t => (IEnumerable<Folder>)t.Subjects)
            //   .UsingEntity<SubjectTag>();

            


            builder.Entity<Image>()
                .HasMany(img => img.Faces)
                .WithMany(face => face.Images)
                .UsingEntity<ImageFace>();
            //builder.Entity<Image>()
            //    .HasMany(zi => zi.Tags)
            //    .WithMany(t => (IEnumerable<TaggableEntity>)t.Subjects)
            //    .UsingEntity<SubjectTag>();

            builder.Entity<FolderShare>()
                .HasOne(fs => fs.AllowedUser)
                .WithMany(u => u.FolderShares)
                .HasForeignKey(fs => fs.AllowedUserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<FolderShare>()
                .HasOne(fs => fs.Folder)
                .WithMany(f => f.FolderShares)
                .HasForeignKey(fs => fs.FolderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<FolderSubscription>()
                .HasOne(fs => fs.Folder)
                .WithMany(f => f.FolderSubscriptions)
                .HasForeignKey(fs => fs.FolderId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<FolderSubscription>()
                .HasOne(fs => fs.Subscriber)
                .WithMany(s => s.FolderSubscriptions)
                .HasForeignKey(fs => fs.SubscriberId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<ImageFace>()
                .HasOne(zf => zf.Image)
                .WithMany(z => z.ImagesFaces)
                .HasForeignKey(zf => zf.ImageId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ImageFace>()
                .HasOne(zf => zf.Face)
                .WithMany(z => z.FacesImages)
                .HasForeignKey(zf => zf.FaceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<ImageTag>()
                .HasMany(t => t.Images)
                .WithMany(t => t.Tags)
                .UsingEntity<ImageTagJoinModel>();
            builder.Entity<FolderTag>()
                .HasMany(t=>t.Folders)
                .WithMany(t=>t.Tags)
                .UsingEntity<FolderTagJoinModel>();


            builder.Entity<ImageTagJoinModel>()
                .HasOne(it => it.Image)
                .WithMany(i => i.ImagesTags)
                .HasForeignKey(st => st.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<ImageTagJoinModel>()
                .HasOne(it => it.Tag)
                .WithMany(i => i.ImageTags)
                .HasForeignKey(st => st.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<FolderTagJoinModel>()
               .HasOne(it => it.Folder)
               .WithMany(i => i.FoldersTags)
               .HasForeignKey(st => st.SubjectId)
               .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<FolderTagJoinModel>()
                .HasOne(it => it.Tag)
                .WithMany(i => i.FolderTags)
                .HasForeignKey(st => st.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            ;

        }
    }
}