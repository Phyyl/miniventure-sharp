using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miniventure.Crafting
{
    public class ItemRecipe<TItem> : Recipe
        where TItem : Item, new()
    {
        public ItemRecipe() 
            : base(new TItem())
        {
        }

        public override void Craft(Player player)
        {
            player.inventory.Add(new TItem());
        }
    }
}
