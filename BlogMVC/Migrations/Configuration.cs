namespace BlogMVC.Migrations
{
    using BlogMVC.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BlogMVC.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogMVC.Models.ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(new List<Category>
                {
                    new Category{Name="Sports",Description="Sport (or sports) is all forms of usually competitive physical activity which, through casual or organised participation, aim to use, maintain or improve physical ability and skills while providing entertainment to participants, and in some cases, spectators."},
                    new Category{Name="Politic",Description="Politics is the set of activities that are associated with making decisions in groups, or other forms of power relations between individuals, such as the distribution of resources or status. The academic study of politics is referred to as political science."},
                    new Category{Name="Politics",Description="Politics is the set of activities that are associated with making decisions in groups, or other forms of power relations between individuals, such as the distribution of resources or status. The academic study of politics is referred to as political science."},
                    new Category{Name="Software",Description="Software, instructions that tell a computer what to do. Software comprises the entire set of programs, procedures, and routines associated with the operation of a computer system. The term was coined to differentiate these instructions from hardware, the physical components of a computer system."},
                    new Category{Name="Future",Description="Future studies, futures research or futurology is the systematic, interdisciplinary and holistic study of social and technological advancement, and other environmental trends, often for the purpose of exploring how people will live and work in the future. "},
                    new Category{Name="Movie",Description="Movies, or films, are a type of visual communication which uses moving pictures and sound to tell stories or teach people something. Most people watch (view) movies as a type of entertainment or a way to have fun."},
                     new Category{Name="Science",Description="Movies, or films, are a type of visual communication which uses moving pictures and sound to tell stories or teach people something. Most people watch (view) movies as a type of entertainment or a way to have fun."},
                      new Category{Name="Research",Description="Movies, or films, are a type of visual communication which uses moving pictures and sound to tell stories or teach people something. Most people watch (view) movies as a type of entertainment or a way to have fun."},
                      new Category{Name="Lifestyle",Description="Movies, or films, are a type of visual communication which uses moving pictures and sound to tell stories or teach people something. Most people watch (view) movies as a type of entertainment or a way to have fun."}
                });
            }
            if (!context.Roles.Any(x => x.Name == "admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole("admin"));
            }

            if (!context.Users.Any(x => x.UserName == "admin@example.com"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new ApplicationUserManager(userStore);
                var user = new ApplicationUser()
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    NickName = "Admin01"
                };

                userManager.Create(user, "1234.Lol");
                userManager.AddToRole(user.Id, "admin");
            }


            if (!context.ReportCategories.Any())
            {
                ReportCategory reportCategory1 = new ReportCategory();
                reportCategory1.Name = "Spam";

                ReportCategory reportCategory2 = new ReportCategory();
                reportCategory2.Name = "Harassment";

                ReportCategory reportCategory3 = new ReportCategory();
                reportCategory3.Name = "Rules Violation";

                context.ReportCategories.Add(reportCategory1);
                context.ReportCategories.Add(reportCategory2);
                context.ReportCategories.Add(reportCategory3);
            }




        }

    }

}
