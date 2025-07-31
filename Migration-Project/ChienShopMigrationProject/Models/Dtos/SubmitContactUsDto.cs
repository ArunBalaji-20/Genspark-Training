
namespace ChienVHShopOnline.Models.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class SubmitContactUsDto
    {
        
        public string name { get; set; }=string.Empty;
        public string email { get; set; }=string.Empty;
        public string phone { get; set; }=string.Empty;
        public string content { get; set; }=string.Empty;
    }
}
