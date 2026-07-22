using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SocialCircle.Models;

public partial class SocialCircleDbContext : DbContext
{
    public SocialCircleDbContext()
    {
    }

    public SocialCircleDbContext(DbContextOptions<SocialCircleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<DirectMessage> DirectMessages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostLike> PostLikes { get; set; }

    public virtual DbSet<Story> Stories { get; set; }

    public virtual DbSet<StoryView> StoryViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFollow> UserFollows { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server= FEJIRO\\SQLEXPRESS;Database=SocialCircleDB;Integrated Security=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFAA3496A710");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__PostID__45F365D3");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__UserID__44FF419A");
        });

        modelBuilder.Entity<DirectMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__DirectMe__C87C037CE458A36D");

            entity.ToTable("DirectMessage");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.DirectMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DirectMes__Recei__4BAC3F29");

            entity.HasOne(d => d.Sender).WithMany(p => p.DirectMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DirectMes__Sende__4AB81AF0");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__AA126038FB06812D");

            entity.ToTable("Post");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__UserID__3D5E1FD2");
        });

        modelBuilder.Entity<PostLike>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.PostId }).HasName("PK__PostLike__8D29EAAF98461720");

            entity.ToTable("PostLike");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.LikeDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostLike__PostID__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostLike__UserID__4F7CD00D");
        });

        modelBuilder.Entity<Story>(entity =>
        {
            entity.HasKey(e => e.StoryId).HasName("PK__Story__3E82C02815FCCDD8");

            entity.ToTable("Story");

            entity.Property(e => e.StoryId).HasColumnName("StoryID");
            entity.Property(e => e.ExpirationTime)
                .HasComputedColumnSql("(dateadd(hour,(24),[Timestamp]))", false)
                .HasColumnType("datetime");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Stories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Story__UserID__412EB0B6");
        });

        modelBuilder.Entity<StoryView>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.StoryId }).HasName("PK__StoryVie__8460E0AE2F3284C5");

            entity.ToTable("StoryView");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.StoryId).HasColumnName("StoryID");
            entity.Property(e => e.ViewDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Story).WithMany(p => p.StoryViews)
                .HasForeignKey(d => d.StoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoryView__Story__5AEE82B9");

            entity.HasOne(d => d.User).WithMany(p => p.StoryViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoryView__UserI__59FA5E80");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACC49A7F43");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D1053429FDC1A5").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__User__C9F2845670982EE9").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(150);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.ProfilePicUrl)
                .HasMaxLength(2083)
                .HasColumnName("ProfilePicURL");
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserFollow>(entity =>
        {
            entity.HasKey(e => new { e.FollowerId, e.FollowingId }).HasName("PK__UserFoll__79CB03DBB44FDF67");

            entity.ToTable("UserFollow");

            entity.Property(e => e.FollowerId).HasColumnName("FollowerID");
            entity.Property(e => e.FollowingId).HasColumnName("FollowingID");
            entity.Property(e => e.FollowDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Follower).WithMany(p => p.UserFollowFollowers)
                .HasForeignKey(d => d.FollowerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserFollo__Follo__5441852A");

            entity.HasOne(d => d.Following).WithMany(p => p.UserFollowFollowings)
                .HasForeignKey(d => d.FollowingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserFollo__Follo__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
