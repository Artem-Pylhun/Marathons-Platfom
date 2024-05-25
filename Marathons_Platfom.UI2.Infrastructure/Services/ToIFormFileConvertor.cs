using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using System.IO;

namespace Marathons_Platfom.UI2.Infrastructure.Services
{
    public static class FileConversionExtensions
    {
        public static async Task<string> ToBase64Async(this IBrowserFile file)
        {
            try
            {
                using (var readStream = file.OpenReadStream())
                using (var memoryStream = new MemoryStream())
                {
                    await readStream.CopyToAsync(memoryStream);
                    var bytes = memoryStream.ToArray();
                    return Convert.ToBase64String(bytes);
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
