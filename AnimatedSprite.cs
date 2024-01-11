using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game
{
    public class AnimatedSprite
    {
        private byte currentFrame = 0;                  //declare and initialise variable to control the current frame
        private float currentAnimationTime  = 0;        //declare and initialise variable to control the animation time
        private bool isAnimationEnded = false;          //bool variable to control if the animation is ended

        private Texture2D atlas;                        //variable that stores the atlas image
        private bool horizontalLoading;                 //bool variable to control the way the images are stored in the atlas
        private Rectangle firstFramePosition;           //Rectangle variable to store the frame position for the animation in the atlas
        private byte animationType;                     //byte variable to store the type of animation
        private short frameDuration;                    //short variable to store the frameDuration 
        private byte totalAnimationFrames;              //byte variable to store the total number of animation frames

        //getters and setters
        public byte CurrentFrame { get => currentFrame; set => currentFrame = value; }
        public float CurrentAnimationTime { get => currentAnimationTime; set => currentAnimationTime = value; }
        public bool IsAnimationEnded { get => isAnimationEnded; set => isAnimationEnded = value; }
        public Texture2D Atlas { get => atlas; set => atlas = value; }
        public bool HorizontalLoading { get => horizontalLoading; set => horizontalLoading = value; }
        public Rectangle FirstFramePosition { get => firstFramePosition; set => firstFramePosition = value; }
        public short FrameDuration { get => frameDuration; set => frameDuration = value; }
        public byte TotalAnimationFrames { get => totalAnimationFrames; set => totalAnimationFrames = value; }

        public AnimatedSprite(Texture2D atlas, bool horizontalLoaging, Rectangle firstFramePosition, byte animationType, short frameDuration, byte totalAnimationFrames)
        {
            this.atlas = atlas;
            this.horizontalLoading = horizontalLoaging;
            this.firstFramePosition = firstFramePosition;
            this.animationType = animationType;
            this.frameDuration = frameDuration;
            this.totalAnimationFrames = --totalAnimationFrames;
            this.currentFrame = 0;
            this.currentAnimationTime = 0;
            this.isAnimationEnded = false;
        }

        public void Update(GameTime gameTime)
        {
            if(animationType != (int)AnimationType.STATIC)              //updated the animation if it is a non-static
            {
                if (currentAnimationTime >= frameDuration)
                {
                    currentFrame++;
                    if (currentFrame > totalAnimationFrames)
                    {
                        if (animationType == (int)AnimationType.LOOP)
                            currentFrame = 0;
                        else
                            currentFrame = totalAnimationFrames;
                            isAnimationEnded = true;
                    }
                    currentAnimationTime = 0;
                }
            }
            this.currentAnimationTime += (float)gameTime.ElapsedGameTime.Milliseconds;
        }

        public Rectangle Render()
        {
            int offset = 0;
            if (horizontalLoading)          //horizontal loading applies to the images that are stored in an horizontal fashion
            {
                offset = firstFramePosition.Width * currentFrame;
                return new Rectangle(firstFramePosition.X + offset, firstFramePosition.Y, firstFramePosition.Width, firstFramePosition.Height);
            }
            else
            {                               //horizontal loading applies to the images that are stored in a vertical fashion
                offset = firstFramePosition.Height * (currentFrame);
                return new Rectangle(firstFramePosition.X, firstFramePosition.Y + offset, firstFramePosition.Width, firstFramePosition.Height);
            }
            
        }
    }
}
