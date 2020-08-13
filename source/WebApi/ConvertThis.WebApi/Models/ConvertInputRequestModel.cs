using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace ConvertThis.WebApi.Models
{
    public class ConvertInputRequestModel
    {
        [Required]
        public string Input { get; set; }

        [Required]
        public string ConverterType { get; set; }
    }
}
