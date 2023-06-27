using Arsha.Core.Models;

namespace Arsha.App.Extensions
{
    public static class Class
    {
        public static string CreateImage(this IFormFile formFile, string root, string path)
        {
            string fileName = Guid.NewGuid().ToString() + formFile.FileName;
            string FullPath = Path.Combine(root, path, fileName);
            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return fileName;
        }
    }
}
