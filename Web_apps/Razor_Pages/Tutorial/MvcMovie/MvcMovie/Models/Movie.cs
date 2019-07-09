/** 
 * Classes for managing movies in a database. The "Model" part of the MVC app.
 * ...
 * https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/adding-model?view=aspnetcore-2.2&tabs=visual-studio-mac
 * Scaffold the movie model. Scaffolding tool produces pages for Create, Read, Update,
 * and Delete (CRUD) operations for the movie model.
 * 
 * Need to create the database, and use the EF Core Migrations feature. Migrations
 * lets you create a database that matches your data model and update the database
 * schema when your data model changes.
 * After running .NET Core CLI commands...
 * The database schema is based on the model specified in the MvcMovieContext class.
*/

using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        // Id field is required by the database for the primary key.
        public int Id { get; set; }
        public string Title { get; set; }

        // The DataType attribute specifies the type of the data (Date).
        // With this attribute:
        // -The user is not required to enter the info. in the date field.
        // -Only the date is displayed, not time info.
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
    }
}
