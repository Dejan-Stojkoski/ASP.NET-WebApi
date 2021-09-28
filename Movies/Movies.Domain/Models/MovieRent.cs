using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Models
{
    public class MovieRent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RentId { get; set; }
        public Rent Rent { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
