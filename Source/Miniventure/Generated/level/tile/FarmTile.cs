using Miniventure.Generated.gfx;

namespace com.mojang.ld22.level.tile;


public class FarmTile : Tile
{
    public FarmTile(int id) : base(id)
    { //assigns the id
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.dirtColor - 121, level.dirtColor - 11, level.dirtColor, level.dirtColor + 111); // gives the tile color based on level.dirtColor
        screen.Render((x * 16) + 0, (y * 16) + 0, 2 + 32, col, MirrorFlags.Horizontal); // renders the top-left tile
        screen.Render((x * 16) + 8, (y * 16) + 0, 2 + 32, col, 0); // renders the top-right tile
        screen.Render((x * 16) + 0, (y * 16) + 8, 2 + 32, col, 0); // renders bottom-left tile
        screen.Render((x * 16) + 8, (y * 16) + 8, 2 + 32, col, MirrorFlags.Horizontal); // renders the bottom-right
    }

    /* What happens when you use a tool on this tile */
    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        { // if the item happens to be a params tool[]
            ToolItem tool = (ToolItem)item; // converts the Item object to a ToolItem object
            if (tool.Type == ToolType.Shovel)
            { // if the type of the tool is a params shovel[]
                if (player.payStamina(4 - (int)tool.Level))
                { // If the player can pay the params stamina[]
                    level.SetTile(xt, yt, Tile.dirt, 0); // sets the tile into a dirt tile
                    return true;
                }
            }
        }
        return false;
    }

    public override void Update(Level level, int xt, int yt)
    {
        int age = level.GetData(xt, yt); // gets the current age of the tile
        if (age < 5)
        {
            level.SetData(xt, yt, age + 1); // if the age is under 5, then adds 1 to the age
        }
    }

    /** What happens when you step on the tile */
    public override void SteppedOn(Level level, int xt, int yt, Entity entity)
    {

        if (random.NextInt(60) != 0)
        {
            return; // if a random number between 0 and 59 does NOT equal 0, then skip the rest of this code
        }

        if (level.GetData(xt, yt) < 5)
        {
            return; // if the age of this tile is less than 5, then skip the rest of this code
        }
        //if (entity is Player) // uncommented this bit if you only want the player to trample crops.
        level.SetTile(xt, yt, Tile.dirt, 0); // sets the tile to dirt.

    }
}
