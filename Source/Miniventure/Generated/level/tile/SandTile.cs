namespace com.mojang.ld22.level.tile;


public class SandTile : Tile
{
    public SandTile(int id) : base(id)
    { // assigns the id
        connectsToSand = true; // this tile can connect to sand tiles.
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.sandColor + 2, level.sandColor, level.sandColor - 110, level.sandColor - 110); // the color of the grass
        int transitionColor = Color.Get(level.sandColor - 110, level.sandColor, level.sandColor - 110, level.dirtColor); // the transition color.

        bool u = !level.GetTile(x, y - 1).connectsToSand; // sees if the tile above this one can NOT connect to sand.
        bool d = !level.GetTile(x, y + 1).connectsToSand; // sees if the tile below this one can NOT connect to sand.
        bool l = !level.GetTile(x - 1, y).connectsToSand; // sees if the tile to the left of this one can NOT connect to sand.
        bool r = !level.GetTile(x + 1, y).connectsToSand; // sees if the tile to the right of this one can NOT connect to sand.

        bool steppedOn = level.GetData(x, y) > 0; // determines if the tile has been stepped on recently (for footprints)

        if (!u && !l)
        { // if the tile above and the tile to the left can connect to sand params then[]
            if (!steppedOn) // if the sand has not been stepped on
            {
                screen.Render((x * 16) + 0, (y * 16) + 0, 0, col, 0); // renders a flat sand sprite in the upper-left corner of the sprite.
            }
            else
            {
                screen.Render((x * 16) + 0, (y * 16) + 0, 3 + (1 * 32), col, 0); // renders a footprint instead.
            }
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 0, (l ? 11 : 12) + ((u ? 0 : 1) * 32), transitionColor, 0); // else render a end piece.
        }

        if (!u && !r)
        { // if the tile above and the tile to the right can connect to sand params then[]
            screen.Render((x * 16) + 8, (y * 16) + 0, 1, col, 0); // renders a flat sand sprite in the upper-right corner of the sprite.
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 0, (r ? 13 : 12) + ((u ? 0 : 1) * 32), transitionColor, 0); // else render a end piece.
        }

        if (!d && !l)
        { // if the tile below and the tile to the left can connect to sand params then[]
            screen.Render((x * 16) + 0, (y * 16) + 8, 2, col, 0); // renders a flat sand sprite in the lower-left corner of the sprite.
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 8, (l ? 11 : 12) + ((d ? 2 : 1) * 32), transitionColor, 0); // else render a end piece.
        }

        if (!d && !r)
        { // if the tile below and the tile to the right can connect to sand params then[]
            if (!steppedOn)
            {
                screen.Render((x * 16) + 8, (y * 16) + 8, 3, col, 0); // renders a flat sand sprite in the lower-right corner of the sprite.
            }
            else
            {
                screen.Render((x * 16) + 8, (y * 16) + 8, 3 + (1 * 32), col, 0); // renders a footprint instead.
            }
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 8, (r ? 13 : 12) + ((d ? 2 : 1) * 32), transitionColor, 0); // else render a end piece.
        }
    }

    /** Update method, updates (ticks) every 60 seconds. */
    public override void Update(Level level, int x, int y)
    {
        int d = level.GetData(x, y); // gets the current level data.
        if (d > 0)
        {
            level.SetData(x, y, d - 1); // if that data is larger than 0, then decrease it by 1.
        }
    }

    /** What happens when an entity steps on the tile */
    public override void SteppedOn(Level level, int x, int y, Entity entity)
    {
        if (entity is Mob)
        { // if the entity is a mob
            level.SetData(x, y, 10); // then set the data to 10. (footprints!)
        }
    }

    /** What happens when you use an item in this tile */
    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        { // if the item happens to be a tool
            ToolItem tool = (ToolItem)item; // converts the Item object into a ToolItem object.
            if (tool.Type == ToolType.Shovel)
            { // if the type of tool is a params shovel[]
                if (player.payStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    level.SetTile(xt, yt, Tile.dirt, 0); // set the tile to a dirt tile
                    /* Adds a sand resource to the world */
                    level.Add(new ItemEntity(new ResourceItem(Resource.sand), (xt * 16) + random.NextInt(10) + 3, (yt * 16) + random.NextInt(10) + 3));
                    return true;
                }
            }
        }
        return false;
    }
}
