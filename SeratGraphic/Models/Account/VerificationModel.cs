using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeratGraphic.Models.Account
{
    public class VerificationModel
    {
        [Required(ErrorMessage = "شماره همراه نمی تواند فاقد مقدار باشد")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "کد فعالسازی نمی تواند فاقد مقدار باشد")]
        public string Code { get; set; }
    }
}
