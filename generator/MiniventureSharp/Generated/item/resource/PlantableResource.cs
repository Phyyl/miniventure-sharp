namespace com.mojang.ld22.item.resource;



public class PlantableResource : Resource {
	private List<Tile> sourceTiles; // list of tiles it can be plated on
	private Tile targetTile; // what the source tile turns into when planted. (sapling/wheat seed)

	public PlantableResource(string name, int sprite, int color, Tile targetTile, params Tile[] sourceTiles1) : this(name, sprite, color, targetTile, Arrays.asList(sourceTiles1)) { // assigns everything
	}

	public PlantableResource(string name, int sprite, int color, Tile targetTile, List<Tile> sourceTiles) : base(name, sprite, color) { // assigns the name, sprite, and color.
		this.sourceTiles = sourceTiles; // assigns the source tiles
		this.targetTile = targetTile; // assigns the target tile
	}

	/** Determines what happens when the resource is used on a certain tile */
	public override bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir) {
		if (sourceTiles.contains(tile)) { // if the sourceTiles contains the called params tile[]
			level.setTile(xt, yt, targetTile, 0); // sets the source tile into the targetTile
			return true;
		}
		return false;
	}
}
