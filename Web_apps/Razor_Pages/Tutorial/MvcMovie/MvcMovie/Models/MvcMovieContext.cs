/** 
 * Creates a DbSet property for the entity set. An entity set typically corresponds
 * to a database table, and an entity corresponds to a row in the table.
*/

using System;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options) : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; }
    }
}
