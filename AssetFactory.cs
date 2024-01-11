using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game
{
    internal class AssetFactory
    {
        private Texture2D cAtlas;               //Texture2D variable to store the character atlas
        private Texture2D bAtlas;               //Texture2D variable to store the background atlas
        private Texture2D hAtlas;               //Texture2D variable to store the status bar (hud) atlas
        private Texture2D helpScreen;           //Texture2D variable to store the help screen background
        public AssetFactory(Texture2D cAtlas, Texture2D bAtlas, Texture2D hAtlas, Texture2D helpScreen)
        {
            this.cAtlas = cAtlas;                   //initialized variable to store the character atlas
            this.bAtlas = bAtlas;                   //initialized variable to store the background atlas
            this.hAtlas = hAtlas;                   //initialized variable to store the hud atlas
            this.helpScreen = helpScreen;
        }

        public AnimatedSprite[] GetAsset(int assetId)
        {
            switch (assetId)                //returns the respective animation from the respective atlas
            {
                case 1:
                    AnimatedSprite knight_idle = new AnimatedSprite(cAtlas, true, new Rectangle(0, 0, 256, 256), (byte)AnimationType.LOOP, 100, 4);
                    AnimatedSprite knight_attack = new AnimatedSprite(cAtlas, true, new Rectangle(1280, 0, 256, 256), (byte)AnimationType.ONE_TIME, 60, 5);
                    AnimatedSprite knight_damage = new AnimatedSprite(cAtlas, true, new Rectangle(256, 256, 256, 256), (byte)AnimationType.ONE_TIME,200, 2);
                    AnimatedSprite knight_dead = new AnimatedSprite(cAtlas, true, new Rectangle(2560, 0, 256, 256), (byte)AnimationType.ONE_TIME, 100, 6);
                    AnimatedSprite knight_defend = new AnimatedSprite(cAtlas, true, new Rectangle(0, 256, 256, 256), (byte)AnimationType.ONE_TIME, 800, 1);
                    return new[] { knight_idle, knight_attack, knight_defend, knight_damage, knight_dead };
                case 2:
                    AnimatedSprite samurai_idle = new AnimatedSprite(cAtlas, true, new Rectangle(768, 256, 256, 256), (byte)AnimationType.LOOP, 200, 6);
                    AnimatedSprite samurai_attack = new AnimatedSprite(cAtlas, true, new Rectangle(2304, 256, 256, 256), (byte)AnimationType.ONE_TIME, 100, 4);
                    AnimatedSprite samurai_defend = new AnimatedSprite(cAtlas, true, new Rectangle(1536, 512, 256, 256), (byte)AnimationType.ONE_TIME, 300, 2);
                    AnimatedSprite samurai_damage = new AnimatedSprite(cAtlas, true, new Rectangle(3328, 256, 256, 256), (byte)AnimationType.ONE_TIME, 300, 3);
                    AnimatedSprite samurai_dead = new AnimatedSprite(cAtlas, true, new Rectangle(0, 512, 256, 256), (byte)AnimationType.ONE_TIME, 300, 6);
                    return new[] { samurai_idle, samurai_attack, samurai_defend, samurai_damage, samurai_dead };
                case 3:
                    AnimatedSprite skeleton_idle = new AnimatedSprite(cAtlas, true, new Rectangle(2048, 512, 256, 256), (byte)AnimationType.LOOP, 100, 7);
                    AnimatedSprite skeleton_attack = new AnimatedSprite(cAtlas, true, new Rectangle(0, 768, 256, 256), (byte)AnimationType.ONE_TIME, 100, 4);
                    AnimatedSprite skeleton_defend = new AnimatedSprite(cAtlas, true, new Rectangle(1792, 768, 256, 256), (byte)AnimationType.ONE_TIME, 300, 2);
                    AnimatedSprite skeleton_damage = new AnimatedSprite(cAtlas, true, new Rectangle(1280, 768, 256, 256), (byte)AnimationType.ONE_TIME, 100, 2);
                    AnimatedSprite skeleton_dead = new AnimatedSprite(cAtlas, true, new Rectangle(1536, 768, 256, 256), (byte)AnimationType.ONE_TIME, 200, 4);
                    return new[] { skeleton_idle, skeleton_attack, skeleton_defend, skeleton_damage, skeleton_dead };
                case 4:
                    AnimatedSprite skeleton_idle2 = new AnimatedSprite(cAtlas, true, new Rectangle(0, 1024, 256, 256), (byte)AnimationType.LOOP, 100, 6);
                    AnimatedSprite skeleton_attack2 = new AnimatedSprite(cAtlas, true, new Rectangle(2560, 768, 256, 256), (byte)AnimationType.ONE_TIME,100, 6);
                    AnimatedSprite skeleton_defend2 = new AnimatedSprite(cAtlas, true, new Rectangle(1536, 1024, 256, 256), (byte)AnimationType.ONE_TIME, 200, 3);
                    AnimatedSprite skeleton_damage2 = new AnimatedSprite(cAtlas, true, new Rectangle(1536, 1024, 256, 256), (byte)AnimationType.ONE_TIME, 100, 3);
                    AnimatedSprite skeleton_dead2 = new AnimatedSprite(cAtlas, true, new Rectangle(1536, 1024, 256, 256), (byte)AnimationType.ONE_TIME, 200, 7);
                    return new[] { skeleton_idle2, skeleton_attack2, skeleton_defend2, skeleton_damage2, skeleton_dead2 };
                case 5:
                    AnimatedSprite crow_idle = new AnimatedSprite(cAtlas, true, new Rectangle(0, 1280, 256, 256), (byte)AnimationType.LOOP, 100, 6);
                    AnimatedSprite crow_attack = new AnimatedSprite(cAtlas, true, new Rectangle(1536, 1280, 256, 256), (byte)AnimationType.ONE_TIME, 100, 5);
                    AnimatedSprite crow_defend = new AnimatedSprite(cAtlas, true, new Rectangle(0, 1536, 256, 256), (byte)AnimationType.ONE_TIME, 100, 3);
                    AnimatedSprite crow_damage = new AnimatedSprite(cAtlas, true, new Rectangle(3328, 1024, 256, 256), (byte)AnimationType.ONE_TIME, 100, 3);
                    AnimatedSprite crow_dead = new AnimatedSprite(cAtlas, true, new Rectangle(0, 1536, 256, 256), (byte)AnimationType.ONE_TIME, 100, 6);
                    return new[] { crow_idle, crow_attack, crow_defend, crow_damage, crow_dead };
                case 20:
                    AnimatedSprite candle_idle = new AnimatedSprite(bAtlas, false, new Rectangle(0, 0, 960, 94), (byte)AnimationType.LOOP, 50, 8);
                    return new[] { candle_idle };
                case 21:
                    AnimatedSprite flag_attack_idle = new AnimatedSprite(hAtlas, false, new Rectangle(220, 50, 208, 68), (byte)AnimationType.LOOP, 60, 7);
                    return new[] { flag_attack_idle };
                case 22:
                    AnimatedSprite flag_defend_idle = new AnimatedSprite(hAtlas, false, new Rectangle(432, 50, 208, 68), (byte)AnimationType.LOOP, 70, 7);
                    return new[] { flag_defend_idle };
                case 23:
                    AnimatedSprite flag_wait_idle = new AnimatedSprite(hAtlas, false, new Rectangle(640, 50, 208, 68), (byte)AnimationType.LOOP, 80, 7);
                    return new[] { flag_wait_idle };
                case 24:                            //Animation for the options for the pc when attacking/deffending
                    AnimatedSprite random = new AnimatedSprite(hAtlas, true, new Rectangle(150, 0, 50, 50), (byte)AnimationType.LOOP, 80, 3);
                    return new[] { random };

                default: return null;
            }
        }

        
    }
}
