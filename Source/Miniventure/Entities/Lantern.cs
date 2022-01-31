namespace Miniventure.Entities;

public class Lantern : Furniture
{
    public Lantern()
        : base("Lantern", 5, Color.Get(-1, 000, 111, 555), horizontalRadius: 3, verticalRadius: 2)
    {
    }

    public override int GetLightRadius()
    {
        return 8;
    }
}