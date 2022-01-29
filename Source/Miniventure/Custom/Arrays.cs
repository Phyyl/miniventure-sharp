using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniventureSharp.Custom
{
    public static class Arrays
    {
        public static List<Tile> asList(Tile[] sourceTiles1)
        {
            return sourceTiles1.ToList();
        }
    }
}
