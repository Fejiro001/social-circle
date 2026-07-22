using Microsoft.EntityFrameworkCore;
using SocialCircle.Models;
using SocialCircle.BLL;
using SocialCircle.DAL;

namespace SocialCircle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<SocialCircleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // User Relationships
            builder.Services.AddTransient<UserRepo>();
            builder.Services.AddTransient<UserService>();

            // Post Relationships
            builder.Services.AddTransient<PostRepo>();
            builder.Services.AddTransient<PostService>();

            // Story Relationships
            builder.Services.AddTransient<StoryRepo>();
            builder.Services.AddTransient<StoryService>();

            // Comment Relationships
            builder.Services.AddTransient<CommentRepo>();
            builder.Services.AddTransient<CommentService>();

            // Direct Message Relationships
            builder.Services.AddTransient<DirectMessageRepo>();
            builder.Services.AddTransient<DirectMessageService>();

            // Post Like Relationships
            builder.Services.AddTransient<PostLikeRepo>();
            builder.Services.AddTransient<PostLikeService>();

            // User Follow Relationships
            builder.Services.AddTransient<UserFollowRepo>();
            builder.Services.AddTransient<UserFollowService>();

            // Story View Relationships
            builder.Services.AddTransient<StoryViewRepo>();
            builder.Services.AddTransient<StoryViewService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
