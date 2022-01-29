namespace com.mojang.ld22.entity;


public class Lantern : Furniture
{

    /* This is a sub-class of furniture.java, go there for more info */

    public Lantern() : base("Lantern", horizontalRadius: 3, verticalRadius: 2)
    { // Name of the lantern
        col = Color.Get(-1, 000, 111, 555); // Color of the lantern
        sprite = 5; // Location of the sprite
    }

    /** Gets the size of the radius for light underground (Bigger number, larger light) */
    public override int GetLightRadius()
    {
        return 8;
    }
}