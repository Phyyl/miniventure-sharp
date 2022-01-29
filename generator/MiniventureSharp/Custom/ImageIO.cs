using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public static class ImageIO
    {
        public unsafe static BufferedImage read(Stream stream)
        {
            Bitmap bitmap = new Bitmap(stream);

            var bits = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            int[] data = new int[bitmap.Width * bitmap.Height];
            Marshal.Copy(bits.Scan0, data, 0, data.Length);

            bitmap.UnlockBits(bits);

            return new(new Buffer(bitmap.Width, bitmap.Height, data));
        }
    }
}
