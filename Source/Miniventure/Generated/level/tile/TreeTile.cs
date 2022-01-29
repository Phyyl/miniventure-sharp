namespace com.mojang.ld22.level.tile;


public class TreeTile : Tile
{
    public TreeTile(int id) : base(id)
    { // assigns the tile's id
        connectsToGrass = true; // this tile can connect to grass
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(10, 30, 151, level.grassColor); // main top tree color
        int barkCol1 = Color.Get(10, 30, 430, level.grassColor); // Lighter bark color
        int barkCol2 = Color.Get(10, 30, 320, level.grassColor); // Darker bark color

        bool u = level.GetTile(x, y - 1) == this; // checks if the tile below it is a tree tile.
        bool l = level.GetTile(x - 1, y) == this; // checks if the tile to the left is a tree tile.
        bool r = level.GetTile(x + 1, y) == this; // checks if the tile to the right is a tree tile.
        bool d = level.GetTile(x, y + 1) == this; // checks if the tile above it is a tree tile.
        bool ul = level.GetTile(x - 1, y - 1) == this; // checks if the upper-left tile is a tree tile.
        bool ur = level.GetTile(x + 1, y - 1) == this; // checks if the upper-right tile is a tree tile.
        bool dl = level.GetTile(x - 1, y + 1) == this; // checks if the bottom-left tile is a tree tile.
        bool dr = level.GetTile(x + 1, y + 1) == this; // checks if the bottom-right tile is a tree tile.

        if (u && ul && l)
        { // if there is a tree above, to the left, and to the upper left of this tile params then[]
            screen.Render((x * 16) + 0, (y * 16) + 0, 10 + (1 * 32), col, 0); // render a tree tile sprite that will connect to other trees. (top-left)
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 0, 9 + (0 * 32), col, 0); // else render the normal top-left part of the tree
        }
        if (u && ur && r)
        { // if there is a tree above, to the right, and to the upper right of this tile params then[]
            screen.Render((x * 16) + 8, (y * 16) + 0, 10 + (2 * 32), barkCol2, 0); // render a tree tile sprite that will connect to other trees. (top-right)
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 0, 10 + (0 * 32), col, 0); // else render the normal top-right part of the tree
        }
        if (d && dl && l)
        { // if there is a tree below, to the left, and to the bottom left of this tile params then[]
            screen.Render((x * 16) + 0, (y * 16) + 8, 10 + (2 * 32), barkCol2, 0); // render a tree tile sprite that will connect to other trees. (bottom-left)
        }
        else
        {
            screen.Render((x * 16) + 0, (y * 16) + 8, 9 + (1 * 32), barkCol1, 0); // else render the normal bottom-left part of the tree
        }
        if (d && dr && r)
        { // if there is a tree below, to the right, and to the bottom right of this tile params then[]
            screen.Render((x * 16) + 8, (y * 16) + 8, 10 + (1 * 32), col, 0); // render a tree tile sprite that will connect to other trees. (bottom-right)
        }
        else
        {
            screen.Render((x * 16) + 8, (y * 16) + 8, 10 + (3 * 32), barkCol2, 0); // else render the normal bottom-right part of the tree
        }
    }

    public override void Update(Level level, int xt, int yt)
    {
        int damage = level.GetData(xt, yt); // gets the damage from the tree
        if (damage > 0)
        {
            level.SetData(xt, yt, damage - 1); // if the damage is above 0, then decrease the damage by 1.
        }

        // Huh, so trees and cactuses can heal themselves, weird. - David.
    }

    /** Determines if you can pass through the tile */
    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false; // You can't walk through a tree, silly.
    }

    /** What happens when you punch the tree. */
    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Hurt(level, x, y, dmg); // you do damage to it
    }

    /** What happens when you use a item on the tree */
    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        // converts the Item object to a ToolItem object
        if (item is ToolItem tool)
        { // if the item is a params tool[]
            if (tool.Type == ToolType.Axe)
            { // if the type of tool is an params axe[]
                if (player.PayStamina(4 - (int)tool.Level))
                { // if the player can pay the stamina
                    Hurt(level, xt, yt, Random.NextInt(10) + ((int)tool.Level * 5) + 10); // do extra damage to the tree
                    return true;
                }
            }
        }
        return false;
    }

    private void Hurt(Level level, int x, int y, int dmg)
    {
        {
            int count = Random.NextInt(10) == 0 ? 1 : 0; //if a random number between 0 to 9 equals 0, then count will equal 1. else it will be 0.
            for (int i = 0; i < count; i++)
            { // loop through the count
                level.Add(new ItemEntity(new ResourceItem(Resource.apple), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3));//add an apple to the world
            }
        }
        int damage = level.GetData(x, y) + dmg; // adds damage value to the tree's data.
        level.Add(new SmashParticle((x * 16) + 8, (y * 16) + 8)); // creates a smash particle
        level.Add(new TextParticle("" + dmg, (x * 16) + 8, (y * 16) + 8, Color.Get(-1, 500, 500, 500))); // creates a text particle to tell how much damage the player did.
        if (damage >= 20)
        { // if damage is larger than or equal to 0
            int count = Random.NextInt(2) + 1; // random number between 1 to 2
            for (int i = 0; i < count; i++)
            { // cycles through the count
                level.Add(new ItemEntity(new ResourceItem(Resource.wood), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3)); // adds wood to the world
            }
            count = Random.NextInt(Random.NextInt(4) + 1); // random number between 1 to 4
            for (int i = 0; i < count; i++)
            { // cycles through the count
                level.Add(new ItemEntity(new ResourceItem(Resource.acorn), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3)); // adds an acorn to the world
            }
            level.SetTile(x, y, Tile.Grass, 0); // sets the tile to grass
        }
        else
        {
            level.SetData(x, y, damage); // else it will set the current damage to the tree
        }
    }
}
