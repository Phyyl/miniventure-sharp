using Vildmark.Serialization;

namespace Miniventure.Entities;

public class Player : Mob
{
    public const int maxStamina = 10;

    private int onStairDelay;
    private int attackTime;
    private Direction attackDir;

    public int stamina;
    public int staminaRecharge;
    public int staminaRechargeDelay;
    public int score;
    public int invulnerableTime = 0;

    public Inventory inventory = new();
    public Item attackItem;
    public Item activeItem;

    public int PickupRange => activeItem is PowerGloveItem ? 24 : 12;

    public Player()
        : base(10)
    {
        X = 24;
        Y = 24;

        stamina = maxStamina;

        inventory.Add(new FurnitureItem(new Workbench()));
        inventory.Add(new PowerGloveItem());
    }

    public override void Serialize(IWriter writer)
    {
        base.Serialize(writer);

        writer.WriteValue(onStairDelay);
        writer.WriteValue(attackTime);
        writer.WriteValue(attackDir);
        writer.WriteValue(stamina);
        writer.WriteValue(staminaRecharge);
        writer.WriteValue(staminaRechargeDelay);
        writer.WriteValue(score);
        writer.WriteValue(invulnerableTime);

        writer.WriteObject(inventory);
        writer.WriteObject(attackItem, true);
        writer.WriteObject(activeItem, true);
    }

    public override void Deserialize(IReader reader)
    {
        base.Deserialize(reader);

        onStairDelay = reader.ReadValue<int>();
        attackTime = reader.ReadValue<int>();
        attackDir = reader.ReadValue<Direction>();
        stamina = reader.ReadValue<int>();
        staminaRecharge = reader.ReadValue<int>();
        staminaRechargeDelay = reader.ReadValue<int>();
        score = reader.ReadValue<int>();
        invulnerableTime = reader.ReadValue<int>();

        inventory = reader.ReadObject<Inventory>();
        attackItem = reader.ReadObject<Item>(true);
        activeItem = reader.ReadObject<Item>(true);
    }

    public void Take(Furniture furniture)
    {
        if (activeItem is not null)
        {
            furniture.Remove();
            inventory.Add(0, activeItem);
            activeItem = new FurnitureItem(furniture);
        }
    }

    public override void Update()
    {
        base.Update();

        if (invulnerableTime > 0)
        {
            invulnerableTime--;
        }

        Tile onTile = Level.GetTile(X >> 4, Y >> 4);
        if (onTile == Tile.StairsDown || onTile == Tile.StairsUp)
        {
            if (onStairDelay == 0)
            {
                ChangeLevel(onTile == Tile.StairsUp ? 1 : -1);
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

        if (Game.Instance.Input.Up.Down)
        {
            ya--;
        }

        if (Game.Instance.Input.Down.Down)
        {
            ya++;
        }

        if (Game.Instance.Input.Left.Down)
        {
            xa--;
        }

        if (Game.Instance.Input.Right.Down)
        {
            xa++;
        }

        if (Game.Instance.Input.Throw.Clicked)
        {
            if (activeItem is not null)
            {
                (double xxa, double yya) = Direction switch
                {
                    Direction.Down => (0, 1),
                    Direction.Up => (0, -1),
                    Direction.Left => (-1, 0),
                    _ => (1, 0)
                };

                Level.Add(new ItemEntity(activeItem, X, Y, xxa, yya, 60 * 60));
                activeItem = null;
            }
        }

        if (IsSwimming() && TickTime % 60 == 0)
        {
            if (stamina > 0)
            {
                stamina--;
            }
            else
            {
                Hurt(1, Direction.GetOpposite());
            }
        }

        if (staminaRechargeDelay % 2 == 0)
        {
            Move(xa, ya);
        }

        if (Game.Instance.Input.Attack.Clicked)
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

        int range = PickupRange;

        bool InPickupRange(Entity entity)
        {
            int dx = entity.X - X;
            int dy = entity.Y - Y;

            return (dx * dx + dy * dy) < range * range;
        }

        foreach (var entity in Level.GetEntities(X - range - 16, Y - range - 16, X + range + 16, Y + range + 16).OfType<ItemEntity>().Where(e => e.CanPickup && InPickupRange(e)))
        {
            entity.Take(this);

            inventory.Add(entity.Item);
        }

        if (Game.Instance.Input.Menu.Clicked)
        {
            if (!Use())
            {
                Game.Instance.Menu = new InventoryMenu(this);
            }
        }

        if (Game.Instance.Input.Pause.Clicked)
        {
            Game.Instance.Menu = new PauseMenu();
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
            int yt = Y + yo >> 4;
            int r = 12;

            if (attackDir == Direction.Down)
            {
                yt = Y + r + yo >> 4;
            }

            if (attackDir == Direction.Up)
            {
                yt = Y - r + yo >> 4;
            }

            if (attackDir == Direction.Left)
            {
                xt = X - r >> 4;
            }

            if (attackDir == Direction.Right)
            {
                xt = X + r >> 4;
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
            int yt = Y + yo >> 4;
            int r = 12;

            if (attackDir == Direction.Down)
            {
                yt = Y + r + yo >> 4;
            }

            if (attackDir == Direction.Up)
            {
                yt = Y - r + yo >> 4;
            }

            if (attackDir == Direction.Left)
            {
                xt = X - r >> 4;
            }

            if (attackDir == Direction.Right)
            {
                xt = X + r >> 4;
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
                entity.Hurt(GetAttackDamage(entity), attackDir);
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

    public override void Render(Screen screen)
    {
        int xt = 0;
        int yt = 14;

        int flip1 = WalkDist >> 3 & 1;
        int flip2 = WalkDist >> 3 & 1;

        if (Direction == Direction.Up)
        {
            xt += 2;
        }

        if (Direction == Direction.Left || Direction == Direction.Right)
        {
            flip1 = 0;
            flip2 = WalkDist >> 4 & 1;

            if (Direction == Direction.Left)
            {
                flip1 = 1;
            }

            xt += 4 + (WalkDist >> 3 & 1) * 2;
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

            screen.Render(xo + 0, yo + 3, 5 + 13 * 32, waterColor, MirrorFlags.None);
            screen.Render(xo + 8, yo + 3, 5 + 13 * 32, waterColor, MirrorFlags.Horizontal);
        }

        if (attackTime > 0 && attackDir == Direction.Up)
        {
            screen.Render(xo + 0, yo - 4, 6 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.None);
            screen.Render(xo + 8, yo - 4, 6 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.Horizontal);

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

        screen.Render(xo + 8 * flip1, yo + 0, xt + yt * 32, col, (MirrorFlags)flip1);
        screen.Render(xo + 8 - 8 * flip1, yo + 0, xt + 1 + yt * 32, col, (MirrorFlags)flip1);

        if (!IsSwimming())
        {
            screen.Render(xo + 8 * flip2, yo + 8, xt + (yt + 1) * 32, col, (MirrorFlags)flip2);
            screen.Render(xo + 8 - 8 * flip2, yo + 8, xt + 1 + (yt + 1) * 32, col, (MirrorFlags)flip2);
        }

        if (attackTime > 0 && attackDir == Direction.Left)
        {
            screen.Render(xo - 4, yo, 7 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.Horizontal);
            screen.Render(xo - 4, yo + 8, 7 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.Both);

            if (attackItem != null)
            {
                attackItem.RenderIcon(screen, xo - 4, yo + 4);
            }
        }
        if (attackTime > 0 && attackDir == Direction.Right)
        {
            screen.Render(xo + 8 + 4, yo, 7 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.None);
            screen.Render(xo + 8 + 4, yo + 8, 7 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.Vertical);
            if (attackItem != null)
            {
                attackItem.RenderIcon(screen, xo + 8 + 4, yo + 4);
            }
        }

        if (attackTime > 0 && attackDir == Direction.Down)
        {
            screen.Render(xo + 0, yo + 8 + 4, 6 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.Vertical);
            screen.Render(xo + 8, yo + 8 + 4, 6 + 13 * 32, Color.Get(-1, 555, 555, 555), MirrorFlags.Both);
            if (attackItem != null)
            {
                attackItem.RenderIcon(screen, xo + 4, yo + 8 + 4);
            }
        }

        if (activeItem is FurnitureItem furnitureItem)
        {
            Furniture furniture = furnitureItem.furniture;
            furniture.X = X;
            furniture.Y = yo;
            furniture.Render(screen);

        }
    }

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

            if (level.GetTile(x, y) == Tile.Grass)
            {
                X = x * 16 + 8;
                Y = y * 16 + 8;

                return true;
            }
        }
    }

    public bool PayStamina(int cost)
    {
        if (cost > stamina)
        {
            return false;
        }

        stamina -= cost;
        return true;
    }

    public void ChangeLevel(int dir)
    {
        Game.Instance.ScheduleLevelChange(dir);
    }

    public override int GetLightRadius()
    {
        int r = 2;
        if (activeItem != null)
        {
            if (activeItem is FurnitureItem item1)
            {
                int rr = item1.furniture.GetLightRadius();
                if (rr > r)
                {
                    r = rr;
                }
            }
        }
        return r;
    }

    public override void Die()
    {
        base.Die();
        AudioTracks.PlayerDeath.Play();
    }

    public override void TouchedBy(Entity entity)
    {
        if (!(entity is Player))
        {
            entity.TouchedBy(this);
        }
    }

    public override void DoHurt(int damage, Direction attackDir)
    {
        if (ImmuneTime > 0 || invulnerableTime > 0)
        {
            return;
        }

        AudioTracks.PlayerHurt.Play();
        Level.Add(new TextParticle("" + damage, X, Y, Color.Get(-1, 504, 504, 504)));
        Health -= damage;
        if (attackDir == Direction.Down)
        {
            YKnockback = 6;
        }

        if (attackDir == Direction.Up)
        {
            YKnockback = -6;
        }

        if (attackDir == Direction.Left)
        {
            XKnockback = -6;
        }

        if (attackDir == Direction.Right)
        {
            XKnockback = 6;
        }

        ImmuneTime = 10;
        invulnerableTime = 30;
    }

    public void GameWon()
    {
        Level.Player.invulnerableTime = 60 * 5;
        Game.Instance.Won();
    }
}