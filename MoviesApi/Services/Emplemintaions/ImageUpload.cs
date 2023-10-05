namespace MoviesApi.Services.Emplemintaions
{
    public class ImageUpload : IImagesUpload
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly List<string> _AllowExtentions = new() { ".png, .jpeg, .jpg" };
        private readonly int _AllowSize = 2097152;

        public ImageUpload(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        
        public async Task<(bool isUploaded, string? errorMessege)> UploadAsync(IFormFile image, string imageName, string folderPath)
        {
            var GetExtention = Path.GetExtension(image.FileName);

            //Check for image exstention and size
            if (_AllowExtentions.Contains(GetExtention))
                return (isUploaded: false, errorMessege: "Only png jpeg jpg images are allowed!");

            if (image.Length > _AllowSize)
                return (isUploaded: false, errorMessege: "The size must be less than 2MB !");

            var imagePath = Path.Combine($"{_webHost.WebRootPath}{folderPath}", imageName);
            using var stream = File.Create(imagePath);
            await image.CopyToAsync(stream);

            return (isUploaded: true, errorMessege: null);
        }
 
        public void Delete(string image)
        {
            var oldImagePath = Path.Combine($"{_webHost.WebRootPath}", image);
            if (File.Exists(oldImagePath))
                File.Delete(oldImagePath);
        }
    }
}

