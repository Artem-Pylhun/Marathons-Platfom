using Marathons_Platform.API.Interfaces;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace Marathons_Platform.API.Implementation
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment environment;
        public FileService(IWebHostEnvironment env)
        {
            environment = env;
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "BadgePhotos", imageFileName);
                if (File.Exists(dir))
                {
                    File.Delete(dir);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string SaveImage(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            try
            {
                using var stream = new MemoryStream(bytes);
                var image = System.Drawing.Image.FromStream(stream);
                var img = new Bitmap(image);
                string randomFilename = Path.GetRandomFileName() + Guid.NewGuid() + ".jpeg";
                var dir = Path.Combine(Directory.GetCurrentDirectory(), "BadgePhotos", randomFilename);
                img.Save(dir, ImageFormat.Jpeg);
                return randomFilename;
            }
            catch
            {
                throw;   
            }
            
        }
    }
}
