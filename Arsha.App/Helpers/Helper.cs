namespace Arsha.App.Helpers
{
    public class Helper
    {
        public static bool isImage(IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
        public static bool isSize(IFormFile file,int size) {
            return file.Length / 1024 / 1024 <= size;
        }
  
    }
}
