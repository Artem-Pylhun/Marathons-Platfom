using Marathons_Platfom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathons_Platfom.Core.Context
{
    public class MarathonPlatformContext: DbContext
    {
        public MarathonPlatformContext(DbContextOptions<MarathonPlatformContext> options)
            :base(options)
        {
        }


        DbSet<User> Users => Set<User>();
        DbSet<Badge> Badges=> Set<Badge>();
        DbSet<Marathon> Marathons => Set<Marathon>();
        DbSet<Progress> Progresses => Set<Progress>();
        DbSet<Exercise> Exercises => Set<Exercise>();
        DbSet<Theme> Themes => Set<Theme>();
        DbSet<User_Badge> Users_Badges => Set<User_Badge>();
        
    }
}
