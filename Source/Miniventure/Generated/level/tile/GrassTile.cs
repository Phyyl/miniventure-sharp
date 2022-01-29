namespace com.mojang.ld22.level.tile;


public class GrassTile : Tile
{
    public GrassTile(int id) : base(id)
    { // assigns the id
        connectsToGrass = true; // this tile can connect to grass tiles.
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.grassColor, level.grassColor, level.grassColor + 111, level.grassColor + 111); // the color of the grass
        int transitionColor = Color.Get(level.grassColor - 111, level.grassColor, level.grassColor + 111, level.dirtColor); // the transition color.

        bool u = !level.GetTile(x, y - 1).connectsToGrass; // sees if the tile above this one can NOT connect to grass.
        bool d = !level.GetTile(x, y + 1).connectsToGrass; // sees if the tile below this one can NOT connect to grass.
        bool l = !level.GetTile(x - 1, y).connectsToGrass; // sees if the tile to the left of this one can NOT connect to grass.
        bool r = !level.GetTile(x + 1, y).connectsToGrass; // sees if the tile to the right of this one can NOT connect to grass.

        if (!u && !l)
        { // if the tile above and the tile to the left can connect to grass params then[]
            screen.Render((x * 16) + 0, (y * 16) + 0, 0, col, 0); // renders a flat grass sprite in the upper-left corner of the sprite.
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 0, (l ? 11 : 12) + ((u ? 0 : 1) * 32), transitionColor, 0); // else render a end piece.
        }

        if (!u && !r)
        {  // if the tile above and the tile to the right can connect to grass params then[]
            screen.Render((x * 16) + 8, (y * 16) + 0, 1, col, 0); // renders a flat grass sprite in the upper-right corner of the sprite
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 0, (r ? 13 : 12) + ((u ? 0 : 1) * 32), transitionColor, 0); // else render a end piece.
        }

        if (!d && !l)
        {  // if the tile below and the tile to the left can connect to grass params then[]
            screen.Render((x * 16) + 0, (y * 16) + 8, 2, col, 0); // renders a flat grass sprite in the lower-left corner of the sprite
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 8, (l ? 11 : 12) + ((d ? 2 : 1) * 32), transitionColor, 0); // else render a end piece.
        }

        if (!d && !r)
        {  // if the tile below and the tile to the right can connect to grass params then[]
            screen.Render((x * 16) + 8, (y * 16) + 8, 3, col, 0); // renders a flat grass sprite in the lower-right corner of the sprite
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 8, (r ? 13 : 12) + ((d ? 2 : 1) * 32), transitionColor, 0); // else render a end piece.
        }
    }

    /** Update method, updates (ticks) every 60 seconds. */
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

        if (level.GetTile(xn, yn) == Tile.dirt)
        { // if the next positions are a dirt tile params then[]
            level.SetTile(xn, yn, this, 0); // set that dirt tile to a grass tile
        }
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        { // if the item happens to be a tool
            ToolItem tool = (ToolItem)item; // converts the Item object into a ToolItem object.

            if (tool.Type == ToolType.Shovel)
            { // if the type of tool is a params shovel[]
                if (player.payStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    level.SetTile(xt, yt, Tile.dirt, 0); // sets the tile to a dirt tile
                    Sound.monsterHurt.Play(); // plays a sound
                    if (random.NextInt(5) == 0)
                    { // if a random value between 0 to 4 equals 0 params then[]
                      //Adds seeds to the world
                        level.Add(new ItemEntity(new ResourceItem(Resource.seeds), (xt * 16) + random.NextInt(10) + 3, (yt * 16) + random.NextInt(10) + 3));
                        return true;
                    }
                }
            }

            if (tool.Type == ToolType.Hoe)
            { // if the type of tool is a params hoe[]
                if (player.payStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    Sound.monsterHurt.Play(); // plays a sound
                    if (random.NextInt(5) == 0)
                    { // if a random value between 0 to 4 equals 0 params then[]
                      //Adds seeds to the world
                        level.Add(new ItemEntity(new ResourceItem(Resource.seeds), (xt * 16) + random.NextInt(10) + 3, (yt * 16) + random.NextInt(10) + 3));
                        return true; // skips the rest of the code
                    }
                    level.SetTile(xt, yt, Tile.farmland, 0); // sets the tile to farmland
                    return true;
                }
            }
        }
        return false;

    }
}
