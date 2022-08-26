using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    class TripleShootPowerUp : TimedPowerUp
    {
        public TripleShootPowerUp() : base("triplePowerUp",10)
        {

        }

        public override void OnAttach(Player p)
        {
            base.OnAttach(p);
            attachedPlayer.SetBulletType(BulletType.GreenGlobe);
        }

        public override void OnDetach()
        {
            attachedPlayer.SetBulletType(BulletType.BlueLaser);
            base.OnDetach();
        }
    }
}
