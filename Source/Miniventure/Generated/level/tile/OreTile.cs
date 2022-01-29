namespace com.mojang.ld22.level.tile;


public class OreTile : Tile
{
    private readonly Resource toDrop; // the resource the ore drops
    private int color; // the color of the ore

    public OreTile(int id, Resource toDrop) : base(id)
    { // assigns the id
        this.toDrop = toDrop; // assigns the drop
        color = toDrop.color & 0xffff00; // assigns the color based off the resource's color with an edit to the red & green in the color.
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        color = (toDrop.color & unchecked((int)0xffffff00)) + Color.Get(level.dirtColor); // adds the background dirt color to the sprite
        screen.Render((x * 16) + 0, (y * 16) + 0, 17 + (1 * 32), color, 0); // renders the top left of the ore
        screen.Render((x * 16) + 8, (y * 16) + 0, 18 + (1 * 32), color, 0); // renders the top right of the ore
        screen.Render((x * 16) + 0, (y * 16) + 8, 17 + (2 * 32), color, 0); // renders the bottom left of the ore
        screen.Render((x * 16) + 8, (y * 16) + 8, 18 + (2 * 32), color, 0); // renders the bottom right of the ore
    }

    /** Determines if the player can walk through the ore (they can't) */
    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        return false;
    }

    /** When you punch the ore, it will do 0 damage to the ore */
    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        Hurt(level, x, y, 0);
    }

    /** What happens when you hit the ore with a item */
    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        // converts the Item object into a ToolItem object.
        if (item is ToolItem tool)
        { // If the item happens to be a params tool[]
            if (tool.Type == ToolType.Pickaxe)
            { // if the tool type is a params pickaxe[]
                if (player.PayStamina(6 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    Hurt(level, xt, yt, 1); // Will do 1 damage to the ore
                    return true; // returns true
                }
            }
        }
        return false; // returns false
    }

    /** Hurts the ore by an amount of damage */
    public virtual void Hurt(Level level, int x, int y, int dmg)
    {
        int damage = level.GetData(x, y) + 1; // gets the amount of damage that the ore has on it
        level.Add(new SmashParticle((x * 16) + 8, (y * 16) + 8)); // creates a smash particle to the level
        level.Add(new TextParticle("" + dmg, (x * 16) + 8, (y * 16) + 8, Color.Get(-1, 500, 500, 500))); // creates a text particle telling you how much damage you have done.
        if (dmg > 0)
        { // if the damage is above params 0[]
            int count = Random.NextInt(2); // A random number between 0 to 1
            if (damage >= Random.NextInt(10) + 3)
            { // if the number of damage the ore has is ((random number between 0 to 9) + 3) params then[]
                level.SetTile(x, y, Tile.Dirt, 0); // set the level to dirt
                count += 2; // adds 2 to the count variable
            }
            else
            {
                level.SetData(x, y, damage); // else it adds up the damage that the ore has.
            }
            for (int i = 0; i < count; i++)
            {// loops through the count
                level.Add(new ItemEntity(new ResourceItem(toDrop), (x * 16) + Random.NextInt(10) + 3, (y * 16) + Random.NextInt(10) + 3)); // drops the resource to the world
            }
        }
    }

    /** This is what happens when an entity runs into the ore */
    public override void BumpedInto(Level level, int x, int y, Entity entity)
    {
        entity.Hurt(this, x, y, 3); // does 3 damage to the entity. Ouch!
    }
}