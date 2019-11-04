using System;
using System.Collections.Generic;
using System.Text;

namespace SeratGraphic.Utility
{
    public class ServiceResult
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public static ServiceResult Okay()
        {
            return new ServiceResult
            {
                IsSuccess = true,
                Message = "عملیات با موفقیت صورت گرفت"
            };
        }
        public static ServiceResult Error()
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Message = "در انجام عملیات خطایی صورت گرفت مجددا تلاش کنید"
            };
        }
        public static ServiceResult Okay(string message)
        {
            return new ServiceResult
            {
                IsSuccess = true,
                Message = message
            };
        }
        public static ServiceResult Error(string errorMessage)
        {
            return new ServiceResult
            {
                IsSuccess = false,
                Message = errorMessage
            };
        }
    }
}
