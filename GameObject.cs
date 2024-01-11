using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game
{
    public class GameObject
    {
        private Vector2 position;                       //Vector2 variable to store the position
        private AnimatedSprite[] animationList;         //Array type AnimatedSprite to store the animation frames
        private short health;                           //Short variable to store the health value
        private short maxHealth;                        //Short variable to store the maxHealth value
        private short stamina;                          //Short variable to store the stamina value
        private short maxStamina;                       //Short variable to store the maxStamina value
        private short strength;                         //Short variable to store the strength value
        private byte currentAnimation;                  //byte variable to store the currentAnimation
        private sbyte choice;                           //sbyte variable to store the choice

        //Getters and setters 
        public Vector2 Position { get => position; set => position = value; }
        public AnimatedSprite[] AnimationList { get => animationList; set => animationList = value; }
        public short Health { get => health; set => health = value; }
        public short MaxHealth { get => maxHealth; set => maxHealth = value; }
        public short Stamina { get => stamina; set => stamina = value; }
        public short MaxStamina { get => maxStamina; set => maxStamina = value; }
        public short Strength { get => strength; set => strength = value; }
        public byte CurrentAnimation { get => currentAnimation; set => currentAnimation = value; }
        public sbyte Choice { get => choice; set => choice = value; }

        public GameObject(Vector2 position, AnimatedSprite[] animationList, short maxHealth, short maxStamina, short strength)
        {
            this.position = position;                       //initialized variable with the position of the gameObject
            this.animationList = animationList;             //initialized variable to store the animation frames
            this.health = maxHealth;                        //initialized variable to store the health
            this.maxHealth = maxHealth;                     //initialized variable to store the maxHealth
            this.stamina = 0;                               //initialized variable to store the stamina
            this.maxStamina = maxStamina;                   //initialized variable to store the maxStamina
            this.strength = strength;                       //initialized variable to store the strength
            this.currentAnimation = (int)Animation.IDLE;    //initialized variable with the idle animation
            this.choice = -1;                               //initialized variable to store the choice
        }

        public virtual void Update(GameTime gameTime)
        {
            CheckFinishedAnimation();
            AnimatedSprite currentSprite = animationList[currentAnimation];
            currentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch screen)
        {
            AnimatedSprite currentSprite = animationList[currentAnimation];
            screen.Begin();
            screen.Draw(currentSprite.Atlas, position, currentSprite.Render(), Color.White);
            screen.End();
        }

        public void PlayAnimation(int animation)
        {
            AnimatedSprite currentSprite = animationList[currentAnimation];
            if (animation != this.currentAnimation)
                currentSprite.CurrentFrame = 0;
            else if (currentSprite.IsAnimationEnded)
            {
                currentSprite.CurrentFrame = 0;
            }
            currentAnimation = (byte)animation;
            currentSprite.IsAnimationEnded = false;
        }
        public void CheckFinishedAnimation()
        {
            AnimatedSprite currentSprite = animationList[currentAnimation];
            if(currentSprite.IsAnimationEnded && currentAnimation != (int)Animation.DEAD) 
            {
                PlayAnimation((int)Animation.IDLE);
            }
        }
    }
}
