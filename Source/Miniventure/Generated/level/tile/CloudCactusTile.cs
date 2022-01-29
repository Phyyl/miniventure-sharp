namespace com.mojang.ld22.level.tile;


public class CloudCactusTile : Tile
{
    public CloudCactusTile(int id) : base(id)
    { //assigns the id
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int color = Color.Get(444, 111, 333, 555);  // colors of the cloud cactus
        screen.Render((x * 16) + 0, (y * 16) + 0, 17 + (1 * 32), color, 0); // renders the top-left part of the cloud cactus
        screen.Render((x * 16) + 8, (y * 16) + 0, 18 + (1 * 32), color, 0); // renders the top-right part of the cloud cactus
        screen.Render((x * 16) + 0, (y * 16) + 8, 17 + (2 * 32), color, 0); // renders the bottom-left part of the cloud cactus
        screen.Render((x * 16) + 8, (y * 16) + 8, 18 + (2 * 32), color, 0); // renders the bottom-right part of the cloud cactus
    }

    /* Determines what can pass this tile */
    public override bool MayPass(Level level, int x, int y, Entity e)
    {
        if (e is AirWizard)
        {
            return true; // If the entity is the Air Wizard, then it can pass right through.
        }

        return false;
    }

    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        hurt(level, x, y, 0); // If you punch it, it will do 0 damage
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        { // If the item the player is holding is a params tool[]
            ToolItem tool = (ToolItem)item; // makes a ToolItem conversion of item.
            if (tool.Type == ToolType.Pickaxe)
            { // if the tool happens to be a params pickaxe[]
                if (player.payStamina(6 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    hurt(level, xt, yt, 1); // Do 1 damage to the cloud cactus (call the method below this one)
                    return true;
                }
            }
        }
        return false;
    }

    /** This hurt method is special, called from the interact method above. */
    public virtual void hurt(Level level, int x, int y, int dmg)
    {
        int damage = level.GetData(x, y) + 1; // Adds the damage to the tile's data
        level.Add(new SmashParticle((x * 16) + 8, (y * 16) + 8)); // Adds a smash particle
        level.Add(new TextParticle("" + dmg, (x * 16) + 8, (y * 16) + 8, Color.Get(-1, 500, 500, 500))); // Adds text of how much damage you've done.
        if (dmg > 0)
        { //if the damage you did is over params 0[]
            if (damage >= 10)
            { //if the current damage the cloud cactus has is equal to or larger than params 0[]
                level.SetTile(x, y, Tile.cloud, 0);// set the tile to cloud (destroys the cloud cactus tile)
            }
            else
            {
                level.SetData(x, y, damage);// else just do normal damage to it
            }
        }
    }

    public override void BumpedInto(Level level, int x, int y, Entity entity)
    {
        if (entity is AirWizard)
        {
            return; // The AirWizard will not get hurt by this
        }

        entity.Hurt(this, x, y, 3); // 3 damage will be done to anyone who bumps into this (except the air-wizard)
    }
}