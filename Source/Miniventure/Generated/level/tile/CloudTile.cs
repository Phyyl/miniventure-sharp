using Miniventure.Generated.gfx;

namespace com.mojang.ld22.level.tile;


public class CloudTile : Tile
{
    public CloudTile(int id) : base(id)
    {//assigns the id
    }

    /* Oh boy, it's one of these more complicated connecting tiles classes. 
	 * 
	 * Sorry if I can't explain these well - David.
     */

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(444, 444, 555, 555); // Color of the cloud
        int transitionColor = Color.Get(333, 444, 555, -1); //Transitional color between connections

        bool u = level.GetTile(x, y - 1) == Tile.InfiniteFall; //Checks if the tile above it is a infiniteFall tile.
        bool d = level.GetTile(x, y + 1) == Tile.InfiniteFall; //Checks if the tile below it is a infiniteFall tile.
        bool l = level.GetTile(x - 1, y) == Tile.InfiniteFall; //Checks if the tile to the left is a infiniteFall tile.
        bool r = level.GetTile(x + 1, y) == Tile.InfiniteFall; //Checks if the tile to the right is a infiniteFall tile.

        bool ul = level.GetTile(x - 1, y - 1) == Tile.InfiniteFall; //Checks if the upper-left tile is an infiniteFall tile.
        bool dl = level.GetTile(x - 1, y + 1) == Tile.InfiniteFall; //Checks if the lower-left tile is an infiniteFall tile.
        bool ur = level.GetTile(x + 1, y - 1) == Tile.InfiniteFall; //Checks if the upper-right tile is an infiniteFall tile.
        bool dr = level.GetTile(x + 1, y + 1) == Tile.InfiniteFall; //Checks if the lower-right tile is an infiniteFall tile.

        /* Commenter Note: All sentences with a "*" at the end means I'm making a guess, and not 100% sure. Please confirm it sometime in the future. */

        if (!u && !l)
        { // If there is no infiniteFall tile above or to the left of params this[]
            if (!ul) // if there is no infiniteFall tile at the upper-left corner params then[]
            {
                screen.Render((x * 16) + 0, (y * 16) + 0, 17, col, MirrorFlags.None); // render it as a normal flat cloud tile*
            }
            else
            {
                screen.Render((x * 16) + 0, (y * 16) + 0, 7 + (0 * 32), transitionColor, MirrorFlags.Both); // else render it as a corner piece*
            }
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 0, (l ? 6 : 5) + ((u ? 2 : 1) * 32), transitionColor, MirrorFlags.Both); // else have it render like a end peace*
        }

        if (!u && !r)
        { // If there is no infiniteFall tile above or to the right of params this[]
            if (!ur) // if there is no infiniteFall tile at the upper-right corner params then[]
            {
                screen.Render((x * 16) + 8, (y * 16) + 0, 18, col, MirrorFlags.None); // render it as a normal flat cloud tile*
            }
            else
            {
                screen.Render((x * 16) + 8, (y * 16) + 0, 8 + (0 * 32), transitionColor, MirrorFlags.Both); // else render it as a corner piece*
            }
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 0, (r ? 4 : 5) + ((u ? 2 : 1) * 32), transitionColor, MirrorFlags.Both); // else have it render like a end peace*
        }

        if (!d && !l)
        { // If there is no infiniteFall tile below or to the left of params this[]
            if (!dl) // if there is no infiniteFall tile at the lower-left corner params then[]
            {
                screen.Render((x * 16) + 0, (y * 16) + 8, 20, col, MirrorFlags.None); // render it as a normal flat cloud tile*
            }
            else
            {
                screen.Render((x * 16) + 0, (y * 16) + 8, 7 + (1 * 32), transitionColor, MirrorFlags.Both); // else render it as a corner piece*
            }
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 8, (l ? 6 : 5) + ((d ? 0 : 1) * 32), transitionColor, MirrorFlags.Both); // else have it render like a end peace*
        }

        if (!d && !r)
        { // If there is no infiniteFall tile below or to the right of params this[]
            if (!dr) // if there is no infiniteFall tile at the lower-right corner params then[]
            {
                screen.Render((x * 16) + 8, (y * 16) + 8, 19, col, MirrorFlags.None); // render it as a normal flat cloud tile*
            }
            else
            {
                screen.Render((x * 16) + 8, (y * 16) + 8, 8 + (1 * 32), transitionColor, MirrorFlags.Both); // else render it as a corner piece*
            }
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 8, (r ? 4 : 5) + ((d ? 0 : 1) * 32), transitionColor, MirrorFlags.Both); // else have it render like a end peace*
        }
    }

    /* Players can entities can walk on clouds */
    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return true;
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem tool)
        {
            if (tool.Type == ToolType.Shovel)
            {
                if (player.PayStamina(5))
                {
                    int count = Random.NextInt(2) + 1;
                    
                    for (int i = 0; i < count; i++)
                    {
                        level.Add(new ItemEntity(new ResourceItem(Resource.cloud), (xt * 16) + Random.NextInt(10) + 3, (yt * 16) + Random.NextInt(10) + 3)); //adds a cloud item to the game
                    }

                    return true;
                }
            }
        }
        return false;
    }

}
