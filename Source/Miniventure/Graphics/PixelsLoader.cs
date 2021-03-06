using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using Vildmark.Resources;

namespace Miniventure.Graphics
{
    [Register(typeof(IResourceLoader<Pixels>))]
    public class PixelsLoader : IResourceLoader<Pixels>
    {
        public Pixels Load(Stream stream, Assembly assembly, string resourceName)
        {
            Bitmap bitmap = new(stream);

            var bits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            int[] data = new int[bitmap.Width * bitmap.Height];
            Marshal.Copy(bits.Scan0, data, 0, data.Length);
            bitmap.UnlockBits(bits);

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (data[i] & 0xff) / 64;
            }

            return new Pixels(bitmap.Width, bitmap.Height, data);
        }
    }
}
