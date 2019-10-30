using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeratGraphic.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage ="نام و نام خانوداگی نمی تواند فاقد مقدار باشد")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "شماره همراه نمی تواند فاقد مقدار باشد")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "رمزعبور نمی تواند فاقد مقدار باشد")]
        [MinLength(6,ErrorMessage ="رمزعبور باید حداقل دارای شش کاراکتر باشد")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار رمزعبور نمی تواند فاقد مقدار باشد")]
        [Compare(nameof(Password),ErrorMessage ="رمزعبور با تکرارش مطابقت ندارد")]
        public string ConfirmPassword { get; set; }
    }
}
