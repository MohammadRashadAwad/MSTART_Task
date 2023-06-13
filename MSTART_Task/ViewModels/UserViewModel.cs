using MSTART_Task.Enums;
using System.ComponentModel.DataAnnotations;

namespace MSTART_Task.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Server_DateTime { get; set; }
        [Required]
        public DateTime DateTime_UTC { get; set; }
        [Required]
        public DateTime Update_DateTime_UTC { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Only English letters and numbers are allowed.")]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public Status Status { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public DateTime Date_Of_Birth { get; set; }


    }


  
}
