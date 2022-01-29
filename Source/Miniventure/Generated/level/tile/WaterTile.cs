using Miniventure.Generated.gfx;

namespace com.mojang.ld22.level.tile;



public class WaterTile : Tile
{
    public WaterTile(int id) : base(id)
    { // assigns the id
        connectsToSand = true; // this tile can connect to sand
        connectsToWater = true; // this tile can connect to water
    }

    private Random wRandom = new Random();

    public override void Render(Screen screen, Level level, int x, int y)
    {
        /* Sets the seed for which it will affect random variables */
        wRandom.SetSeed(((tickCount + (((x / 2) - y) * 4311)) / 10 * 54687121) + (x * 3271612) + (y * 3412987161));

        int col = Color.Get(005, 005, 115, 115); // main color of water
        int transitionColor1 = Color.Get(3, 005, level.dirtColor - 111, level.dirtColor); // transition color with dirt
        int transitionColor2 = Color.Get(3, 005, level.sandColor - 110, level.sandColor); // transition color with sand

        bool u = !level.GetTile(x, y - 1).connectsToWater; // Checks if the tile above can NOT connect to water
        bool d = !level.GetTile(x, y + 1).connectsToWater; // Checks if the tile below can NOT connect to water
        bool l = !level.GetTile(x - 1, y).connectsToWater; // Checks if the tile to the left can NOT connect to water
        bool r = !level.GetTile(x + 1, y).connectsToWater; // Checks if the tile to the right can NOT connect to water

        bool su = u && level.GetTile(x, y - 1).connectsToSand; // Checks u, and sees if the tile above can connect to sand
        bool sd = d && level.GetTile(x, y + 1).connectsToSand; // Checks d, and sees if the tile down can connect to sand 
        bool sl = l && level.GetTile(x - 1, y).connectsToSand; // Checks l, and sees if the tile to the left can connect to sand 
        bool sr = r && level.GetTile(x + 1, y).connectsToSand; // Checks r, and sees if the tile to the right can connect to sand 

        if (!u && !l)
        { // if the tile to the left, and the tile above can connect to params water[]
            screen.Render((x * 16) + 0, (y * 16) + 0, wRandom.NextInt(4), col, (MirrorFlags)wRandom.NextInt(4)); // renders the top-left part of the tile
        }
        else
        {
            /* Renders the the top-left part with a corner depending on if the tile is grass or sand */
            screen.Render((x * 16) + 0, (y * 16) + 0, (l ? 14 : 15) + ((u ? 0 : 1) * 32), (su || sl) ? transitionColor2 : transitionColor1, MirrorFlags.None);
        }

        if (!u && !r)
        { // if the tile to the right, and the tile above can connect to params water[]
            screen.Render((x * 16) + 8, (y * 16) + 0, wRandom.NextInt(4), col, (MirrorFlags)wRandom.NextInt(4)); // renders the top-right part of the tile
        }
        else
        {
            /* Renders the the top-right part with a corner depending on if the tile is grass or sand */
            screen.Render((x * 16) + 8, (y * 16) + 0, (r ? 16 : 15) + ((u ? 0 : 1) * 32), (su || sr) ? transitionColor2 : transitionColor1, MirrorFlags.None);
        }

        if (!d && !l)
        { // if the tile to the left, and the tile below can connect to params water[]
            screen.Render((x * 16) + 0, (y * 16) + 8, wRandom.NextInt(4), col, (MirrorFlags)wRandom.NextInt(4)); // renders the bottom-left part of the tile
        }
        else
        {
            /* Renders the the top-right part with a corner depending on if the tile is grass or sand */
            screen.Render((x * 16) + 0, (y * 16) + 8, (l ? 14 : 15) + ((d ? 2 : 1) * 32), (sd || sl) ? transitionColor2 : transitionColor1, MirrorFlags.None);
        }

        if (!d && !r)
        { // if the tile to the right, and the tile below can connect to params water[]
            screen.Render((x * 16) + 8, (y * 16) + 8, wRandom.NextInt(4), col, (MirrorFlags)wRandom.NextInt(4)); // renders the bottom-right part of the tile
        }
        else
        {
            /* Renders the the top-right part with a corner depending on if the tile is grass or sand */
            screen.Render((x * 16) + 8, (y * 16) + 8, (r ? 16 : 15) + ((d ? 2 : 1) * 32), (sd || sr) ? transitionColor2 : transitionColor1, MirrorFlags.None);
        }
    }

    /** Determines if the entity can pass through the block */
    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return e.CanSwim(); // If the entity can swim (which only the player can), then it will allow that entity to pass.
    }

    /** Update method, updates(ticks) every 60 ticks */
    public override void Update(Level level, int xt, int yt)
    {
        int xn = xt; // next x position
        int yn = yt; // next y position

        if (random.NextBoolean()) // makes a random decision of true or false
        {
            xn += (random.NextInt(2) * 2) - 1; // if that decision is true, then the next x position = (random value between 0 to 1) * 2 - 1
        }
        else
        {
            yn += (random.NextInt(2) * 2) - 1; // if that decision is false, then the next y position = (random value between 0 to 1) * 2 - 1
        }

        if (level.GetTile(xn, yn) == Tile.hole)
        { // if the next positions are a hole tile params then[]
            level.SetTile(xn, yn, this, 0); // set that hole tile to a water tile
        }
    }
}
