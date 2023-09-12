using StaticContent.Services.Interfaces;


namespace StaticContent.Services
{
    public class ImageFilesService: IImageFilesService
    {

        private readonly IConfiguration _conf;
        private readonly string _imagePath;


        public ImageFilesService(IConfiguration conf)
        {
            _conf = conf;
            _imagePath = _conf.GetSection("Images").Value;
        }





        public FileStream GetById(string id)
        {
            var image = File.OpenRead(_imagePath + id);

            return image;
        }


    }
}
