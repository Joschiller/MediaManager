using System.IO;
using System.Windows.Media.Imaging;

namespace MediaManager.Globals
{
    public static class ImageUtils
    {
        public static byte[] convertImageToByteArray(string fileName)
        {
            using (var image = System.Drawing.Image.FromFile(fileName))
            {
                using (var m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    return m.ToArray();
                }
            }
        }
        public static BitmapImage convertByteArrayToBitmapImage(byte[] bytes)
        {
            var bmp = new BitmapImage();
            using (var mem = new MemoryStream(bytes))
            {
                mem.Position = 0;
                bmp.BeginInit();
                bmp.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = null;
                bmp.StreamSource = mem;
                bmp.EndInit();
            }
            bmp.Freeze();
            return bmp;
        }
    }
}
