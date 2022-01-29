namespace com.mojang.ld22.item;


public class ResourceItem : Item
{
    public Resource Resource; // The resource of this item
    public int Count = 1; // The amount of resources

    public ResourceItem(Resource resource)
    {
        Resource = resource; //assigns the resource
    }

    public ResourceItem(Resource resource, int count)
    {
        Resource = resource; //assigns the resource
        Count = count; //assigns the count
    }

    /** Gets the color of the resource */
    public override int GetColor()
    {
        return Resource.color;
    }

    /** Gets the sprite of the resource */
    public override int GetSprite()
    {
        return Resource.sprite;
    }

    /** Renders the icon used for the resource */
    public override void RenderIcon(Screen screen, int x, int y)
    {
        screen.Render(x, y, Resource.sprite, Resource.color, 0); // renders the icon
    }

    /** Renders the icon, name, and count of the resource */
    public override void RenderInventory(Screen screen, int x, int y)
    {
        screen.Render(x, y, Resource.sprite, Resource.color, 0); // renders the icon
        Font.Draw(Resource.name, screen, x + 32, y, Color.Get(-1, 555, 555, 555)); // draws the name of the resource
        int cc = Count; // count of the resource
        if (cc > 999)
        {
            cc = 999; // If the resource count is above 999, then just render 999 (for spacing reasons)
        }

        Font.Draw("" + cc, screen, x + 8, y, Color.Get(-1, 444, 444, 444));// draws the resource count
    }

    /** Gets the name of the resource */
    public override string GetName()
    {
        return Resource.name;
    }

    /** What happens when you pick up the item off the ground */
    public override void OnTake(ItemEntity itemEntity)
    {
    }

    /** What happens when you interact and item with the world */
    public override bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        if (Resource.InteractOn(tile, level, xt, yt, player, attackDir))
        {
            Count--;
            return true;
        }

        return false;
    }

    /** If the count is equal to, or less than 0. Then this will return true. */
    public override bool IsDepleted()
    {
        return Count <= 0;
    }

}