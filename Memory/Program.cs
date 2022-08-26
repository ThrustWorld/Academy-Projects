using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Draw;

namespace Progetto_1_Memory_
{
    struct Game
    {
        public int Turns;
        public Window Window;
        public Card[] Cards;
        public Random Random;
        public int CardWidth;
        public int CardHeight;
        public int Columns;
        public int Rows;
        public  float ClickDefaultCd;
        public float ClickCD;
        public float ShowCardCd;
        public float ShowCardDefaultCd;
    }

    struct Card
    {
        public Color Color;
        public bool IsCovered;
        public bool IsWon;
    }

    struct Color
    {
        public byte R;
        public byte G;
        public byte B;
    }
    class Program
    {
        static Card CreateCard(Color color)
        {
            Card card = new Card();
            card.Color = color;
            card.IsCovered = true;
            return card;
        }
        static Color CreateColor(byte r, byte g, byte b)
        {
            Color color = new Color();
            color.R = r;
            color.G = g;
            color.B = b;
            return color;
        }

        static void PutPixel(Window window, int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x >= window.width || y >= window.height)
            {
                return;
            }

            int index = (y * window.width + x) * 3;
            window.bitmap[index] = color.R;
            window.bitmap[index + 1] = color.G;
            window.bitmap[index + 2] = color.B;
        }

        static void DrawHorizontalLine(Window window, int x, int y, int width, Color color)
        {
            for (int i = x; i < x + width; i++)
            {
                PutPixel(window, i, y, color);
            }
        }

        static void DrawFillRectangle(Window window, int x, int y, int width, int height, Color color)
        {
            for (int i = y; i < y + height; i++)
            {
                DrawHorizontalLine(window, x, i, width, color);
            }
        }

        static void DrawCard(ref Game game, int index)
        {
            Card card = game.Cards[index];
            Color cardColor = CreateColor(100, 100, 100);
            if (!card.IsCovered || card.IsWon)
            {
                cardColor = card.Color;
            }
            int x = (index % game.Columns) * game.CardWidth;
            int y = (index / game.Columns) * game.CardHeight;
            DrawFillRectangle(game.Window, x, y, game.CardWidth, game.CardHeight, cardColor);
        }

        static void DrawCards(ref Game game)
        {
            for (int i = 0; i < game.Cards.Length; i++)
            {
                DrawCard(ref game, i);
            }
        }

        static void CardsShuffle(ref Game game)
        {
            for (int i = 0; i < game.Cards.Length; i++)
            {
                int randomIndex = game.Random.Next(i, game.Cards.Length);
                Card cardCopy = game.Cards[i];
                game.Cards[i] = game.Cards[randomIndex];
                game.Cards[randomIndex] = cardCopy;
            }
        }

        static int CheckClick(ref Game game)
        {
            if(game.ClickCD > 0)
            {
                game.ClickCD -= game.Window.deltaTime;
                return -1;
            }
            if (!game.Window.mouseLeft)
            {
                return -1;
            }
            game.ClickCD = game.ClickDefaultCd;
            int cellX = game.Window.mouseX / game.CardWidth;
            int cellY = game.Window.mouseY / game.CardHeight;
            int index = cellY * game.Columns + cellX;
            game.Cards[index].IsCovered = false;
            return index;
        }

        static bool ColorCompare(Color colorA, Color colorB)
        {
            if (colorA.R != colorB.R)
                return false;

            if (colorA.G != colorB.G)
                return false;

            if (colorA.B != colorB.B)
                return false;

            return true;
        }

        static bool CardCompare(ref Game game, int cardA)
        {
            for (int i = 0; i < game.Cards.Length; i++)
            {
                if (i == cardA)
                    continue;
                if (game.Cards[i].IsWon)
                    continue;
                if (game.Cards[i].IsCovered)
                    continue;

                // NOTE: the first card met, is the second one
                return ColorCompare(game.Cards[cardA].Color, game.Cards[i].Color);
            }

            return false;
        }

        static bool IsFirstCardSelected(ref Game game)
        {
            for (int i = 0; i < game.Cards.Length; i++)
            {
                if(!game.Cards[i].IsCovered && !game.Cards[i].IsWon)
                {
                    return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            
            Color red = CreateColor(255, 0, 0);
            Color blue = CreateColor(0, 0, 255);
            Color green = CreateColor(0, 255, 0);
            Color yellow = CreateColor(255, 255, 0);

            int nCards = 0;
            int columns;
            int rows;

            Console.Write("- - - - - Warning!If the cards are odd, the game will crash - - - - -");
            Console.Write("\nColumns:");
            columns = int.Parse(Console.ReadLine());
            Console.Write("\nRows:");
            rows = int.Parse(Console.ReadLine());


            Game memory = new Game();
            memory.Cards = new Card[nCards];
            memory.Random = new Random();
            memory.Columns = columns;
            memory.Rows = rows;
            memory.CardWidth = 200 / columns;
            memory.CardHeight = 400 / rows;
            memory.ClickCD = 0f;
            memory.ClickDefaultCd = 0.2f;
            memory.ShowCardCd = 0f;
            memory.ShowCardDefaultCd = 0.4f;


            /*memory.Cards[0] = CreateCard(red);
            memory.Cards[1] = CreateCard(red);
            memory.Cards[2] = CreateCard(green);
            memory.Cards[3] = CreateCard(green);
            memory.Cards[4] = CreateCard(blue);
            memory.Cards[5] = CreateCard(blue);
            memory.Cards[6] = CreateCard(yellow);
            memory.Cards[7] = CreateCard(yellow);*/

            if ((columns * rows) % 2 != 0)
            {
                return;
            }

            for (int i = 0; i <= memory.Columns * memory.Rows; i++)
            {
                memory.Cards = new Card[i];
            }

            for (int i = 0; i < memory.Cards.Length/2; i++ )
            {
                Color differentColors = CreateColor(
                               (byte)memory.Random.Next(256), 
                               (byte)memory.Random.Next(256), 
                               (byte)memory.Random.Next(256));
                memory.Cards[i] = CreateCard(differentColors);
                memory.Cards[i + memory.Cards.Length / 2] = CreateCard(differentColors);
            }

            CardsShuffle(ref memory);

            for (int i = 0; i < memory.Cards.Length; i++)
            {
                Color cardColor = memory.Cards[i].Color;
                Console.WriteLine("R: {0} G: {1} B: {2}", cardColor.R, cardColor.G, cardColor.B);
            }

            memory.Window = new Window(memory.CardWidth * memory.Columns, memory.CardHeight * memory.Rows, "Memory", PixelFormat.RGB);

            bool firstCardSelected = false;
            bool secondCardSelected = false;
            int firstCardIndex = -1;
            int index = -1;

            while (memory.Window.opened)
            {
                // if the game is paused, CheckClick() must be skipped
                if (secondCardSelected)
                {
                    if ( memory.ShowCardCd > 0)
                    {
                        memory.ShowCardCd -= memory.Window.deltaTime;
                    }
                    else
                    {
                        secondCardSelected = false;
                        firstCardSelected = false;
                        if (!memory.Cards[firstCardIndex].IsWon)
                        {
                            memory.Cards[firstCardIndex].IsCovered = true;
                            memory.Cards[index].IsCovered = true;
                        }
                    }
                }
                else
                {
                    index = CheckClick(ref memory);
                    if (index >= 0 && !memory.Cards[index].IsWon)
                    {
                        if (!firstCardSelected)
                        {
                            firstCardSelected = true;

                            firstCardIndex = index;
                        }
                        else if (index != firstCardIndex)
                        {
                            secondCardSelected = true;

                            if (ColorCompare(memory.Cards[index].Color, memory.Cards[firstCardIndex].Color))
                            {
                                memory.Cards[firstCardIndex].IsWon = true;
                                memory.Cards[index].IsWon = true;
                            }
                            else
                            {
                                memory.ShowCardCd = memory.ShowCardDefaultCd;
                                //memory.Cards[firstCardIndex].IsCovered = true;
                                //memory.Cards[index].IsCovered = true;
                            }
                            firstCardSelected = false;
                            memory.Turns++;
                            Console.WriteLine("Turn:" + memory.Turns);
                        }
                    }
                }
                DrawCards(ref memory);
                memory.Window.Blit();
            }
        }
    }
}
