using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerProject.Utility.Model
{
    [Table("Brewery")]
    public class Brewery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BreweryID { get; set; }
        public string Name { get; set; }
        public Brewery()
        {
        }
    }
}
