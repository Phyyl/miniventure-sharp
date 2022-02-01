using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vildmark.Maths;

namespace Miniventure.UI
{
    internal class PauseMenu : Menu
    {
        private int selectedIndex;
        private readonly MenuItem[] items = new MenuItem[]
        {
            new("Save Game") { Selected = true }
        };

        public override void Update()
        {
            base.Update();

            int delta = input.Up.Clicked ? -1 : (input.Down.Clicked ? 1 : 0);

            if (input.Attack.Clicked)
            {
                game.SaveGame();
                game.Menu = null;
                return;
            }
            
            if (input.Menu.Clicked)
            {
                game.Menu = null;
                return;
            }

            if (delta == 0)
            {
                return;
            }

            items[selectedIndex].Selected = false;

            selectedIndex = MathsHelper.Mod(selectedIndex, items.Length);

            items[selectedIndex].Selected = true;
        }

        public override void Render(Screen screen)
        {
            Font.RenderFrame(screen, "Pause", 1, 1, 12, 11);
            RenderItemList(screen, 1, 1, 12, 11, items, selectedIndex);
        }
    }

    public class MenuItem : IListItem
    {
        public string Text { get; }
        public bool Selected { get; set; }

        public MenuItem(string text)
        {
            Text = text;
        }

        public void RenderInventory(Screen screen, int x, int y)
        {
            int textColor = Selected ? Color.Get(-1, 555, 555, 555) : Color.Get(-1, 222, 222, 222);

            Font.Draw(Text, screen, x, y, textColor);
        }
    }
}
