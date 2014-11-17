using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace cocaine_smasher_420
{
    enum BugMoods
    {
        Timid,
        Angry,
        Normal
    }


    class Bug : Sprite
    {
        public BugMoods mood = BugMoods.Normal;
        private Random rand = new Random((int)DateTime.UtcNow.Ticks);
        float timeRemaining = 0.0f;
        float TimePerNewTarget = 2.0f;
        public Boolean isFirstRun = true;
        public Boolean isAlive = true;
        public Boolean Splatted = false;
        public Boolean manualTarget = false;
       
        public Bug(
           Vector2 location,
           Texture2D texture,
           Rectangle initialFrame,
           Vector2 velocity)
            : base(location, texture, initialFrame, velocity)
        {
            System.Threading.Thread.Sleep(1);
        }

        public override void Update(GameTime gameTime)
        {
            if (isAlive && !Splatted)
            {

                if (timeRemaining == 0.0f && !manualTarget)
                {
                    NewTarget();
                    timeRemaining = TimePerNewTarget;
                }
                

                timeRemaining = MathHelper.Max(0, timeRemaining -
               (float)gameTime.ElapsedGameTime.TotalSeconds);
                base.Update(gameTime);
            }
        }
        public void NewTarget()
        {
            Vector2 target;
            if (Velocity.X > 0)
                //target = new Vector2(Location.X + 400, Location.Y + rand.Next(-150, 150));
                target = new Vector2(Location.X + 400, rand.Next(0, 1680));
            else
            {
                target = new Vector2(Location.X - 400, Location.Y + rand.Next(-150, 150));
                this.FlipHorizontal = false;
            }
            Vector2 vel = target - Location;
            vel.Normalize();
            vel *= 600;
            Velocity = vel;
            Rotation = (float)Math.Atan2(vel.Y, vel.X);
        }
        public void foodTarget(float x, float y)
        {
            Vector2  target = new Vector2(x, y);
            Vector2 vel = target - Location;
            vel.Normalize();
            vel *= 610;
            Velocity = vel;
            Rotation = (float)Math.Atan2(vel.Y, vel.X);
        }
        public void Splat()
        {
            
            Splatted = true;
            this.frames[0] = new Rectangle(0, 150, 100, 100);
           
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            /*
            if (mood == BugMoods.Angry)
            {
                this.TintColor = Color.Red;
                this.Velocity *= new Vector2(1.1f, 1f);

                if (Velocity.Length() > 150)
                {
                    this.velocity.Normalize();
                    this.velocity *= 150;
                }
            }
            else
            {
                this.TintColor = Color.White;
            }
            */
            base.Draw(spriteBatch);
        }
    }
}
