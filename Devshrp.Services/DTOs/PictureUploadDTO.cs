
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace Devsharp.Services.DTOs
{

    public class PictureUploadDTO : BaseDTO
    {
        public IFormFile File { get; set; }
        public string ContentType { get; set; }
        public string fileExtension { get; set; }
        
    }
   

  
}
