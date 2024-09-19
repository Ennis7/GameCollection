using Microsoft.EntityFrameworkCore;
using GameCollection.Data;

namespace GameCollection.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new GameCollectionContext(
                serviceProvider.GetRequiredService<DbContextOptions<GameCollectionContext>>()))
            {
                if (context == null || context.Owner == null)
                {
                    throw new ArgumentNullException("Null GameCollection");
                }
                //Looking for any game
                if (context.Owner.Any())
                {
                    return; //Db has bee seeded
                }
                context.Owner.AddRange(
                new Owner
                {
                    Name = "Sarah",
                    Password = "7"

                },
                new Owner
                {
                    Name = "Ethan",
                    Password = "5"

                },
                new Owner
                {
                    Name = "Kazi",
                    Password = "4"

                },
                new Owner
                {
                    Name = "Tyler",
                    Password = "3"

                }
                );
                context.SaveChanges();

                context.Games.AddRange(
                    new Games
                    {
                        Title = "Kena",
                        GenreType = Genre.Adventure,
                        Developer = "Ember Lab",
                        ReleaseDate = DateTime.Parse("09/21/2021"),
                        Price = 30,
                        OwnerID = 1
                    },

                    new Games
                    {
                        Title = "The Legend of Zelda: Ocarina of Time",
                        GenreType = Genre.Adventure,
                        Developer = "Nintendo",
                        ReleaseDate = DateTime.Parse("09/21/1998"),
                        Price = 130,
                        OwnerID = 2
                    },

                    new Games
                    {
                        Title = "Soul Calibur",
                        GenreType = Genre.Fighting,
                        Developer = "Project Soul",
                        ReleaseDate = DateTime.Parse("07/30/1998"),
                        Price = 20,
                        OwnerID = 3
                    },

                    new Games
                    {
                        Title = "Gta 4",
                        GenreType = Genre.OpenWorld,
                        Developer = "Rockstar",
                        ReleaseDate = DateTime.Parse("04/29/2008"),
                        Price = 14,
                        OwnerID = 1
                    },

                    new Games
                    {
                        Title = "Super Mario Galaxy",
                        GenreType = Genre.Adventure,
                        Developer = "Nintendo",
                        ReleaseDate = DateTime.Parse("11/01/2007"),
                        Price = 15,
                        OwnerID = 1
                    },

                    new Games
                    {
                        Title = "Super Mario Galaxy 2",
                        GenreType = Genre.Adventure,
                        Developer = "Nintendo",
                        ReleaseDate = DateTime.Parse("05/24/2010"),
                        Price = 15,
                        OwnerID = 4
                    },

                    new Games
                    {
                        Title = "The Legend of Zelda: Breath of the Wild",
                        GenreType = Genre.Action,
                        Developer = "Nintendo",
                        ReleaseDate = DateTime.Parse("03/03/2017"),
                        Price = 59,
                        OwnerID = 3
                    },

                    new Games
                    {
                        Title = "Tony Hawk's Pro Skater 3",
                        GenreType = Genre.Sports,
                        Developer = "Neversoft",
                        ReleaseDate = DateTime.Parse("10/31/2001"),
                        Price = 22,
                        OwnerID = 2
                    },

                    new Games
                    {
                        Title = "Perfect Dark",
                        GenreType = Genre.FPS,
                        Developer = "Rare",
                        ReleaseDate = DateTime.Parse("03/22/2000"),
                        Price = 68,
                        OwnerID = 4
                    },

                    new Games
                    {
                        Title = "Red Dead Redemption 2",
                        GenreType = Genre.Action,
                        Developer = "Rockstar",
                        ReleaseDate = DateTime.Parse("10/26/2018"),
                        Price = 23,
                        OwnerID = 3
                    },

                    new Games
                    {
                        Title = "Gta 5",
                        GenreType = Genre.Action,
                        Developer = "Rockstar",
                        ReleaseDate = DateTime.Parse("09/17/2013"),
                        Price = 39,
                        OwnerID = 2
                    },

                    new Games
                    {
                        Title = "Metroid Prime",
                        GenreType = Genre.Action,
                        Developer = "Retro Studios",
                        ReleaseDate = DateTime.Parse("11/18/2002"),
                        Price = 20,
                        OwnerID = 4
                    },

                    new Games
                    {
                        Title = "Gta 3",
                        GenreType = Genre.Action,
                        Developer = "Rockstar",
                        ReleaseDate = DateTime.Parse("10/21/2002"),
                        Price = 14,
                        OwnerID = 4
                    }
                ); 
                context.SaveChanges();
            }
        }
        
    }
}
