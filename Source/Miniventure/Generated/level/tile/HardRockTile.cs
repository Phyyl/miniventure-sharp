namespace com.mojang.ld22.level.tile;


public class HardRockTile : RockTile {
	public HardRockTile(int id) : base(id) {
		mainColor = 334; // assigns the main color 
		darkColor = 001; // assigns the dark color (for shadows)
		t = this; // assigns the tile (for rendering purposes)
	}

	/* I changed this class to be a extension of RockTile.java. So now this class is a lot shorter than it normally is. */

	/** What happens when you punch the tile */
	public override void hurt(Level level, int x, int y, Mob source, int dmg, int attackDir) {
		hurt(level, x, y, 0); // when you punch the tile it will do 0 damage.
	}

	/** What happens when you use a item on the tile. */
	public override bool interact(Level level, int xt, int yt, Player player, Item item, int attackDir) {
		if (item is ToolItem) { // if the item is a tool
			ToolItem tool = (ToolItem) item; // converts the Item into a ToolItem
			if (tool.type == ToolType.pickaxe && tool.level == 4) { // if the tool is a Gem Pickaxe params then[]
				if (player.payStamina(4 - tool.level)) { // if the player can pay the params stamina[]
					hurt(level, xt, yt, random.nextInt(10) + (tool.level) * 5 + 10); // does damage to the rock.
					return true;
				}
			}
		}
		return false;
	}
	
}
