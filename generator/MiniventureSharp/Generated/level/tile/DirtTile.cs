namespace com.mojang.ld22.level.tile;


public class DirtTile : Tile {
	public DirtTile( int id) : base(id) { //assigns the id
	}

	public override void render( Screen screen,  Level level,  int x,  int y) {
		 int col = Color.get(level.dirtColor, level.dirtColor, level.dirtColor - 111, level.dirtColor - 111); // Colors of the dirt (more info in level.java)
		screen.render(x * 16 + 0, y * 16 + 0, 0, col, 0); // renders the top-left part of the tile
		screen.render(x * 16 + 8, y * 16 + 0, 1, col, 0); // renders the top-right part of the tile
		screen.render(x * 16 + 0, y * 16 + 8, 2, col, 0); // renders the bottom-left part of the tile
		screen.render(x * 16 + 8, y * 16 + 8, 3, col, 0); // renders the bottom-right part of the tile
	}

	public override bool interact( Level level,  int xt,  int yt,  Player player,  Item item,  int attackDir) {
		if (item is ToolItem) { // if the player's current item is a params tool[]
			 ToolItem tool = (ToolItem) item; // Makes a ToolItem conversion of item.
			if (tool.type == ToolType.shovel) { // if the tool is a params shovel[]
				if (player.payStamina(4 - tool.level)) { // if the player can pay the params stamina[]
					level.setTile(xt, yt, Tile.hole, 0); //sets the tile to a hole
					level.add(new ItemEntity(new ResourceItem(Resource.dirt), xt * 16 + random.nextInt(10) + 3, yt * 16 + random.nextInt(10) + 3)); // pops out a dirt resource
					Sound.monsterHurt.play();// sound plays
					return true;
				}
			}
			if (tool.type == ToolType.hoe) { // if the tool is a params hoe[]
				if (player.payStamina(4 - tool.level)) { // if the player can pay the params stamina[]
					level.setTile(xt, yt, Tile.farmland, 0); //sets the tile to a FarmTile
					Sound.monsterHurt.play(); //sound plays
					return true;
				}
			}
		}
		return false;
	}
}
