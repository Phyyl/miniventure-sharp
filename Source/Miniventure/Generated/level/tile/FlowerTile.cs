namespace com.mojang.ld22.level.tile;


public class FlowerTile : GrassTile
{
    public FlowerTile(int id) : base(id)
    { // this is here so that errors won't yell at us. Calls the GrassTile.java part of this class
        Tiles[id] = this; // assigns the id
        connectsToGrass = true; // this tile can connect to grass.
    }

    /** Draws the tile on the screen */
    public override void Render(Screen screen, Level level, int x, int y)
    {
        base.Render(screen, level, x, y); // calls the render method of GrassTile.java

        int data = level.GetData(x, y); // gets the data of this tile
        int shape = data / 16 % 2; // gets the shape of this tile. shape = the remainder of ((data/16) / 2)
        int flowerCol = Color.Get(10, level.grassColor, 555, 440); // color of the flower

        if (shape == 0)
        {
            screen.Render((x * 16) + 0, (y * 16) + 0, 1 + (1 * 32), flowerCol, 0); // if shape is equal to 0, then render it at the top-left part of the grass tile.
        }

        if (shape == 1)
        {
            screen.Render((x * 16) + 8, (y * 16) + 0, 1 + (1 * 32), flowerCol, 0); // if shape is equal to 1, then render it at the top-right part of the grass tile.
        }

        if (shape == 1)
        {
            screen.Render((x * 16) + 0, (y * 16) + 8, 1 + (1 * 32), flowerCol, 0); // if shape is equal to 1, then render it at the bottom-left part of the grass tile.
        }

        if (shape == 0)
        {
            screen.Render((x * 16) + 8, (y * 16) + 8, 1 + (1 * 32), flowerCol, 0); // if shape is equal to 0, then render it at the bottom-right part of the grass tile.
        }
    }

    /** What happens when you use an item on the tile */
    public override bool Interact(Level level, int x, int y, Player player, Item item, Direction attackDir)
    {
        // converts the Item object into a ToolItem object
        if (item is ToolItem tool)
        { // if the item happens to be a params tool[]
            if (tool.Type == ToolType.Shovel)
            { // if the type of the tool is a params shovel[]
                if (player.PayStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    level.Add(new ItemEntity(new ResourceItem(Resource.flower), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3)); // adds a flower to the level
                    level.Add(new ItemEntity(new ResourceItem(Resource.flower), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3)); // adds a flowre to the level
                    level.SetTile(x, y, Tile.Grass, 0); // sets the tile to a grass tile
                    return true;
                }
            }
        }
        return false;
    }

    /** What happens when you punch the tile */
    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        int count = Random.NextInt(2) + 1; // random count between 1 and 2.
        for (int i = 0; i < count; i++)
        { // cycles through the count
            level.Add(new ItemEntity(new ResourceItem(Resource.flower), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3)); // adds a flower to the world
        }
        level.SetTile(x, y, Tile.Grass, 0); // sets the tile to a grass tile
    }
}