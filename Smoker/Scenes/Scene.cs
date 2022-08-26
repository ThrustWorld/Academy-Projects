using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto6_Smoker_
{
    abstract class Scene
    {
        public bool IsPlaying { get; protected set; }
        public Scene PreviousScene { get; set; }
        public Scene NextScene { get; set; }


        public Scene()
        {
            
        }

        public virtual void Start()
        {
            IsPlaying = true;
        }

        public abstract void Input();
        public virtual void Update()
        {

        }
        public abstract void Draw();

        public virtual Scene OnExit() 
        {
            IsPlaying = false; // redundant
            return NextScene;
        }
        

       

        

        
    }
}
