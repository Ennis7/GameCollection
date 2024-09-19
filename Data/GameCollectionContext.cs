using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameCollection.Models;

namespace GameCollection.Data
{
    public class GameCollectionContext : DbContext
    {
        public GameCollectionContext (DbContextOptions<GameCollectionContext> options)
            : base(options)
        {
        }

        public DbSet<GameCollection.Models.Games> Games { get; set; } = default!;
        public DbSet<GameCollection.Models.Owner> Owner { get; set; } = default!;
    }
}
