using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto4_SpaceShooter_
{

    class TitleScene : Scene
    {
        protected Sprite img;
        protected Texture imgTexture;

        //buttons
        protected float buttonPressWait;
        protected KeyCode nextSceneKey;

        //fade
        protected float counter;
        protected float counterDirection;
        protected float colorMul;

        public bool FadeIn { get; set; }
        public bool FadeOut { get; set; }
        public float FadeTime { get; set; }
        public float ShowTime { get; set; }//if 0 -> infinite


        public TitleScene(string texture, KeyCode keyNext)
        {
            FadeIn = true;
            FadeOut = true;
            FadeTime = 2.0f;
            ShowTime = 0;//infinite
            counterDirection = 0;
            nextSceneKey = keyNext;
            colorMul = 1;
            buttonPressWait = 0.0f;

            imgTexture = new Texture(texture);
            img = new Sprite(Game.Window.Width, Game.Window.Height);
        }

        protected void GoFadeIn()
        {
            counterDirection = 1;
            counter = 0;
        }

        protected void GoFadeOut()
        {
            counterDirection = -1;
            counter = FadeTime;
        }

        public override void Start()
        {
            base.Start();
            if (FadeIn)
            {
                GoFadeIn();
                //counter = 0;
                //counterDirection = 1;
            }
            else
            {
                counterDirection = 0;
                counter = ShowTime;
                colorMul = 1;
            }
        }

        public override void Draw()
        {
            img.DrawTexture(imgTexture);
        }

        public override void Input()
        {
            if (Game.Window.GetKey(nextSceneKey) && buttonPressWait <= 0)
            {
                if (FadeOut)
                {
                    if (counterDirection != -1)//if fade in or show
                    {//go to fade out
                        GoFadeOut();
                    }
                }
                else
                {
                    IsPlaying = false;
                }
            }
        }

        public override void Update()
        {
            buttonPressWait -= Game.DeltaTime;
            if (counterDirection != 0)//Fade in or Fade out
            {
                //inc/dec counter
                counter += Game.DeltaTime * counterDirection;

                if (counterDirection > 0)
                {//Fade in
                    if (counter >= FadeTime)
                    {
                        //fade in end
                        counter = FadeTime;
                        counterDirection = 0;
                    }
                }
                else
                {//Fade out
                    if (counter <= 0)
                    {
                        //fade out end
                        counter = 0;
                        IsPlaying = false;
                    }
                }

                colorMul = counter / FadeTime;

                if (counterDirection == 0)
                {//show image (fade in end)
                    counter = ShowTime;
                }

                img.SetMultiplyTint(colorMul, colorMul, colorMul, 1);
            }
            else
            {//Show image
                if (ShowTime != 0)
                {
                    counter -= Game.DeltaTime;
                    if (counter <= 0)
                    {
                        if (FadeOut)
                        {
                            GoFadeOut();
                        }
                        else
                        {
                            IsPlaying = false;
                        }
                    }
                }
            }


        }
    }
}
