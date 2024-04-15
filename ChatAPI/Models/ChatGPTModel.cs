using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatAPI.Models
{
    public class ChatGPTModel
    {
        [Required]
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}