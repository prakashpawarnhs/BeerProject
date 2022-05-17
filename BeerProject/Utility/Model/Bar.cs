using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeerProject.Utility.Model
{
    [Table("Bar")]
    public class Bar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BarID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Bar()
        {
        }
    }
}
