using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Buffers;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Vildmark.Resources;

namespace Miniventure.Graphics;

public class PixelsLoader : IResourceLoader<Pixels>
{
    public Pixels Load(string name, ResourceLoadContext context)
    {
        Image baseImage = Image.Load(context.GetStream(name));
        using var image = baseImage.CloneAs<Bgra32>();

        int[] data = new int[image.Width * image.Height];
        image.CopyPixelDataTo(MemoryMarshal.Cast<int, Bgra32>(data));

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (data[i] & 0xff) / 64;
        }

        return new Pixels(image.Width, image.Height, data);
    }
}
