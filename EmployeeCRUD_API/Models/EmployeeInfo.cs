using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeCRUD_API.Models
{
    [Table("EmployeeInfo")]
    public class EmployeeInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Name { get; set; }    
        public string? Gender { get; set; }
        public string? JobDescription { get; set; }
        public int Salary { get; set; }
    }
}
