using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{
    class EnergyPowerUp : PowerUp
    {
        public EnergyPowerUp() : base("nrgPowerUp")
        {
            
        }
        public override void OnAttach(Player p)
        {
            base.OnAttach(p);
            attachedPlayer.ResetEnergy();
            OnDetach();
        }
    }
}
