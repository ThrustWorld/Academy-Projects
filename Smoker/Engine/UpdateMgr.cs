using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    static class UpdateMgr
    {
        private static List<IUpdatable> items;

        static UpdateMgr()
        {
            items = new List<IUpdatable>();
        }

        public static void AddItem(IUpdatable item)
        {
            items.Add(item);
        }

        public static void RemoveItem(IUpdatable item)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
            }
            
        }

        public static void Clear()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (int i = items.Count-1;  i >= 0 ; i--)
            {
                items[i].Update();
            }
        }
    }
}
