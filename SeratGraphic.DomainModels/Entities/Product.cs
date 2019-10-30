using SeratGraphic.DomainModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeratGraphic.DomainModels.Entities
{
    public class Product : BaseEntity<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        public string SmallImage { get; set; }

        public string File { get; set; }

        public string TagJson { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// پسود فایل
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// ابعاد
        /// </summary>
        public string Dimensions { get; set; }

        public ProductStatus Status { get; set; }

        public int Ranking { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }
    }
}
