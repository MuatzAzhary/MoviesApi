namespace MoviesApi.Services.Interfaces
{
    public interface IImagesUpload
    {
        #region Upload method
        /// <summary>
        ///   Uploads new images and return true if succsess and false if not 
        /// </summary>
        /// <param name="image">IFormFile for image file</param>
        /// <param name="imageName">the image name</param>
        /// <param name="folderPath">the path you want to save in</param>
        /// <returns>true if succsess and false if not </returns>
        /// 
       
        Task<(bool isUploaded, string? errorMessege)> UploadAsync(IFormFile file, string imageName, string folderPath);
        #endregion

        #region Delete method
        /// <summary>
        /// Delete and existing image
        /// </summary>
        /// <param name="image">image path</param>
        void Delete(string image);
        #endregion
    }
}
