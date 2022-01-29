namespace com.mojang.ld22.item;

public class FurnitureItem : Item
{
    public Furniture furniture;
    public bool placed = false;

    public FurnitureItem(Furniture furniture)
    {
        this.furniture = furniture;
    }

    public override int GetColor()
    {
        return furniture.col;
    }

    public override int GetSprite()
    {
        return furniture.sprite + (10 * 32);
    }

    public override void RenderIcon(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0); // renders the icon
    }

    public override void RenderInventory(Screen screen, int x, int y)
    {
        screen.Render(x, y, GetSprite(), GetColor(), 0); // renders the icon
        Font.Draw(furniture.name, screen, x + 8, y, Color.Get(-1, 555, 555, 555)); // draws the name of the furniture
    }

    public override bool CanAttack()
    {
        return false;
    }

    public override bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        if (tile.MayPass(level, xt, yt, furniture))
        { // If the furniture can go on the tile
            furniture.X = (xt * 16) + 8; // Placed furniture's X position
            furniture.Y = (yt * 16) + 8; // Placed furniture's Y position
            level.Add(furniture); // adds the furniture to the world
            placed = true; // the value becomes true, which removes it from the player's active item
            return true;
        }
        return false;
    }

    /** Removes this item from the player's active item slot when depleted is true */
    public override bool IsDepleted()
    {
        return placed;
    }

    /** Gets the name of the furniture */
    public override string GetName()
    {
        return furniture.name;
    }
}