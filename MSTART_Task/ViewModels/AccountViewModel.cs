using Microsoft.AspNetCore.Mvc.Rendering;
using MSTART_Task.Enums;
using MSTART_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace MSTART_Task.ViewModels
{


    public class AccountViewModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "User ID is required.")]
        public int User_ID { get; set; }
        [Required]
        public DateTime Server_DateTime { get; set; }
        [Required]
        public DateTime DateTime_UTC { get; set; }
        [Required]
        public DateTime Update_DateTime_UTC { get; set; }
        [Required]
        [RegularExpression("^[0-9]{7}$", ErrorMessage = "Account number must be 7 digits.")]
        public string Account_Number { get; set; }
        [Required(ErrorMessage = "Balance is required.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Balance must be a decimal value.")]
        public decimal Balance { get; set; }
        [Required(ErrorMessage = "Currency is required.")]
        public string Currency { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public Status Status { get; set; }
        //public User User { get; set; }

       

    }




}



