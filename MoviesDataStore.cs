using System.Collections.Generic;
using api.Models;

namespace api
{
    public class MoviesDataStore
    {
        public static MoviesDataStore Current {get;} = new MoviesDataStore();
        public List<MovieDto> Movies{get; set;}
        public MoviesDataStore(){
            Movies = new List<MovieDto>(){
                new MovieDto(){
                Id = 1,
                Name = "Pandillas de nueva york",
                Description = "Descripcion de pandillas",
                             Casts  = new List<CastDto>() {
                            new CastDto { Id = 1, Name = "Daniel Day-Lewis",  Character = "The Butcher"},
                            new CastDto { Id = 2, Name = "Leonardo DiCaprio",  Character = "Amsterdam Vallon"},
                            new CastDto { Id = 3, Name = "Liam Neeson",  Character = "Priest Vallon"},
                        }
                },

                new MovieDto(){
                Id = 2,
                Name = "Forrest Gump",
                Description = "Descripcion de forrest gump",
                     Casts  = new List<CastDto>() {
                            new CastDto { Id = 1, Name = "Tom Hanks",  Character = "Forrest Gump"},
                            new CastDto { Id = 2, Name = "Gary Sinise",  Character = "Teniente Dan"},
                            new CastDto { Id = 3, Name = "Robin Wright",  Character = "Jenny curran"},
                        }
                },

                new MovieDto(){
                Id = 3,
                Name = "Taxi Driver",
                Description = "Descripcion de taxi driver",
                  Casts  = new List<CastDto>() {
                            new CastDto { Id = 1, Name = "Robert De Niro",  Character = "Travis Bickle"},
                            new CastDto { Id = 2, Name = "Martin scorsese",  Character = "Passenger"},
                            new CastDto { Id = 3, Name = "Jodie Foster",  Character = "Iris"},
                        }
                },
            };
        }
    }
}