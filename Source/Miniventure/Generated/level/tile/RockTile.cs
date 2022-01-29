using Miniventure.Generated.gfx;

namespace com.mojang.ld22.level.tile;


public class RockTile : Tile
{
    public RockTile(int id) : base(id)
    { // assigns the id
        t = this;
    }

    public Tile t; // this tile, (The reason why this is here is for HardRockTile.java)
    public int mainColor = 444; // main color of the rock
    public int darkColor = 111; // dark color of the rock

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(mainColor, mainColor, mainColor - 111, mainColor - 111); // color of the rock
        int transitionColor = Color.Get(darkColor, mainColor, mainColor + 111, level.dirtColor); // transitional color for the rock

        bool u = level.GetTile(x, y - 1) != t; // sees if the tile above this is not a rock tile.
        bool d = level.GetTile(x, y + 1) != t; // sees if the tile below this is not a rock tile.
        bool l = level.GetTile(x - 1, y) != t; // sees if the tile to the left this is not a rock tile.
        bool r = level.GetTile(x + 1, y) != t; // sees if the tile to the right this is not a rock tile.

        bool ul = level.GetTile(x - 1, y - 1) != t; // sees if the tile to the upper-left is not a rock tile.
        bool dl = level.GetTile(x - 1, y + 1) != t; // sees if the tile to the lower-left is not a rock tile.
        bool ur = level.GetTile(x + 1, y - 1) != t; // sees if the tile to the upper-right is not a rock tile.
        bool dr = level.GetTile(x + 1, y + 1) != t; // sees if the tile to the lower-right is not a rock tile.

        if (!u && !l)
        { // if there is a rock tile above, or to the left of this params then[]
            if (!ul) // if there is a rock tile to the upper-left of this one params then[]
            {
                screen.Render((x * 16) + 0, (y * 16) + 0, 0, col, MirrorFlags.None);  // render a corner piece. (upper-left sprite)
            }
            else
            {
                screen.Render((x * 16) + 0, (y * 16) + 0, 7 + (0 * 32), transitionColor, MirrorFlags.Both); // render a flat tile
            }
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 0, (l ? 6 : 5) + ((u ? 2 : 1) * 32), transitionColor, MirrorFlags.Both); // else render an end piece.
        }

        if (!u && !r)
        { // if there is a rock tile above, or to the right of this params then[]
            if (!ur) // if there is a rock tile to the upper-right of this one params then[]
            {
                screen.Render((x * 16) + 8, (y * 16) + 0, 1, col, MirrorFlags.None);  // render a corner piece. (upper-right sprite)
            }
            else
            {
                screen.Render((x * 16) + 8, (y * 16) + 0, 8 + (0 * 32), transitionColor, MirrorFlags.Both); // render a flat tile
            }
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 0, (r ? 4 : 5) + ((u ? 2 : 1) * 32), transitionColor, MirrorFlags.Both); // else render an end piece.
        }

        if (!d && !l)
        { // if there is a rock tile below, or to the left of this params then[]
            if (!dl) // if there is a rock tile to the lower-left of this one params then[]
            {
                screen.Render((x * 16) + 0, (y * 16) + 8, 2, col, MirrorFlags.None);  // render a corner piece. (lower-left sprite)
            }
            else
            {
                screen.Render((x * 16) + 0, (y * 16) + 8, 7 + (1 * 32), transitionColor, MirrorFlags.Both); // render a flat tile
            }
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 8, (l ? 6 : 5) + ((d ? 0 : 1) * 32), transitionColor, MirrorFlags.Both); // else render an end piece.
        }

        if (!d && !r)
        { // if there is a rock tile below, or to the right of this params then[]
            if (!dr) // if there is a rock tile to the lower-right of this one params then[]
            {
                screen.Render((x * 16) + 8, (y * 16) + 8, 3, col, MirrorFlags.None);  // render a corner piece. (lower-right sprite)
            }
            else
            {
                screen.Render((x * 16) + 8, (y * 16) + 8, 8 + (1 * 32), transitionColor, MirrorFlags.Both); // render a flat tile
            }
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 8, (r ? 4 : 5) + ((d ? 0 : 1) * 32), transitionColor, MirrorFlags.Both); // else render an end piece.
        }
    }

    /** Determines of the player can pass through this tile */
    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false; // the player cannot pass through it.
    }

    /** What happens when you punch the tile */
    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Hurt(level, x, y, dmg); // do a punch amount of damage to it (1-3)
    }

    /** What happens when you use an item in this tile (like a pick-axe) */
    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        // converts the Item object to a ToolItem object
        if (item is ToolItem tool)
        { //if the item is a tool
            if (tool.Type == ToolType.Pickaxe)
            { // if the type of tool is a params pickaxe[]
                if (player.PayStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    Hurt(level, xt, yt, Random.NextInt(10) + ((int)tool.Level * 5) + 10); // hurts the tile, damage based on the tool's level.
                    return true;
                }
            }
        }
        return false;
    }

    public virtual void Hurt(Level level, int x, int y, int dmg)
    {
        int damage = level.GetData(x, y) + dmg; // adds the damage.
        level.Add(new SmashParticle((x * 16) + 8, (y * 16) + 8)); // creates a smash particle
        level.Add(new TextParticle("" + dmg, (x * 16) + 8, (y * 16) + 8, Color.Get(-1, 500, 500, 500))); // adds text telling how much damage you did.
        if (damage >= 50)
        { // if the damage is larger or equal to 50 params then[]
            int count = Random.NextInt(4) + 1; // count is between 1 to 4
            for (int i = 0; i < count; i++)
            { //loops through the count
                /* adds stone to the world */
                level.Add(new ItemEntity(new ResourceItem(Resource.stone), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3));
            }
            count = Random.NextInt(2); // count is between 0 and 1
            for (int i = 0; i < count; i++)
            { // loops through the count
                /* adds coal to the world */
                level.Add(new ItemEntity(new ResourceItem(Resource.coal), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3));
            }
            level.SetTile(x, y, Tile.Dirt, 0); // sets the tile to dirt
        }
        else
        {
            level.SetData(x, y, damage); // else just set the damage data.
        }
    }

    public override void Update(Level level, int xt, int yt)
    {
        int damage = level.GetData(xt, yt); // gets the current damage of the tile
        if (damage > 0)
        {
            level.SetData(xt, yt, damage - 1); // if the damage is larger than 0, then minus the current damage by 1.
        }
    }
}
