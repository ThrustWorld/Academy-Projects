using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    static class PhysicsMgr
    {
        public enum CollisionType
        {
            Player = 1,
            Enemy = 2,
            PlayerBullet = 4,
            EnemyBullet = 8,
            PowerUp = 16
        }

        static List<RigidBody> items;

        static PhysicsMgr()
        {
            items = new List<RigidBody>();
        }

        public static void AddItem(RigidBody item)
        {
            items.Add(item);
        }

        public static void RemoveItem(RigidBody item)
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
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GameObject.IsActive)
                {
                    items[i].Update();
                }
            }
        }

        public static void CheckCollisions()
        {
            // for each item 
            for (int i = 0; i < items.Count - 1; i++)
            {
                // if item is active and is collisions affected
                if (!items[i].GameObject.IsActive || !items[i].IsCollisionsAffected)
                {
                    continue;
                }
                // for each other item1
                for (int j = i+1; j < items.Count; j++)
                {
                    // if other item is active and is collisions affected
                    if (!items[j].GameObject.IsActive || !items[j].IsCollisionsAffected)
                    {
                        continue;
                    }

                    bool checkFirst = items[i].CheckCollision((uint)items[j].CollisionType);
                    bool checkSecond = items[j].CheckCollision((uint)items[i].CollisionType);
                    // check if item and other item collides
                    if ((checkFirst || checkSecond) && items[i].Collides(items[j]))
                    {
                        // call OnCollide 
                        if (checkFirst)
                            items[i].GameObject.OnCollide(items[j].GameObject);
                        if (checkSecond)
                            items[j].GameObject.OnCollide(items[i].GameObject);
                    }
                }
            }
        }
    }
}
