namespace com.mojang.ld22.level.tile;


public class HardRockTile : RockTile
{
    public HardRockTile(int id) : base(id)
    {
        mainColor = 334; // assigns the main color 
        darkColor = 001; // assigns the dark color (for shadows)
        t = this; // assigns the tile (for rendering purposes)
    }

    /* I changed this class to be a extension of RockTile.java. So now this class is a lot shorter than it normally is. */

    /** What happens when you punch the tile */
    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Hurt(level, x, y, 0); // when you punch the tile it will do 0 damage.
    }

    /** What happens when you use a item on the tile. */
    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        // converts the Item into a ToolItem
        if (item is ToolItem tool)
        { // if the item is a tool
            if (tool.Type == ToolType.Pickaxe && (int)tool.Level == 4)
            { // if the tool is a Gem Pickaxe params then[]
                if (player.PayStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    Hurt(level, xt, yt, Random.NextInt(10) + ((int)tool.Level * 5) + 10); // does damage to the rock.
                    return true;
                }
            }
        }
        return false;
    }

}
