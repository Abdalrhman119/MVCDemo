using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtensions = [".jpg", ".png", ".jpeg", ".pdf"];
        int maxSize = 2_097_152;



        public string? Upload(IFormFile file, string folderName)
        {
            if(file is null) return null;
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension)) return null;
            if (file.Length > maxSize) return null;
            //C:\Users\User\source\repos\MVCDemo\Demo.Peresentation\wwwroot\
            //C:\Users\User\source\repos\MVCDemo\Demo.Peresentation\wwwroot\images
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images");
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(folderPath, fileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            //return filePath;
            return fileName;

        }

        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath) ) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }
    }
}
