namespace com.mojang.ld22.item;


public class Item : ListItem {
	
	/* Note: Most of the stuff in the class is expanded upon in ResourceItem/PowerGloveItem/FurnitureItem/etc */
	
	/** Gets the color of the item */
	public virtual int getColor() {
		return 0;
	}

	/** Gets the sprite of the item */
	public virtual int getSprite() {
		return 0;
	}

	/** What happens when you pick up the item off the ground */
	public virtual void onTake(ItemEntity itemEntity) {
	}

	/** Renders an item (sprite & name) in an inventory */
	public virtual void renderInventory(Screen screen, int x, int y) {
	}

	/** Determines what happens when the player interacts with a entity */
	public virtual bool interact(Player player, Entity entity, int attackDir) {
		return false;
	}

	/** Renders the icon of the Item */
	public virtual void renderIcon(Screen screen, int x, int y) {
	}

	/** Determines what happens when you use a item in a tile */
	public virtual bool interactOn(Tile tile, Level level, int xt, int yt, Player player, int attackDir) {
		return false;
	}
	
	/** Returns if the item is depleted or not */
	public virtual bool isDepleted() {
		return false;
	}

	/** Returns if the item can attack mobs or not */
	public virtual bool canAttack() {
		return false;
	}

	/** Gets the attack bonus from an item/tool (sword/axe) */
	public virtual int getAttackDamageBonus(Entity e) {
		return 0;
	}

	/** Gets the name of the tool */
	public virtual string getName() {
		return "";
	}

	/** Sees if an item matches another item */
	public virtual bool matches(Item item) {
		return item.getClass() == this.getClass();
	}
}