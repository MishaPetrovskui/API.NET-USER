using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Models
{
    public class DoctorsSpecializations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DoctorID { get; set; }
        public int SpecializationID { get; set; }
    }
}
