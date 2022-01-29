namespace com.mojang.ld22.entity;


public class Slime : Mob
{
    private int xa, ya;
    private int jumpTime = 0;
    private int lvl;

    public Slime(int lvl)
        : base(lvl * lvl * 5, Random.NextInt(64 * 16), Random.NextInt(64 * 16))
    {
        this.lvl = lvl;
    }

    public override void Update()
    {
        base.Update(); // ticks the Entity.java part of this class

        int speed = 1; // the speed of the slime/ length of jump
        if (!Move(xa * speed, ya * speed) || Random.NextInt(40) == 0)
        { //moves the params slime[] doubles as a check to see if it's still moving -OR- random chance out of 40
            if (jumpTime <= -10)
            { // if jump is equal or less than ten
                xa = Random.NextInt(3) - 1; // Sets direction randomly from -1 to 1
                ya = Random.NextInt(3) - 1;

                if (Level.player != null)
                { // if player exists on the level
                    int xd = Level.player.X - X; // gets the horizontal distance between the slime and the player
                    int yd = Level.player.Y - Y; // gets the vertical distance between the slime and the player 

                    /* If the horizontal distance� + vertical distance� is smaller than 50� params then[]*/
                    if ((xd * xd) + (yd * yd) < 50 * 50)
                    { // Notch is an evil man who should be punished for this line of code, it seems to test the distance between slime and the player
                      // Why is notch evil for making that line of code? :o -David
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

                if (xa != 0 || ya != 0)
                {
                    jumpTime = 10; // if slime has it's direction, jump!
                }
            }
        }

        jumpTime--; //lower jump time by 1
        if (jumpTime == 0)
        { // when our jump has ended
            xa = ya = 0; // reset direction to 0
        }
    }

    //@Override
    public virtual void die()
    {
        base.Die(); // Parent death call

        int count = Random.NextInt(2) + 1; // Random amount of slime(item) to drop from 1 to 2
        for (int i = 0; i < count; i++)
        { // loops through the count
            Level.Add(new ItemEntity(new ResourceItem(Resource.slime), X + Random.NextInt(11) - 5, Y + Random.NextInt(11) - 5)); //creates slime items
        }

        if (Level.player != null)
        { // if player exists on my level
            Level.player.score += 25 * lvl; // add score for slime death
        }

    }

    public override void Render(Screen screen)
    {
        /* our texture in the png file */
        int xt = 0; // X tile coordinate in the sprite-sheet
        int yt = 18; // Y tile coordinate in the sprite-sheet

        /* where to draw the sprite relative to our position */
        int xo = X - 8; // the horizontal location to start drawing the sprite
        int yo = Y - 11; // the vertical location to start drawing the sprite

        if (jumpTime > 0)
        { // if jumping
            xt += 2; // change sprite
            yo -= 4; // draw sprite a little higher
        }

        int col = Color.Get(-1, 10, 252, 555); // lvl 1 colour (Green)
        if (lvl == 2)
        {
            col = Color.Get(-1, 100, 522, 555); // lvl 2 colour (Red)
        }

        if (lvl == 3)
        {
            col = Color.Get(-1, 111, 444, 555); // lvl 3 colour (Gray)
        }

        if (lvl == 4)
        {
            col = Color.Get(-1, 000, 111, 224); // lvl 4 colour (Black/Dark Gray)
        }

        if (ImmuneTime > 0)
        { // if hurt
            col = Color.Get(-1, 555, 555, 555); // make our colour white
        }

        /* Draws the sprite as 4 different 8*8 images instead of one 16*16 image, really weird, probably an artifact from the zombies and the players render code */
        /* Well, it draws the 8*8 images because the screen.render() method is stupid :) - David */
        screen.Render(xo + 0, yo + 0, xt + (yt * 32), col, 0); // draws the top-left tile
        screen.Render(xo + 8, yo + 0, xt + 1 + (yt * 32), col, 0); // draws the top-right tile
        screen.Render(xo + 0, yo + 8, xt + ((yt + 1) * 32), col, 0); // draws the bottom-left tile
        screen.Render(xo + 8, yo + 8, xt + 1 + ((yt + 1) * 32), col, 0); // draws the bottom-right tile
    }

    //@Override
    public virtual void touchedBy(Entity entity)
    {
        if (entity is Player)
        { // if we touch the player
            entity.Hurt(this, lvl, Direction); // attack
        }
    }
}