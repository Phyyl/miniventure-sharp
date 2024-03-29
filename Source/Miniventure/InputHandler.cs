using OpenTK.Windowing.GraphicsLibraryFramework;
using Vildmark.Input;

namespace Miniventure;

public class InputHandler
{
    private readonly InputKey[] keys;

    public InputKey Up { get; } = new(Keys.Up);
    public InputKey Down { get; } = new(Keys.Down);
    public InputKey Left { get; } = new(Keys.Left);
    public InputKey Right { get; } = new(Keys.Right);
    public InputKey Attack { get; } = new(Keys.C);
    public InputKey Pause { get; } = new(Keys.Escape);
    public InputKey Menu { get; } = new(Keys.X);
    public InputKey Debug { get; } = new(Keys.F3);
    public InputKey Throw { get; } = new(Keys.Z);

    public InputHandler()
    {
        keys = new[] { Up, Down, Left, Right, Attack, Menu, Debug, Pause, Throw };

        Keyboard.OnKeyPressed += Keyboard_OnKeyPressed;
        Keyboard.OnKeyReleased += Keyboard_OnKeyReleased;
    }

    private void Keyboard_OnKeyReleased(Keys key)
    {
        foreach (var inputKey in keys)
        {
            if (inputKey.Key == key)
            {
                inputKey.Toggle(false);
            }
        }
    }

    private void Keyboard_OnKeyPressed(Keys key)
    {
        foreach (var inputKey in keys)
        {
            if (inputKey.Key == key)
            {
                inputKey.Toggle(true);
            }
        }
    }

    public void ReleaseAll()
    {
        foreach (var key in keys)
        {
            key.Down = false;
        }
    }

    public void Update()
    {
        foreach (var key in keys)
        {
            key.Update();
        }
    }

    public class InputKey
    {
        private int presses, absorbs;

        public bool Down { get; set; }
        public bool Clicked { get; set; }
        public Keys Key { get; }

        public InputKey(Keys key)
        {
            Key = key;
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

        public void Toggle(bool pressed)
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
    }
}
