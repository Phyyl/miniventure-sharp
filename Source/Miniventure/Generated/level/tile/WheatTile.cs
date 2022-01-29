using Miniventure.Generated.gfx;

namespace com.mojang.ld22.level.tile;


public class WheatTile : Tile
{
    public WheatTile(int id) : base(id)
    { // assigns the id
    }

    /** Draws the tile to the screen */
    public override void Render(Screen screen, Level level, int x, int y)
    {
        int age = level.GetData(x, y); // gets the tile's age
        int col = Color.Get(level.dirtColor - 121, level.dirtColor - 11, level.dirtColor, 50); // gets the color of the tile
        int icon = age / 10; // gets the icon of the tile based on it's age
        if (icon >= 3)
        { // if the icon is larger or equal to 3.
            col = Color.Get(level.dirtColor - 121, level.dirtColor - 11, 50 + (icon * 100), 40 + ((icon - 3) * 2 * 100)); // adds more color based on the icon
            if (age == 50)
            { // if the age is equal to 50 params then[]
                col = Color.Get(0, 0, 50 + (icon * 100), 40 + ((icon - 3) * 2 * 100)); // changes the color again (for fully grown wheat)
            }
            icon = 3; // sets the icon value to 3
        }

        screen.Render((x * 16) + 0, (y * 16) + 0, 4 + (3 * 32) + icon, col, MirrorFlags.None); // renders the top-left part of the tile
        screen.Render((x * 16) + 8, (y * 16) + 0, 4 + (3 * 32) + icon, col, MirrorFlags.None); // renders the top-right part of the tile
        screen.Render((x * 16) + 0, (y * 16) + 8, 4 + (3 * 32) + icon, col, MirrorFlags.Horizontal); // renders the bottom-left part of the tile
        screen.Render((x * 16) + 8, (y * 16) + 8, 4 + (3 * 32) + icon, col, MirrorFlags.Horizontal); // renders the bottom-right part of the tile
    }

    public override void Update(Level level, int xt, int yt)
    {
        /* random.nextBoolean() gives a random choice between true or false */
        if (random.NextBoolean() == false)
        {
            return; // if the random bool is false, then skip the rest of the code
        }

        int age = level.GetData(xt, yt); // gets the current age of the tile
        if (age < 50)
        {
            level.SetData(xt, yt, age + 1); // if the age of the tile is under 50, then add 1 to the age.
        }
    }

    /** determines what happens when an item is used in the tile */
    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        if (item is ToolItem)
        { // if the item is a params tool[]
            ToolItem tool = (ToolItem)item; // converts an Item object into a ToolItem object
            if (tool.Type == ToolType.Shovel)
            { // if the type is a shovel
                if (player.payStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    level.SetTile(xt, yt, Tile.dirt, 0); // then set the tile to a dirt tile
                    return true;
                }
            }
        }
        return false;
    }

    /** What happens when you step on the tile */
    public override void SteppedOn(Level level, int xt, int yt, Entity entity)
    {
        if (random.NextInt(60) != 0)
        {
            return; // if a random number between 0 and 59 does NOT equal 0, then skip the rest of this code
        }

        if (level.GetData(xt, yt) < 2)
        {
            return; // if the age of this tile is less than 2, then skip the rest of this code
        }

        harvest(level, xt, yt); // harvest the tile
    }

    /** What happens when you punch the tile */
    public override void Hurt(Level level, int x, int y, Mob source, int dmg, Direction attackDir)
    {
        harvest(level, x, y); // harvest the tile
    }

    private void harvest(Level level, int x, int y)
    {
        int age = level.GetData(x, y); // gets the current age of the tile

        int count = random.NextInt(2); // creates a random amount from 0 to 1 
        for (int i = 0; i < count; i++)
        { // cycles through the count
            level.Add(new ItemEntity(new ResourceItem(Resource.seeds), (x * 16) + random.NextInt(10) + 3, (y * 16) + random.NextInt(10) + 3)); // adds seeds to the world
        }

        count = 0; // reset the count
        if (age == 50)
        { // if the age is equal to 50 (fully grown) params then[]
            count = random.NextInt(3) + 2; // count will be anywhere between 2 to 4
        }
        else if (age >= 40)
        { // if the age is larger or equal to 40 params then[]
            count = random.NextInt(2) + 1; // count will be anywhere between 1 and 2
        }
        for (int i = 0; i < count; i++)
        { // loops through the count
            level.Add(new ItemEntity(new ResourceItem(Resource.wheat), (x * 16) + random.NextInt(10) + 3, (y * 16) + random.NextInt(10) + 3)); // adds wheat to the world
        }

        level.SetTile(x, y, Tile.dirt, 0); // sets the tile to a dirt tile
    }
}
