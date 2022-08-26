using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    abstract class TimedPowerUp : PowerUp
    {
        protected float counter;
        protected float duration;

        public TimedPowerUp(string textureName, float duration) : base(textureName)
        {
            this.duration = duration;
            counter = duration;

        }

        public override void Reset()
        {
            base.Reset();
            counter = duration;
        }

        public override void Update()
        {

            base.Update();
            if (attachedPlayer!=null)//powerup is attached
            {
                counter -= Game.DeltaTime;
                if (counter <= 0)
                {
                    OnDetach();
                }
            }

        }

        public override void OnAttach(Player p)
        {
            base.OnAttach(p);
            IsActive = false;
        }
    }
}
