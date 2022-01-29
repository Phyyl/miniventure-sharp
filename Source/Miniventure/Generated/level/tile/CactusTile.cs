namespace com.mojang.ld22.level.tile;


public class CactusTile : Tile
{
    public CactusTile(int id) : base(id)
    { //Assigns the id
        connectsToSand = true; // Can connect to sand
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(20, 40, 50, level.sandColor); // colors of the cactus
        screen.Render((x * 16) + 0, (y * 16) + 0, 8 + (2 * 32), col, 0); // renders the top-left part of the cactus
        screen.Render((x * 16) + 8, (y * 16) + 0, 9 + (2 * 32), col, 0); // renders the top-right part of the cactus
        screen.Render((x * 16) + 0, (y * 16) + 8, 8 + (3 * 32), col, 0); // renders the bottom-left part of the cactus
        screen.Render((x * 16) + 8, (y * 16) + 8, 9 + (3 * 32), col, 0); // renders the bottom-right part of the cactus
    }

    /* Player cannot walk on the cactus (will act as a wall) */
    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false;
    }

    /* Damage do to the cactus by the player */
    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        int damage = level.GetData(x, y) + dmg; // Damage done to the cactus (it's "health" in some sense). dmg is the amount the player did to the cactus.
        level.Add(new SmashParticle((x * 16) + 8, (y * 16) + 8)); // creates a smash particle
        level.Add(new TextParticle("" + dmg, (x * 16) + 8, (y * 16) + 8, Color.Get(-1, 500, 500, 500))); // creates a text particle about how much damage has been done.
        if (damage >= 10)
        { // If the damage is equal to, or larger than 10 params then[]
            int count = random.NextInt(2) + 1; // count is random from 0 to 1 and adds one. (1-2 count)
            for (int i = 0; i < count; i++)
            { //cycles through the count
                level.Add(new ItemEntity(new ResourceItem(Resource.cactusFlower), (x * 16) + random.NextInt(10) + 3, (y * 16) + random.NextInt(10) + 3));//adds a cactus flower
            }
            level.SetTile(x, y, Tile.sand, 0); // sets the tile to cactus
        }
        else
        {
            level.SetData(x, y, damage); // else it will set the data to damage
        }
    }

    public override void BumpedInto(Level level, int x, int y, Entity entity)
    {
        entity.Hurt(this, x, y, 1); // the player will take 1 damage if they bump into it.
    }

    public override void Update(Level level, int xt, int yt)
    {
        int damage = level.GetData(xt, yt); // gets the amount of damage the cactus has
        if (damage > 0)
        {
            level.SetData(xt, yt, damage - 1); // If the number of damage is above 0, then it will minus itself by 1 (heal)
        }
        // Commenter note: I had no idea that cactuses healed themselves.
    }
}