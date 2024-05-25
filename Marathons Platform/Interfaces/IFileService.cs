namespace Marathons_Platform.API.Interfaces
{
    public interface IFileService
    {
        public string SaveImage(string imageFile);
        public bool DeleteImage(string imageFileName);
    }
}
