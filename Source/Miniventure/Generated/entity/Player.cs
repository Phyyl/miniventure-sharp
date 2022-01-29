using Miniventure.Generated.gfx;

namespace com.mojang.ld22.entity;

public class Player : Mob
{
    private InputHandler input;
    private int attackTime;
    private Direction attackDir;

    public Game game;
    public Inventory inventory = new Inventory();
    public Item attackItem;
    public Item activeItem;
    public int stamina;
    public int staminaRecharge;
    public int staminaRechargeDelay;
    public int score;
    public int maxStamina = 10;
    private int onStairDelay;
    public int invulnerableTime = 0;


    public Player(Game game, InputHandler input)
    {
        this.game = game;
        this.input = input;
        X = 24;
        Y = 24;
        stamina = maxStamina;

        inventory.Add(new FurnitureItem(new Workbench()));
        inventory.Add(new PowerGloveItem());
    }

    public override void Update()
    {
        base.Update();

        if (invulnerableTime > 0)
        {
            invulnerableTime--;
        }

        Tile onTile = Level.GetTile(X >> 4, Y >> 4);
        if (onTile == Tile.stairsDown || onTile == Tile.stairsUp)
        {
            if (onStairDelay == 0)
            {
                changeLevel((onTile == Tile.stairsUp) ? 1 : -1);
                onStairDelay = 10;
                return;
            }
            onStairDelay = 10;
        }
        else
        {
            if (onStairDelay > 0)
            {
                onStairDelay--;
            }
        }

        if (stamina <= 0 && staminaRechargeDelay == 0 && staminaRecharge == 0)
        {
            staminaRechargeDelay = 40;
        }

        if (staminaRechargeDelay > 0)
        {
            staminaRechargeDelay--;
        }

        if (staminaRechargeDelay == 0)
        {
            staminaRecharge++;
            if (IsSwimming())
            {
                staminaRecharge = 0;
            }
            while (staminaRecharge > 10)
            {
                staminaRecharge -= 10;
                if (stamina < maxStamina)
                {
                    stamina++;
                }
            }
        }

        int xa = 0;
        int ya = 0;

        if (input.up.down)
        {
            ya--;
        }

        if (input.down.down)
        {
            ya++;
        }

        if (input.left.down)
        {
            xa--;
        }

        if (input.right.down)
        {
            xa++;
        }

        if (IsSwimming() && TickTime % 60 == 0)
        {
            if (stamina > 0)
            {
                stamina--;
            }
            else
            {
                Hurt(this, 1, Direction.GetOpposite());
            }
        }

        if (staminaRechargeDelay % 2 == 0)
        {
            Move(xa, ya);
        }

        if (input.attack.clicked)
        {
            if (stamina == 0)
            {

            }
            else
            {
                stamina--;
                staminaRecharge = 0;
                Attack();
            }
        }

        if (input.menu.clicked)
        {
            if (!Use())
            {
                game.Menu = new InventoryMenu(this);
            }
        }

        if (attackTime > 0)
        {
            attackTime--;
        }
    }

    private bool Use()
    {
        if (Direction == Direction.Down && Use(X - 8, Y + 4 - 2, X + 8, Y + 12 - 2))
        {
            return true;
        }

        if (Direction == Direction.Up && Use(X - 8, Y - 12 - 2, X + 8, Y - 4 - 2))
        {
            return true;
        }

        if (Direction == Direction.Left && Use(X - 12, Y - 8 - 2, X - 4, Y + 8 - 2))
        {
            return true;
        }

        if (Direction == Direction.Right && Use(X + 4, Y - 8 - 2, X + 12, Y + 8 - 2))
        {
            return true;
        }

        return false;
    }

    private void Attack()
    {
        WalkDist += 8;
        attackDir = Direction;
        attackItem = activeItem;
        bool done = false;

        if (activeItem != null)
        {
            attackTime = 10;
            int yo = -2;
            int range = 12;

            if (Direction == Direction.Down && Interact(X - 8, Y + 4 + yo, X + 8, Y + range + yo))
            {
                done = true;
            }

            if (Direction == Direction.Up && Interact(X - 8, Y - range + yo, X + 8, Y - 4 + yo))
            {
                done = true;
            }

            if (Direction == Direction.Left && Interact(X - range, Y - 8 + yo, X - 4, Y + 8 + yo))
            {
                done = true;
            }

            if (Direction == Direction.Right && Interact(X + 4, Y - 8 + yo, X + range, Y + 8 + yo))
            {
                done = true;
            }

            if (done)
            {
                return;
            }

            int xt = X >> 4;
            int yt = (Y + yo) >> 4;
            int r = 12;

            if (attackDir == Direction.Down)
            {
                yt = (Y + r + yo) >> 4;
            }

            if (attackDir == Direction.Up)
            {
                yt = (Y - r + yo) >> 4;
            }

            if (attackDir == Direction.Left)
            {
                xt = (X - r) >> 4;
            }

            if (attackDir == Direction.Right)
            {
                xt = (X + r) >> 4;
            }

            if (xt >= 0 && yt >= 0 && xt < Level.Width && yt < Level.Height)
            {
                if (activeItem.InteractOn(Level.GetTile(xt, yt), Level, xt, yt, this, attackDir))
                {
                    done = true;
                }
                else
                {
                    if (Level.GetTile(xt, yt).Interact(Level, xt, yt, this, activeItem, attackDir))
                    {
                        done = true;
                    }
                }
                if (activeItem.IsDepleted())
                {
                    activeItem = null;
                }
            }
        }

        if (done)
        {
            return;
        }

        if (activeItem == null || activeItem.CanAttack())
        {
            attackTime = 5;
            int yo = -2;
            int range = 20;
            if (Direction == Direction.Down)
            {
                Hurt(X - 8, Y + 4 + yo, X + 8, Y + range + yo);
            }

            if (Direction == Direction.Up)
            {
                Hurt(X - 8, Y - range + yo, X + 8, Y - 4 + yo);
            }

            if (Direction == Direction.Left)
            {
                Hurt(X - range, Y - 8 + yo, X - 4, Y + 8 + yo);
            }

            if (Direction == Direction.Right)
            {
                Hurt(X + 4, Y - 8 + yo, X + range, Y + 8 + yo);
            }

            int xt = X >> 4;
            int yt = (Y + yo) >> 4;
            int r = 12;

            if (attackDir == Direction.Down)
            {
                yt = (Y + r + yo) >> 4;
            }

            if (attackDir == Direction.Up)
            {
                yt = (Y - r + yo) >> 4;
            }

            if (attackDir == Direction.Left)
            {
                xt = (X - r) >> 4;
            }

            if (attackDir == Direction.Right)
            {
                xt = (X + r) >> 4;
            }

            if (xt >= 0 && yt >= 0 && xt < Level.Width && yt < Level.Height)
            {
                Level.GetTile(xt, yt).Hurt(Level, xt, yt, this, Random.NextInt(3) + 1, attackDir);
            }
        }

    }

    private bool Use(int x0, int y0, int x1, int y1)
    {
        foreach (var entity in Level.GetEntities(x0, y0, x1, y1))
        {
            if (entity != this)
            {
                if (entity.Use(this, attackDir))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool Interact(int x0, int y0, int x1, int y1)
    {
        foreach (var entity in Level.GetEntities(x0, y0, x1, y1))
        {
            if (entity != this)
            {
                if (entity.Interact(this, activeItem, attackDir))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Hurt(int x0, int y0, int x1, int y1)
    {
        foreach (var entity in Level.GetEntities(x0, y0, x1, y1))
        {
            if (entity != this)
            {
                entity.Hurt(this, GetAttackDamage(entity), attackDir);
            }
        }
    }

    private int GetAttackDamage(Entity e)
    {
        int dmg = Random.NextInt(3) + 1;

        if (attackItem != null)
        {
            dmg += attackItem.GetAttackDamageBonus(e);
        }

        return dmg;
    }

    /** Draws the player on the screen */
    public override void Render(Screen screen)
    {
        int xt = 0;
        int yt = 14;

        int flip1 = (WalkDist >> 3) & 1;
        int flip2 = (WalkDist >> 3) & 1;

        if (Direction == Direction.Up)
        {
            xt += 2;
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

        if (IsSwimming())
        {
            yo += 4;


            int waterColor = Color.Get(-1, -1, 115, 335);

            if (TickTime / 8 % 2 == 0)
            {
                waterColor = Color.Get(-1, 335, 5, 115);
            }

            screen.Render(xo + 0, yo + 3, 5 + (13 * 32), waterColor, MirrorFlags.None);
            screen.Render(xo + 8, yo + 3, 5 + (13 * 32), waterColor, MirrorFlags.Horizontal);
        }

        if (attackTime > 0 && attackDir == Direction.Up)
        {
            screen.Render(xo + 0, yo - 4, 6 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.None);
            screen.Render(xo + 8, yo - 4, 6 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.Horizontal);

            if (attackItem != null)
            {
                attackItem.RenderIcon(screen, xo + 4, yo - 4);
            }
        }

        int col = Color.Get(-1, 100, 220, 532);

        if (ImmuneTime > 0)
        {
            col = Color.Get(-1, 555, 555, 555);
        }

        if (activeItem is FurnitureItem)
        {
            yt += 2;
        }

        screen.Render(xo + (8 * flip1), yo + 0, xt + (yt * 32), col, (MirrorFlags)flip1);
        screen.Render(xo + 8 - (8 * flip1), yo + 0, xt + 1 + (yt * 32), col, (MirrorFlags)flip1);

        if (!IsSwimming())
        {
            screen.Render(xo + (8 * flip2), yo + 8, xt + ((yt + 1) * 32), col, (MirrorFlags)flip2);
            screen.Render(xo + 8 - (8 * flip2), yo + 8, xt + 1 + ((yt + 1) * 32), col, (MirrorFlags)flip2);
        }

        if (attackTime > 0 && attackDir == Direction.Left)
        {
            screen.Render(xo - 4, yo, 7 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.Horizontal);
            screen.Render(xo - 4, yo + 8, 7 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.Both);

            if (attackItem != null)
            {
                attackItem.RenderIcon(screen, xo - 4, yo + 4);
            }
        }
        if (attackTime > 0 && attackDir == Direction.Right)
        {
            screen.Render(xo + 8 + 4, yo, 7 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.None);
            screen.Render(xo + 8 + 4, yo + 8, 7 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.Vertical);
            if (attackItem != null)
            {
                attackItem.RenderIcon(screen, xo + 8 + 4, yo + 4);
            }
        }

        if (attackTime > 0 && attackDir == Direction.Left)
        {
            screen.Render(xo + 0, yo + 8 + 4, 6 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.Vertical);
            screen.Render(xo + 8, yo + 8 + 4, 6 + (13 * 32), Color.Get(-1, 555, 555, 555), MirrorFlags.Both);
            if (attackItem != null)
            {
                attackItem.RenderIcon(screen, xo + 4, yo + 8 + 4);
            }
        }

        if (activeItem is FurnitureItem furnitureItem)
        {
            Furniture furniture = furnitureItem.furniture;
            furniture.X = X; //?
            furniture.Y = yo;
            furniture.Render(screen);

        }
    }

    public override void TouchItem(ItemEntity itemEntity)
    {
        itemEntity.Take(this);

        inventory.Add(itemEntity.item);
    }

    /** Returns if the entity can swim */
    public override bool CanSwim()
    {
        return true;
    }

    public override bool TrySpawn(Level level)
    {

        while (true)
        {
            int x = Random.NextInt(level.Width);
            int y = Random.NextInt(level.Height);

            if (level.GetTile(x, y) == Tile.grass)
            {
                X = (x * 16) + 8;
                Y = (y * 16) + 8;

                return true;
            }
        }
    }

    /** Pays the stamina used for an action */
    public virtual bool payStamina(int cost)
    {
        if (cost > stamina)
        {
            return false;
        }

        stamina -= cost;
        return true;
    }

    /** Changes the level */
    public virtual void changeLevel(int dir)
    {
        game.ScheduleLevelChange(dir);
    }

    /** Gets the player's light radius underground */
    public override int GetLightRadius()
    {
        int r = 2;
        if (activeItem != null)
        {
            if (activeItem is FurnitureItem)
            {
                int rr = ((FurnitureItem)activeItem).furniture.GetLightRadius();
                if (rr > r)
                {
                    r = rr;
                }
            }
        }
        return r;
    }

    /** What happens when the player dies */

    public virtual void die()
    {
        base.Die();
        Sound.playerDeath.Play();
    }

    /** What happens when the player touches an entity */

    public virtual void touchedBy(Entity entity)
    {
        if (!(entity is Player))
        {
            entity.TouchedBy(this);
        }
    }

    /** What happens when the player is hurt */

    public virtual void doHurt(int damage, int attackDir)
    {
        if (ImmuneTime > 0 || invulnerableTime > 0)
        {
            return;
        }

        Sound.playerHurt.Play();
        Level.Add(new TextParticle("" + damage, X, Y, Color.Get(-1, 504, 504, 504)));
        Health -= damage;
        if (attackDir == 0)
        {
            YKnockback = +6;
        }

        if (attackDir == 1)
        {
            YKnockback = -6;
        }

        if (attackDir == 2)
        {
            XKnockback = -6;
        }

        if (attackDir == 3)
        {
            XKnockback = +6;
        }

        ImmuneTime = 10;
        invulnerableTime = 30;
    }

    /** What happens when the player wins */
    public virtual void gameWon()
    {
        Level.player.invulnerableTime = 60 * 5;
        game.won();
    }
}