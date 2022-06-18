using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EmployeeCRUD_API.Models
{
    [Table("UserInfo")]
    public class UserInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
    }
}
