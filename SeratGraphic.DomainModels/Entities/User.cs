using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SeratGraphic.DomainModels.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public DateTime RegisterDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
