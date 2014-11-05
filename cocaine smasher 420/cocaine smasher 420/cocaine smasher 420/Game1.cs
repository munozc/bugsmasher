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
        int bugNum = 100;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

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

            
            for (int i = 0; i < bugNum; i++)
            {
                int bugX = rand.Next(0, 3);
                int bugY = rand.Next(0, 2);
                int Y = rand.Next(-40, 40);
                if (Y == 10) Y=10;
                Bug bug = new Bug(new Vector2 (rand.Next(-400,-50),rand.Next(50,400)), spritesheet, new Rectangle(64*bugX, 64*bugY, 64, 64), new Vector2(rand.Next(40, 150),Y));
                bugs.Add(bug);
            }
            
            
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            
            for (int i = 0; i < bugs.Count; i++)
            {
                bugs[i].Update(gameTime);
                bugs[i].mood = BugMoods.Normal;
                if (bugs[i].Location.X > this.Window.ClientBounds.Width) 
                {
                    bugs[i].Velocity *= new Vector2(-1, 1);
                    bugs[i].FlipHorizontal = true; 
                }

                for (int j = 0; j < bugs.Count; j++)
                {
                   
                    if (bugs[i].IsBoxColliding(bugs[j].BoundingBoxRect))
                    {
                        bugs[i].mood = BugMoods.Angry;
                    }
                }
            }

            base.Update(gameTime);
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
