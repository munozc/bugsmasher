using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace cocaine_smasher_420
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background, spritesheet;
        Random rand = new Random(System.Environment.TickCount);
        List<Bug> bugs = new List<Bug>();
        Sprite bar1,bar3;
        Sprite hand,progressbar,coner;
        Vector2 barProgress = new Vector2(632,23);
        int timer = 390;
        int BugCount;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 988;
            graphics.PreferredBackBufferWidth = 1680;
            
            graphics.ApplyChanges();
          
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            

            base.Initialize();
        }


        protected override void LoadContent()
        {
            
            background = Content.Load<Texture2D>("background");
            spritesheet = Content.Load<Texture2D>("spritesheet");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            hand = new Sprite(new Vector2(500,500), spritesheet, new Rectangle(137,200,40,44),new Vector2(0,0));
            progressbar = new Sprite(new Vector2(600, 0), spritesheet, new Rectangle(0, 300, 465, 80), new Vector2(0, 0));
            
            bar1 = new Sprite(new Vector2(barProgress.X, barProgress.Y), spritesheet, new Rectangle(3, 384, 25, 37), new Vector2(0, 0));
            coner = new Sprite(new Vector2(420, 420), spritesheet, new Rectangle(4, 258, 27, 35), new Vector2(0, 0));
            bar3 = new Sprite(new Vector2(barProgress.X+timer, barProgress.Y), spritesheet, new Rectangle(64, 384, 15, 37), new Vector2(0, 0));
            
            
            for (int gridx = 0; gridx < 10; gridx++)
            {
                int tempx = (gridx * 60)+rand.Next(-15,15);
                tempx -= 500;
                for (int gridY = 0; gridY < 15; gridY++)
                {
                    int tempY = ((gridY * 60) + rand.Next(-15, 15)+23);
                    
                    int bugX = rand.Next(0, 3);
                    int bugY = rand.Next(0, 2);
                    int VelY = rand.Next(-40, 40);
                    if (VelY == 10) VelY = 10;
                    Bug bug = new Bug(new Vector2(tempx, tempY), spritesheet, new Rectangle(64 * bugX, 64 * bugY, 64, 64), new Vector2(rand.Next(40, 150), VelY));
                    bugs.Add(bug);
                }

            }
            BugCount = bugs.Count;
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
          
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState ms = Mouse.GetState();
            hand.Location = new Vector2(ms.X, ms.Y);
            for (int i = 0; i < bugs.Count; i++)
            {
                if (bugs[i].IsBoxColliding(hand.BoundingBoxRect) && ms.LeftButton == ButtonState.Pressed && bugs[i].Splatted == false)
                {
                    bugs[i].Splat();
                    BugCount--;
                }
                bugs[i].Update(gameTime);
                //bugs[i].foodTarget(coner.Location.X, coner.Location.Y);
                bugs[i].mood = BugMoods.Normal;
                
                if (bugs[i].Location.X >1700) 
                {
                    bugs[i].Velocity *= new Vector2(-1, 1);
                    bugs[i].FlipHorizontal = true; 
                    bugs[i].isFirstRun = false;
                }
                if (bugs[i].Location.X < -100 && !bugs[i].isFirstRun)
                {
                    bugs[i].Velocity *= new Vector2(-1, 1);
                    bugs[i].FlipHorizontal = false;
                }

                for (int j = 0; j < bugs.Count; j++)
                {

                    if (bugs[i].IsBoxColliding(bugs[j].BoundingBoxRect))
                    {
                        if (bugs[i].Location.X < bugs[j].Location.X)
                        {

                            bugs[i].isAlive = false;
                            bugs[j].isAlive = true;
                        }
                        else
                        {
                            bugs[j].isAlive = false;
                            bugs[i].isAlive = true;
                        }
                    }
                    else bugs[i].isAlive = true;
                }
            }

            base.Update(gameTime);
            this.Window.Title = "Bugs Left: " + BugCount;
        }

        public void Method(GameTime gameTime)
        {
        //new Vector2(rand.Next(40,150),rand.Next(-40,40))
        
        
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, this.Window.ClientBounds.Width, this.Window.ClientBounds.Height), Color.White); // Draw the background at (0,0) - no crazy tinting

           
            for (int i = 0; i < bugs.Count; i++)
            {
                bugs[i].Draw(spriteBatch);
            }
              hand.Draw(spriteBatch);
              coner.Draw(spriteBatch);
              progressbar.Draw(spriteBatch);
              bar1.Draw(spriteBatch);
              bar3.Draw(spriteBatch);
            
            spriteBatch.End();
            
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null);
                spriteBatch.Draw(spritesheet, new Rectangle((int)barProgress.X + 24, (int)barProgress.Y-1, timer, 41), new Rectangle(64, 384, 16, 39), Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
