using Miniventure.Generated.gfx;

namespace com.mojang.ld22.entity;

public class Zombie : Mob
{
    private int xa, ya;
    private readonly int lvl;
    private int randomWalkTime = 0;

    public Zombie(int lvl)
        : base(lvl * lvl * 10, Random.NextInt(64 * 16), Random.NextInt(64 * 16))
    {
        this.lvl = lvl;
    }

    public override void Update()
    {
        base.Update(); // ticks the Entity.java part of this class

        if (Level.player != null && randomWalkTime == 0)
        { // checks if player is on zombies level and if there is no time left on timer
            int xd = Level.player.X - X; // gets the horizontal distance between the zombie and the player
            int yd = Level.player.Y - Y; // gets the vertical distance between the zombie and the player 
            if ((xd * xd) + (yd * yd) < 50 * 50)
            { // more evil distance checker code
                xa = 0; // sets direction to nothing
                ya = 0;
                if (xd < 0)
                {
                    xa = -1; // if the horizontal difference is smaller than 0, then the x acceleration will be 1 (negative direction)
                }

                if (xd > 0)
                {
                    xa = +1; // if the horizontal difference is larger than 0, then the x acceleration will be 1
                }

                if (yd < 0)
                {
                    ya = -1; // if the vertical difference is smaller than 0, then the y acceleration will be 1 (negative direction)
                }

                if (yd > 0)
                {
                    ya = +1; // if the vertical difference is larger than 0, then the y acceleration will be 1
                }
            }
        }

        //halp david! I have no idea what the & sign does in maths! Unless it's a bit opereator, in which case I'm rusty
        // Calm down, go google "java bitwise AND operator" for information about this. -David
        int speed = TickTime & 1; // Speed is either 0 or 1 depending on the tickTime
        if (!Move(xa * speed, ya * speed) || Random.NextInt(200) == 0)
        { //moves the zombie, doubles as a check to see if it's still moving -OR- random chance out of 200
            randomWalkTime = 60; // sets the not-so-random walk time to 60
            xa = (Random.NextInt(3) - 1) * Random.NextInt(2); //sets the acceleration to random i.e. idling code
            ya = (Random.NextInt(3) - 1) * Random.NextInt(2); //sets the acceleration to random i.e. idling code
        }
        if (randomWalkTime > 0)
        {
            randomWalkTime--;//if walk time is larger than 0, decrement!
        }
    }

    public override void Render(Screen screen)
    {
        /* our texture in the png file */
        int xt = 0; // X tile coordinate in the sprite-sheet
        int yt = 14; // Y tile coordinate in the sprite-sheet

        // change the 3 in (walkDist >> 3) to change the time it will take to switch sprites. (bigger number = longer time).
        int flip1 = (WalkDist >> 3) & 1; // This will either be a 1 or a 0 depending on the walk distance (Used for walking effect by mirroring the sprite)
        int flip2 = (WalkDist >> 3) & 1; // This will either be a 1 or a 0 depending on the walk distance (Used for walking effect by mirroring the sprite)

        if (Direction == Direction.Up)
        { 
            xt += 2; //change sprite to up
        }

        if (Direction == Direction.Left || Direction == Direction.Right)
        { 

            flip1 = 0;
            flip2 = (WalkDist >> 4) & 1; 

            if (Direction == Direction.Left)
            {
                flip1 = 1;
            }

            xt += 4 + (((WalkDist >> 3) & 1) * 2);
        }

        int xo = X - 8;
        int yo = Y - 11;

        int col = Color.Get(-1, 10, 252, 050); // lvl 1 colour green
        if (lvl == 2)
        {
            col = Color.Get(-1, 100, 522, 050); // lvl 2 colour pink
        }

        if (lvl == 3)
        {
            col = Color.Get(-1, 111, 444, 050); // lvl 3 light gray
        }

        if (lvl == 4)
        {
            col = Color.Get(-1, 000, 111, 020); // lvl 4 dark grey
        }

        if (ImmuneTime > 0)
        { // if hurt
            col = Color.Get(-1, 555, 555, 555); //make our colour white
        }

        /* Draws the sprite as 4 different 8*8 images instead of one 16*16 image */
        screen.Render(xo + (8 * flip1), yo + 0, xt + (yt * 32), col, (MirrorFlags)flip1); // draws the top-left tile
        screen.Render(xo + 8 - (8 * flip1), yo + 0, xt + 1 + (yt * 32), col, (MirrorFlags)flip1); // draws the top-right tile
        screen.Render(xo + (8 * flip2), yo + 8, xt + ((yt + 1) * 32), col, (MirrorFlags)flip2); // draws the bottom-left tile
        screen.Render(xo + 8 - (8 * flip2), yo + 8, xt + 1 + ((yt + 1) * 32), col, (MirrorFlags)flip2); // draws the bottom-right tile
    }

    public override void TouchedBy(Entity entity)
    {
        if (entity is Player)
        { // if the entity touches the player
            entity.Hurt(this, lvl + 1, Direction); // hurts the player, damage is based on lvl.
        }
    }

    public override void Die()
    {
        base.Die(); // Parent death call

        int count = Random.NextInt(2) + 1; // Random amount of cloth to drop from 1 to 2
        for (int i = 0; i < count; i++)
        { // loops through the count
            Level.Add(new ItemEntity(new ResourceItem(Resource.cloth), X + Random.NextInt(11) - 5, Y + Random.NextInt(11) - 5)); // creates cloth
        }

        if (Level.player != null)
        { // if player is on zombie level
            Level.player.score += 50 * lvl; // add score for zombie death
        }

    }

}