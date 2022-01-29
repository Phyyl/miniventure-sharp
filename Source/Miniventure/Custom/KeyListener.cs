using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public abstract class KeyListener
    {
        public abstract void keyPressed(KeyEvent ke);
        public abstract void keyReleased(KeyEvent ke);
        public abstract void keyTyped(KeyEvent ke);
    }
}
