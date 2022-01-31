namespace Miniventure.Graphics
{
    public class Pixels
    {
        public int[] Data { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public int Length => Data.Length;

        public int this[int index]
        {
            get => Data[index];
            set => Data[index] = value;
        }

        public int this[int x, int y]
        {
            get => Data[x + y * Width];
            set => Data[x + y * Width] = value;
        }

        public Pixels(int width, int height, int[] data)
        {
            Width = width;
            Height = height;
            Data = data;
        }

        public Pixels(int width, int height)
            : this(width, height, new int[width * height])
        {
        }

        public void Resize(int width, int height)
        {
            Data = new int[width * height];
            Width = width;
            Height = height;
        }
    }
}
