namespace com.mojang.ld22.level.tile;


public class SaplingTile : Tile
{
    private Tile onType; // The tile it grows on (Grass/Sand)
    private Tile growsTo; // What the sapling grows into (Tree/Cactus)

    public SaplingTile(int id, Tile onType, Tile growsTo) : base(id)
    { // Assigns the id
        this.onType = onType; // Assigns the tile it grows on
        this.growsTo = growsTo; // Assigns the tile it grows into
        connectsToSand = onType.connectsToSand; //Becomes connect to sand if the type it grows on can connect to sand.
        connectsToGrass = onType.connectsToGrass; //Becomes connect to grass if the type it grows on can connect to grass.
        connectsToWater = onType.connectsToWater; //Becomes connect to water if the type it grows on can connect to water.
        connectsToLava = onType.connectsToLava; //Becomes connect to lava if the type it grows on can connect to sand.
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        onType.Render(screen, level, x, y); // Calls the render method of the tile it grows on
        int col = Color.Get(10, 40, 50, -1); // Color of the sapling
        screen.Render((x * 16) + 4, (y * 16) + 4, 11 + (3 * 32), col, 0); // renders the small sprite of the sapling
    }


    public override void Update(Level level, int x, int y)
    {
        int age = level.GetData(x, y) + 1;
        if (age > 100)
        {
            level.SetTile(x, y, growsTo, 0); 
        }
        else
        {
            level.SetData(x, y, age); 
        }
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        level.SetTile(x, y, onType, 0); 
    }
}