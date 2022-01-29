using OpenTK.Windowing.GraphicsLibraryFramework;
using Vildmark.Windowing;

namespace com.mojang.ld22;

public class InputHandler
{
    private readonly InputKey[] keys;

    public InputKey Up { get; }
    public InputKey Down { get; }
    public InputKey Left { get; }
    public InputKey Right { get; }
    public InputKey Attack { get; }
    public InputKey Menu { get; }

    public void ReleaseAll()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Down = false;
        }
    }

    public void Update()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Update();
        }
    }

    public InputHandler(IKeyboard keyboard)
    {
        keyboard.OnKeyPressed += Keyboard_OnKeyPressed;
        keyboard.OnKeyReleased += Keyboard_OnKeyReleased;

        keys = new[]
        {
            Up = new InputKey(),
            Down = new InputKey(),
            Left = new InputKey(),
            Right = new InputKey(),
            Attack = new InputKey(),
            Menu = new InputKey()
        };
    }

    private void Keyboard_OnKeyPressed(Keys key)
    {
        Toggle(key, true);
    }

    private void Keyboard_OnKeyReleased(Keys key)
    {
        Toggle(key, false);
    }

    private void Toggle(Keys key, bool pressed)
    {
        InputKey inputKey = key switch
        {
            Keys.KeyPad8 => Up,
            Keys.KeyPad2 => Down,
            Keys.KeyPad4 => Left,
            Keys.KeyPad6 => Right,
            Keys.W => Up,
            Keys.S => Down,
            Keys.A => Left,
            Keys.D => Right,
            Keys.Up => Up,
            Keys.Down => Down,
            Keys.Left => Left,
            Keys.Right => Right,
            Keys.Tab => Menu,
            Keys.LeftAlt => Menu,
            Keys.RightAlt => Menu,
            Keys.Menu => Menu,
            Keys.Space => Attack,
            Keys.LeftControl => Attack,
            Keys.RightControl => Attack,
            Keys.KeyPad0 => Attack,
            Keys.Insert => Attack,
            Keys.Enter => Menu,
            Keys.X => Menu,
            Keys.C => Attack,
            _ => default
        };

        if (inputKey is null)
        {
            return;
        }

        inputKey.Toggle(pressed);
    }

    public class InputKey
    {
        private int presses, absorbs;

        public bool Down { get; set; }
        public bool Clicked { get; set; }

        public virtual void Toggle(bool pressed)
        {
            if (pressed != Down)
            {
                Down = pressed;
            }
            if (pressed)
            {
                presses++;
            }
        }

        public virtual void Update()
        {
            if (absorbs < presses)
            {
                absorbs++;
                Clicked = true;
            }
            else
            {
                Clicked = false;
            }
        }
    }

}
