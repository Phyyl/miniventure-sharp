namespace com.mojang.ld22.entity;


public class Lantern : Furniture
{



    public Lantern() : base("Lantern", horizontalRadius: 3, verticalRadius: 2)
    {
        col = Color.Get(-1, 000, 111, 555);
        sprite = 5;
    }


    public override int GetLightRadius()
    {
        return 8;
    }
}