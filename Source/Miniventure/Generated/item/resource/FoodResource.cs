namespace com.mojang.ld22.item.resource;


public class FoodResource : Resource
{
    private readonly int heal; // the amount of health the food heals you for.
    private readonly int staminaCost; // the amount of stamina it costs to consume the food.

    public FoodResource(string name, int sprite, int color, int heal, int staminaCost) : base(name, sprite, color)
    { // assigns the name, sprite, and color
        this.heal = heal; // assigns the heal amount
        this.staminaCost = staminaCost; // assigns the stamina cost
    }

    /** What happens when the players uses the item on a tile */
    public override bool InteractOn(Tile tile, Level level, int xt, int yt, Player player, Direction attackDir)
    {
        if (player.Health < player.MaxHealth && player.PayStamina(staminaCost))
        { // If the player's health is less than the max health AND he can pay the stamina
            player.Heal(heal); // heal the player.
            return true; // return true
        }
        return false; // return false
    }
}
