namespace Miniventure.Graphics
{
    [Flags]
    public enum MirrorFlags : byte
    {
        None = 0,
        Horizontal = 1,
        Vertical = 2,
        Both = Horizontal | Vertical
    }
}
