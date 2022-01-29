namespace com.mojang.ld22.level.tile;


public class DirtTile : Tile
{
    public DirtTile(int id) : base(id)
    { //assigns the id
    }

    public override void Render(Screen screen, Level level, int x, int y)
    {
        int col = Color.Get(level.dirtColor, level.dirtColor, level.dirtColor - 111, level.dirtColor - 111); // Colors of the dirt (more info in level.java)
        screen.Render((x * 16) + 0, (y * 16) + 0, 0, col, 0); // renders the top-left part of the tile
        screen.Render((x * 16) + 8, (y * 16) + 0, 1, col, 0); // renders the top-right part of the tile
        screen.Render((x * 16) + 0, (y * 16) + 8, 2, col, 0); // renders the bottom-left part of the tile
        screen.Render((x * 16) + 8, (y * 16) + 8, 3, col, 0); // renders the bottom-right part of the tile
    }

    public override bool Interact(Level level, int xt, int yt, Player player, Item item, Direction attackDir)
    {
        // Makes a ToolItem conversion of item.
        if (item is ToolItem tool)
        { // if the player's current item is a params tool[]
            if (tool.Type == ToolType.Shovel)
            { // if the tool is a params shovel[]
                if (player.PayStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    level.SetTile(xt, yt, Tile.Hole, 0); //sets the tile to a hole
                    level.Add(new ItemEntity(new ResourceItem(Resource.dirt), (xt * 16) + Random.NextInt(10) + 3, (yt * 16) + Random.NextInt(10) + 3)); // pops out a dirt resource
                    Sound.monsterHurt.Play();// sound plays
                    return true;
                }
            }
            if (tool.Type == ToolType.Hoe)
            { // if the tool is a params hoe[]
                if (player.PayStamina(4 - (int)tool.Level))
                { // if the player can pay the params stamina[]
                    level.SetTile(xt, yt, Tile.Farmland, 0); //sets the tile to a FarmTile
                    Sound.monsterHurt.Play(); //sound plays
                    return true;
                }
            }
        }
        return false;
    }
}
