using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace RPG_game
{
    public class RPGGame : Game
    {
        private GraphicsDeviceManager graphics;                 //declare GraphicsDeviceManager to define the graphics device 
        private SpriteBatch screen;                             //SpriteBatch variable to access the screen to draw
        private Texture2D sAtlas;                               //Texture2D variable to store the screen atlas
        private Texture2D cAtlas;                               //Texture2D variable to store the character atlas
        private Texture2D bAtlas;                               //Texture2D variable to store the background atlas
        private Texture2D hAtlas;                               //Texture2D variable to store the hud atlas
        private Texture2D helpScreen;                           //Texture2D variable to store the help screen
        private SpriteFont font;                                //SpriteFont variable to store the font
        private SpriteFont fontSmall;                           //SpriteFont variable to store the font
        private FileManager file;                               //FileManager variable to store the file name
        //Game logic
        private byte gameState;                                 //byte variable to store the gameState                            
        private byte round;                                     //byte variable to store the round  
        private bool character;                                 //bool variable to control the character used
        private short waitTime;                                 //short variable to store and control the waitTime
        private short score;                                    //short variable to store the score
        private bool battleResult;                              //bool variable to control the battleResult
        private bool usePotion;                                 //bool variable to control the use of potion
        private byte potions;                                   //byte variable to control the number of potions available
        // Animated GameObjects
        private GameObject hero;                                //GameObject variable to store the hero gameObject
        private GameObject foe;                                 //GameObject variable to store the foe gameObject
        private GameObject[] enemies;                           //array type GameObject to store the enemies gameObjects
        private GameObject[] warriors;                          //array type GameObject to store the heroes gameObjects
        private GameObject candles;                             //GameObject variable to store the candles gameObject
        private GameObject flagAttack;                          //GameObject variable to store the flagAttack gameObject
        private GameObject flagDefend;                          //GameObject variable to store the flagDefend gameObject
        private GameObject flagWait;                            //GameObject variable to store the flagWait gameObject
        private GameObject random;                              //GameObject variable to store the random gameObject
  
        // Text
        private String announcerText;                           //String variable to store the announcement text
        private string name;                                    //String variable to store the name 
      
        //Input
        Rectangle mousePos;                                     //Rectangle variable to store the mouse position
        // Colliders
        private Rectangle rockCollider;                         //Rectangle variable to store the collider for the rock
        private Rectangle paperCollider;                        //Rectangle variable to store the collider for the paper
        private Rectangle scissorCollider;                      //Rectangle variable to store the collider for the scissors
        private Rectangle potionCollider;                       //Rectangle variable to store the collider for the potions
        // Animation logic
        private bool arrived;                                   //bool variable to control if the attacker has arrived to the attack position
        private bool moveHero;                                  //bool variable to control if the hero has to move
        private bool moveFoe;                                   //bool variable to control if the foe has to move
        // stringBuilder
        private StringBuilder userInput = new StringBuilder();  //StringBuilder variable to store the userInput and initialized 
        // 2d textures to show FX
        Texture2D red_FX;                                      //Texture2D variable to show the background in red or silver color to show if the attacker inflicted damage
        // Music and SFX
        private SoundEffect swordSlash;                        //sound effects and songs to play in the background
        private SoundEffect swordClash;
        private SoundEffect hudSelect1;
        private SoundEffect hudSelect2;
        private SoundEffect hudSelect3;
        private SoundEffect hudHover;
        private SoundEffect heroDamage;
        private SoundEffect heroDeath;
        private SoundEffect foeDamage;
        private SoundEffect foeDeath;
        private SoundEffect takePotion;
        private SoundEffect announcerSelectHero;
        private SoundEffect announcerAttack;
        private SoundEffect announcerDefend;
        private SoundEffect announcerGameover;
        private Song mainTheme;
        private Song battle;
        private Song gameOver;
        // music logic variables
        private byte currentSfxPlay = 10;                   //variables to control when the sound effects have to be played
        private byte currentSfxAnnouncer = 0;

        public RPGGame()
        {
            graphics = new GraphicsDeviceManager(this);         
            graphics.PreferredBackBufferWidth = 960;                //set window width
            graphics.PreferredBackBufferHeight = 540;               //set window height
            graphics.ApplyChanges();                                
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            Window.Title = ("Ultrasword - The RPG Game");           //set window title
        }

        protected override void Initialize()
        {
            base.Initialize();                                  //initialized the game attributes from monogame
            round = 0;                                          
            character = true;
            waitTime = 0;
            score = 0;
            battleResult = false;   
            usePotion = false;
            potions = 0;
            name = "";
            arrived = false;
            moveHero = false;
            moveFoe = false;
            // file system
            file = new FileManager("../../../Data/Scores.txt");
            // Initialize gameState
            gameState = (byte)GameState.START;

            // Assest creation
            AssetFactory factory = new AssetFactory(cAtlas, bAtlas, hAtlas, helpScreen);
            // Characters
            GameObject samurai = new GameObject(new Vector2(200, 120), factory.GetAsset(2), 500, 5000, 75);
            GameObject knight = new GameObject(new Vector2(200, 120), factory.GetAsset(1), 500, 2500, 100);
            //Enemies
            GameObject skeleton = new GameObject(new Vector2(500, 120), factory.GetAsset(3), 500, 6000, 75);
            GameObject skeleton2 = new GameObject(new Vector2(500, 120), factory.GetAsset(4), 600, 4000, 80);
            GameObject crow = new GameObject(new Vector2(500, 120), factory.GetAsset(5), 700, 2000, 100);
            enemies = new[] { skeleton, skeleton2, crow };
            warriors = new[] { samurai, knight };
            hero = samurai;
            foe = skeleton;
            // BG
            candles = new GameObject(new Vector2(0, 134), factory.GetAsset(20), 0, 0, 0);
            // HUD
            flagAttack = new GameObject(new Vector2(50, 100), factory.GetAsset(21), 0, 0, 0);
            flagDefend = new GameObject(new Vector2(50, 100), factory.GetAsset(22), 0, 0, 0);
            flagWait = new GameObject(new Vector2(50, 100), factory.GetAsset(23), 0, 0, 0);
            random = new GameObject(new Vector2(786, 252), factory.GetAsset(24), 0, 0, 0);
            // Colliders
            rockCollider = new Rectangle(71, 254, 50, 50);
            paperCollider = new Rectangle(117, 254, 50, 50);
            scissorCollider = new Rectangle(163, 254, 50, 50);
            potionCollider = new Rectangle(35, 350, 24, 24);
            // Texts
            announcerText = "Wait until is time to take action!"; 
            character = true;
            Window.TextInput += TextInputHandler;
            // FX texture
            red_FX = new Texture2D(screen.GraphicsDevice, 1, 1);
            red_FX.SetData(new[] { Color.White });
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
        }

        protected override void LoadContent()
        {
            screen = new SpriteBatch(GraphicsDevice);
            sAtlas = Content.Load<Texture2D>("Atlas/screen_atlas");
            cAtlas = Content.Load<Texture2D>("Atlas/character_atlas");
            bAtlas = Content.Load<Texture2D>("Atlas/background_atlas");
            hAtlas = Content.Load<Texture2D>("Atlas/hud_atlas");
            helpScreen = Content.Load<Texture2D>("Atlas/help");
            font = Content.Load<SpriteFont>("Fonts/font");
            fontSmall = Content.Load<SpriteFont>("Fonts/fontSmall");
            // Load sound effects
            swordSlash = Content.Load<SoundEffect>("Music/sword_slash");
            swordClash = Content.Load<SoundEffect>("Music/sword_clash");
            hudSelect1 = Content.Load<SoundEffect>("Music/select1");
            hudSelect2 = Content.Load<SoundEffect>("Music/select2");
            hudSelect3 = Content.Load<SoundEffect>("Music/select3");
            hudHover = Content.Load<SoundEffect>("Music/option");
            heroDamage = Content.Load<SoundEffect>("Music/hero_damage");
            heroDeath = Content.Load<SoundEffect>("Music/hero_death");
            foeDamage = Content.Load<SoundEffect>("Music/foe_damage");
            foeDeath = Content.Load<SoundEffect>("Music/foe_death");
            takePotion = Content.Load<SoundEffect>("Music/potion");
            announcerSelectHero = Content.Load<SoundEffect>("Music/announcer_select_hero");
            announcerAttack = Content.Load<SoundEffect>("Music/announcer_attack");
            announcerDefend = Content.Load<SoundEffect>("Music/announcer_defend");
            announcerGameover = Content.Load<SoundEffect>("Music/announcer_gameover");
            
            // Load music
            mainTheme = Content.Load<Song>("Music/main_theme");
            battle = Content.Load<Song>("Music/battle");
            gameOver = Content.Load<Song>("Music/gameover");

            MediaPlayer.Volume = 0.3f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainTheme);
            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();                                     //if the escape key is pressed the game is ended.

            switch (gameState)
            {
                case (byte)GameState.START:                 //the start screen will give them option to load the hiscore table or to select the character
                    if (waitTime <= 0)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            gameState = (byte)GameState.CHARACTER_SELECT;
                            waitTime = 10;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                        {
                            // Perform actions on left mouse button click
                            gameState = (byte)GameState.CHARACTER_SELECT;
                            waitTime = 10;
                            announcerSelectHero.Play();
                        }
                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            // Perform actions on right mouse button click
                            gameState = (byte)GameState.HISCORE;
                            waitTime = 10;
                        }
                    }
                    break;
                case (byte)GameState.HISCORE:                                       //at the hiscore table the user can return to the start screen if the left click is pressed
                    if (waitTime <= 0)
                    {
                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            // Perform actions on right mouse button click
                            gameState = (byte)GameState.START;
                            waitTime = 10;
                        }
                    }
                    break;
                case (byte)GameState.CHARACTER_SELECT:                              //when the user clicks on a character, the battle screen is load.
                    if (waitTime <= 0)
                    {
                        Point pos = new Point(Mouse.GetState().X, Mouse.GetState().Y);
                        if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        {
                            gameState = (byte)GameState.START;
                            waitTime = 10;
                        }

                        if (pos.X > 30 && pos.X < 455 && pos.Y > 135 && pos.Y < 430)
                        {
                            character = true;
                            hero = warriors[0];
                            potions = 2;
                            if (currentSfxPlay != 5)
                            {
                                hudHover.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                                currentSfxPlay = 5;
                            }
                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                            {
                                gameState = (byte)GameState.BATTLE;
                                waitTime = 20;
                                hudSelect1.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                                MediaPlayer.IsRepeating = true;
                                MediaPlayer.Volume = 0.5f;
                                MediaPlayer.Play(battle);
                            }
                        }
                        else if (pos.X > 495 && pos.X < 920 && pos.Y > 135 && pos.Y < 430)
                        {
                            character = false;
                            hero = warriors[1];
                            potions = 1;
                            if (currentSfxPlay != 6)
                            {
                                hudHover.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                                currentSfxPlay =6;
                            }

                            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                            {
                                gameState = (byte)GameState.BATTLE;
                                waitTime = 10;
                                hudSelect1.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                                MediaPlayer.IsRepeating = true;
                                MediaPlayer.Volume = 0.5f;
                                MediaPlayer.Play(battle);
                            }
                        }
                    }
                    break;

                case (byte)GameState.BATTLE:                                                    //battle is when the game starts and the user choose options to fight the foe
                    
                    if (Keyboard.GetState().IsKeyDown(Keys.H))
                        gameState = (byte)GameState.HELP_S;

                    if (waitTime <= 0)
                    {
                        if (hero.Stamina >= hero.MaxStamina || foe.Stamina >= foe.MaxStamina)
                        {
                            foe.Choice = CpuRandomChoice();                                     //if the stamina bar is full the foe will fight the hero
                            if (hero.Stamina >= hero.MaxStamina)
                            {
                                if (currentSfxAnnouncer == 0)
                                {
                                    announcerAttack.Play();
                                    currentSfxAnnouncer = 1;
                                }
                                announcerText = "Time to attack!, choose wisely!";
                            }
                            else
                            {
                                if (currentSfxAnnouncer == 0)
                                {
                                    announcerDefend.Play();
                                    currentSfxAnnouncer = 2;
                                }
                                announcerText = "Time to defend yourself!, choose wisely!";     //messages are display to indicate that is time to attack or deffend
                            }
                            hero.Choice = MouseEvents(0);
                            if (hero.Choice != 0)
                            {
                                GameObject attacker, defender;
                                if (hero.Stamina >= hero.MaxStamina)
                                {
                                    attacker = hero;                                            //assign hero to attacker and defender to foe to re use code below 
                                    defender = foe;
                                    moveHero = true;                                            //if it's time to attack the hero will move
                                }
                                else
                                {
                                    attacker = foe;
                                    defender = hero;
                                    moveFoe = true;                                             //if it's time to defend the foe will move
                                }

                                battleResult = Battle(attacker.Choice, defender.Choice);        //calculates if points have to be discounted for the respective attacker

                                if (battleResult)
                                {

                                    swordSlash.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                                    defender.Health -= attacker.Strength;                       //reduces health points if the attacked was not blocked
                                    attacker.PlayAnimation((byte)Animation.ATTACK);             //play the attack animation
                                    if (defender.Health <= 0)
                                        defender.PlayAnimation((byte)Animation.DEAD);
                                    else
                                        defender.PlayAnimation((byte)Animation.DAMAGE);
                                }
                                else
                                {
                                    swordClash.Play(volume: 0.2f, pitch: 0.0f, pan: 0.0f);
                                    defender.PlayAnimation((byte)Animation.DEFEND);
                                    attacker.PlayAnimation((byte)Animation.ATTACK);
                                }

                                if (hero.Stamina >= hero.MaxStamina)                            //evaluates if the hero has attacked
                                {
                                    if (battleResult)
                                    {
                                        foeDamage.Play(volume: 0.7f, pitch: 0.0f, pan: 0.0f);
                                        announcerText = "The attack has reduced the enemy's health by "+ hero.Strength; //display message 
                                        score += 10;
                                        waitTime = 150;
                                    }
                                    else
                                    {                                                           //evaluates if the foe has attacked 
                                        announcerText = "The attack has been blocked by the enemy!";    //display message 
                                        waitTime = 150;
                                    }
                                    foe.Health = defender.Health;                               //assign values to the original object
                                    hero.Stamina = 0;                                           //restart stamina to wait for its turn
                                }
                                else
                                {
                                    if (battleResult)
                                    {
                                        heroDamage.Play(volume: 0.9f, pitch: 0.0f, pan: 0.0f);
                                        announcerText = "The attack has reduced the Hero's health by "+ foe.Strength +" points"; //display message 
                                        waitTime = 150;
                                    }
                                    else
                                    {
                                        announcerText = "The attack has been blocked by the Hero!"; //display message 
                                        waitTime = 150;
                                    }
                                    hero.Health = defender.Health;                              //assign values to the original object
                                    foe.Stamina = 0;                                            //restart stamina to wait for its turn
                                }

                                if (hero.Health <= 0)
                                {
                                    heroDeath.Play();
                                    waitTime = 180;
                                    announcerText = "You are dead!";                            //display message 
                                }
                                if (foe.Health <= 0)
                                {
                                    foeDeath.Play(volume: 0.9f, pitch: 0.0f, pan: 0.0f);
                                    waitTime = 180;
                                    announcerText = "The enemy is dead!";                       //display message 
                                }
                            }
                        }
                        else
                        {
                            currentSfxAnnouncer = 0;
                            hero.Stamina += (short)gameTime.ElapsedGameTime.Milliseconds;       //add delta time to wait for its turn to play
                            foe.Stamina += (short)gameTime.ElapsedGameTime.Milliseconds;        //add delta time to wait for its turn to play
                        }
                    }
                    if (waitTime <= 0)
                    {
                        if (foe.Health <= 0)
                        {
                            if (round >= 2)                                                     //calculates if the player fought all the enemies to end the game 
                            {
                                gameState = (byte)GameState.GAMEOVER;
                                announcerGameover.Play();
                                MediaPlayer.Volume = 0.3f;
                                MediaPlayer.IsRepeating = true;
                                MediaPlayer.Play(gameOver);

                            }
                            else
                            {
                                potions++;                                                      //calculates if the player won the round to award the prize and change the enemy
                                round++;
                                foe = enemies[round];
                                announcerText = "A new foe has appeared! do your best hero!";
                            }
                        }
                        if (hero.Health <= 0)
                        {
                            gameState = (byte)GameState.GAMEOVER;                               //if the player has no health means that lost the game
                            announcerGameover.Play();
                            MediaPlayer.Volume = 0.3f;
                            MediaPlayer.IsRepeating = true;
                            MediaPlayer.Play(gameOver);
                        }
                           
                    }

                    if (moveHero)                                                               //invokes to move the character or foe
                        MoveHero((float)gameTime.ElapsedGameTime.Milliseconds);
                    if (moveFoe)
                        MoveFoe((float)gameTime.ElapsedGameTime.Milliseconds);

                    // BG elements                                                              //updates animations for background, characters and other visual assist.
                    candles.Update(gameTime);
                    // Character elements
                    hero.Update(gameTime);
                    foe.Update(gameTime);
                    // HUD elements
                    flagAttack.Update(gameTime);
                    flagDefend.Update(gameTime);
                    flagWait.Update(gameTime);
                    random.Update(gameTime);
                    
                    break;

                case (byte)GameState.HELP_S:
                    if (Mouse.GetState().RightButton == ButtonState.Pressed)
                        gameState = (byte)GameState.BATTLE;
                    break;

                case (byte)GameState.GAMEOVER:
                    KeyboardState keys = Keyboard.GetState();                                   //detects keys pressed to get the initials
                    if (keys.IsKeyDown(Keys.Enter) && name.Length > 0)
                    {
                        file.AddNewScore(name+" "+score);                                       //add new score to the text file
                        gameState = (byte)GameState.HISCORE;                                    //fter the initials are entered, the high score table will be displayed
                        name = string.Empty;                                                    //re start status for all game objects for a new game
                        hero.Health = hero.MaxHealth;
                        hero.CurrentAnimation = (byte)Animation.IDLE;
                        hero.Stamina = 0;
                        announcerText = "Wait until is time to take action!";
                        MediaPlayer.Volume = 0.3f;
                        MediaPlayer.IsRepeating = true;
                        MediaPlayer.Play(mainTheme);

                        foreach (var enemy in enemies)                                          //reset values for enemies 
                        {
                            enemy.Health = enemy.MaxHealth;
                            enemy.CurrentAnimation = (byte)Animation.IDLE;
                            enemy.Stamina = 0;
                        }
                        score = 0;
                        round = 0;
                        foe = enemies[round];                                                   //assigns the enemy based on the current round
                    }
                    break;
            }

            if (waitTime > 0)
            {
                waitTime--;                                                                     //reduce the wait time by one
            }
            // Update mouse position for collision calculations
            MouseState mouseState = Mouse.GetState();
            mousePos = new Rectangle(mouseState.Position.X, mouseState.Position.Y, 1, 1);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            switch (gameState)
            {
                case (byte)GameState.START:                                                                 //draws background
                    screen.Begin();
                    screen.Draw(sAtlas, new Vector2(0,0), new Rectangle(0,1084,960,540), Color.White);
                    screen.End();
                    break;
                case (byte)GameState.CHARACTER_SELECT:
                    if(character)                                                                           //draws background with Samurai character selected
                    {
                        screen.Begin();
                        screen.Draw(sAtlas, new Vector2(0, 0), new Rectangle(0, 0, 960, 540), Color.White);
                        screen.End();
                    }
                    else
                    {                                                                                       //draws background with Knight character selected
                        screen.Begin();
                        screen.Draw(sAtlas, new Vector2(0, 0), new Rectangle(962, 0, 960, 540), Color.White);
                        screen.End();
                    }
                    break;

                case (byte)GameState.HISCORE:                                                               //draws table score
                    List<string[]> scoresTableRead = file.ReadFileHiscore();                
                    screen.Begin();
                    screen.Draw(sAtlas, new Vector2(0, 0), new Rectangle(0, 542, 960, 540), Color.White);  //draws background for hiscore screen
                    screen.End();
                    int printX = 380;                                                                      //position  where the text is going to be drawn in x
                    int printY = 160;                                                                      //position  where the text is going to be drawn in y
                    int counter = 0;                                                                       //int counter to control the while loop
                    while (counter < scoresTableRead.Count)
                    {
                        string name = scoresTableRead[counter][0];
                        string score = scoresTableRead[counter][1];
                        // Render and draw the name
                        Vector2 namePosition = new Vector2(printX, printY);
                        screen.Begin();
                        screen.DrawString(font, name, namePosition, Color.Lime);
                        // Render and draw the score
                        Vector2 scorePosition = new Vector2(printX + 150, printY);
                        screen.DrawString(font, score, scorePosition, Color.Lime);
                        screen.End();
                        counter++;
                        printY += 50;
                    }
                    break;
                case (byte)GameState.BATTLE:
                    screen.Begin();
                    screen.Draw(bAtlas, new Vector2(0, 0), new Rectangle(1000, 0, 960, 540), Color.White);  //draws background for battle screen
                    screen.End();
                    candles.Draw(screen);                                                                   //candles animation per frame
                    DrawHud(screen);                                                                        //draws visual background items
                    screen.Begin();
                    screen.Draw(hAtlas, new Vector2(0, 415), new Rectangle(0, 526, 958, 102), Color.White); //draws announcements section
                    screen.DrawString(font, announcerText, new Vector2(45, 440), Color.White);
                    screen.DrawString(font, "Score: "+score.ToString(), new Vector2(765, 17), Color.White);
                    screen.DrawString(fontSmall, "Press H to see how to play.", new Vector2(45, 515), new Color(255, 255, 153));
                    screen.End();

                    if (arrived)                                                                            //calculates if the attacker got to the attack position
                    {
                        screen.Begin();
                        if (battleResult)
                            screen.Draw(red_FX, new Rectangle(0, 0, 960, 540), new Color(100, 0, 10));
                        else
                            screen.Draw(red_FX, new Rectangle(0, 0, 960, 540), new Color(92, 93, 113));
                        screen.End();
                    }

                    if (usePotion)                                                                          //draws when the potion is use by the player
                    {
                        screen.Begin();
                        screen.Draw(red_FX, new Rectangle(0, 0, 960, 540), new Color(85, 255, 0));
                        usePotion = false;
                        screen.End();
                    }
                    //draws characters
                    hero.Draw(screen); 
                    foe.Draw(screen);  
                    break;
                case (byte)GameState.HELP_S:
                    screen.Begin();
                    screen.Draw(helpScreen, new Vector2(0, 0), new Rectangle(0, 0, 960, 540), Color.White); //draws help screen background
                    screen.End();
                    break;
                case (byte)GameState.GAMEOVER:
                    name = name.ToUpper();                                                                  //display the name in upper case
                    screen.Begin();
                    screen.Draw(sAtlas, new Vector2(0, 0), new Rectangle(962, 542, 960, 540), Color.White); //draws high score background
                    screen.DrawString(font, "Score : "+ score , new Vector2(380,250), Color.Lime);
                    screen.DrawString(font, name, new Vector2(550,430), Color.Lime);
                    screen.End();
                    break;


            }
            MouseEvents(1);
            screen.Begin();
            screen.Draw(hAtlas, new Vector2(mousePos.X, mousePos.Y), new Rectangle(558, 0, 28, 28), Color.White);   //draws the mouse cursor as a sword
            screen.End();
            base.Draw(gameTime);
        }

        public void DrawHud(SpriteBatch screen)
        {
            Rectangle flagPoleSpriteRect = new Rectangle(846, 54, 14, 170);
            Rectangle scrollHeroActiveRect = new Rectangle(0, 458, 208, 68);
            Rectangle scrollHeroDisabledRect = new Rectangle(0, 390, 208, 68);
            Rectangle scrollHeroEmptyRect = new Rectangle(0, 254, 208, 68);
            Rectangle scrollFoeRect = new Rectangle(2, 324, 72, 64);
            
            screen.Begin();
            screen.Draw(hAtlas, new Vector2(38, 100), flagPoleSpriteRect, Color.White);  //draw flag pole sprite
            screen.End();
            GameObject flagSprite = flagDefend;
            Rectangle scrollRect = scrollHeroActiveRect;

            if (hero.Stamina >= hero.MaxStamina)
            {
                flagSprite = flagAttack;
                scrollRect = scrollHeroActiveRect;
            }
            if (hero.Stamina < hero.MaxStamina && foe.Stamina < foe.MaxStamina)
            {
                flagSprite = flagWait;
                scrollRect = scrollHeroDisabledRect;
            }

            flagSprite.Draw(screen);
            screen.Begin();
            screen.Draw(hAtlas, new Vector2(37, 244), scrollRect, Color.White);  //draw hero's Scroll sprite - Attack mode
            screen.Draw(hAtlas, new Vector2(775, 244), scrollFoeRect, Color.White);  //draws foe's Scroll sprite
            screen.End();
            if (waitTime > 0)
            {   
                screen.Begin(); 
                switch (foe.Choice)
                {
                    case 1:
                        screen.Draw(hAtlas, new Vector2(786, 252), new Rectangle(150, 0, 50, 50), Color.White);
                        break;
                    case 2:
                        screen.Draw(hAtlas, new Vector2(786, 252), new Rectangle(200, 0, 50, 50), Color.White);
                        break;
                    case 3:
                        screen.Draw(hAtlas, new Vector2(786, 252), new Rectangle(250, 0, 50, 50), Color.White);
                        break;
                }

                switch (hero.Choice)
                {
                    case 1:
                        screen.Draw(hAtlas, new Vector2(71, 254), new Rectangle(150, 0, 50, 50), Color.White);
                        break;
                    case 2:
                        screen.Draw(hAtlas, new Vector2(117, 254), new Rectangle(200, 0, 50, 50), Color.White);
                        break;
                    case 3:
                        screen.Draw(hAtlas, new Vector2(163, 254), new Rectangle(250, 0, 50, 50), Color.White);
                        break;
                }
                screen.End();
            }
            
            if (hero.Stamina >= hero.MaxStamina || foe.Stamina >= foe.MaxStamina)
            {
                screen.Begin();
                screen.Draw(hAtlas, new Vector2(775, 244), scrollFoeRect, Color.White);
                screen.End();
                random.Draw(screen);
            }

            screen.Begin();
            //Hero bars
            DrawBar( new Vector2(60, 315), new Vector2(180, 12), Color.Black, new Color(40, 80, 150),
                (float)hero.Stamina / hero.MaxStamina, false);  //Health Bar
            DrawBar( new Vector2(60, 333), new Vector2(180, 12), Color.Black, new Color(100, 0, 10),
                 (float)hero.Health / hero.MaxHealth, true);  //Stamina Bar
                                                      //Foe bars
            DrawBar( new Vector2(725, 315), new Vector2(180, 12), Color.Black, new Color(40, 80, 150),
                 (float)foe.Stamina / foe.MaxStamina, false);  //Health Bar
            DrawBar( new Vector2(725, 333), new Vector2(180, 12), Color.Black, new Color(100, 0, 10),
                 (float)foe.Health / foe.MaxHealth, true);  //Stamina Bar
            screen.End();


            //Potions
            screen.Begin();
            screen.Draw(hAtlas, new Vector2(35, 350), new Rectangle(486, 0, 24, 24), Color.White);
            string potionsText = $" x {potions}";
            Vector2 textPosition = new Vector2(55, 350);
            screen.DrawString(fontSmall, potionsText, textPosition, Color.White);
            screen.End();
        }

        private void DrawBar(Vector2 pos, Vector2 size, Color borderC, Color barC, float progress, bool barType)
        {
            Vector2 innerPos = new Vector2(pos.X + 2, pos.Y + 2);                   ////draws the type of bar with the color and the position received.
            Vector2 innerSize = new Vector2((size.X - 4) * progress, size.Y - 4);

            // Draw the border
            screen.DrawRectangle(new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y), borderC, 2);

            // Draw the inner bar
            screen.Draw(red_FX, new Rectangle((int)innerPos.X, (int)innerPos.Y, (int)innerSize.X, (int)innerSize.Y), barC);

            //Draw the icon based on the barType
            Rectangle icon;

            if (barType)
                icon = new Rectangle(438, 0, 24, 24);
            else
                icon = new Rectangle(462, 0, 24, 24);  
            screen.Draw(hAtlas, new Vector2(pos.X - 25, pos.Y - 5), icon, Color.White);
        }

        public void MoveHero(float deltaTime)
        {
            if (hero.Position.X < 400 && !arrived)      //calculates if the player character has gotten to the attack position, if it has will return to the idle position
                hero.Position = new Vector2(hero.Position.X + deltaTime, hero.Position.Y);
            else
            {
                arrived = true;
                if (hero.Position.X > 200 && arrived)
                    hero.Position = new Vector2(hero.Position.X - deltaTime, hero.Position.Y);
                else
                {
                    moveHero = false;
                    arrived = false;
                }
            }
        }

        public void MoveFoe(float deltaTime)
        {
            if (foe.Position.X > 300 && !arrived)           //calculates if the foe has gotten to the attack position, if it has will return to the idle position
                foe.Position = new Vector2(foe.Position.X - deltaTime, foe.Position.Y);
            else
            {
                arrived = true;
                if (foe.Position.X < 500 && arrived)
                    foe.Position = new Vector2(foe.Position.X + deltaTime, foe.Position.Y);
                else
                {
                    moveFoe = false;
                    arrived = false;
                }
            }
        }

        public sbyte MouseEvents(byte mouseEventType)
        {
            bool collideRock = rockCollider.Contains((int)mousePos.X, (int)mousePos.Y);         //detects if there have been a collision with one of the items
            bool collidePaper = paperCollider.Contains((int)mousePos.X, (int)mousePos.Y);
            bool collideScissor = scissorCollider.Contains((int)mousePos.X, (int)mousePos.Y);
            bool collidePotion = potionCollider.Contains((int)mousePos.X, (int)mousePos.Y);

            if (mouseEventType == 0)
            {
                MouseState mouseState = Mouse.GetState();
                if (mouseState.LeftButton == ButtonState.Pressed)                               //detects the clicks of the mouse
                {
                    if (collideRock)
                    {
                        hudSelect1.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                        return 1;
                    }
                    else if (collidePaper)
                    {
                        hudSelect2.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                        return 2;
                    }
                    else if (collideScissor)
                    {
                        hudSelect3.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                        return 3;
                    }
                    else if (collidePotion)
                    {
                        if (potions > 0)                                                        //if potions available will increment health
                        {
                            takePotion.Play();
                            potions--;
                            usePotion = true;
                            waitTime = 60;                                                      //set a wait time to allow clicks
                            announcerText = "You have used a potion, your health has been restored 100 points"; //display message to indicate that a potion has been used.
                            if ((hero.Health + 100) <= hero.MaxHealth)
                                hero.Health += 100;
                            else
                                hero.Health = hero.MaxHealth;
                        }
                    }
                }
                else
                    return 0;                                                                   //if return 0 indicated that an error occurred
            }

            if (mouseEventType == 1 && gameState != (byte)GameState.HELP_S)                     //detects the hover of the mouse
            {
                if (hero.Stamina >= hero.MaxStamina || foe.Stamina >= foe.MaxStamina)           //if stamina bar is full the item will be highlighted
                {
                    screen.Begin();
                    if (collideRock)
                    {
                        if (currentSfxPlay != 0)
                        {
                            hudHover.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                            currentSfxPlay = 0;
                        }
                        screen.Draw(hAtlas, new Vector2(71, 254), new Rectangle(150, 0, 50, 50), Color.White);
                    }
                    else if (collidePaper)
                    {
                        if (currentSfxPlay !=1)
                        {
                            hudHover.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                            currentSfxPlay = 1;
                        }
                        screen.Draw(hAtlas, new Vector2(117, 254), new Rectangle(200, 0, 50, 50), Color.White);
                    }
                    else if (collideScissor)
                    {
                        if (currentSfxPlay != 2)
                        {
                            hudHover.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                            currentSfxPlay = 2;
                        }
                        screen.Draw(hAtlas, new Vector2(163, 254), new Rectangle(250, 0, 50, 50), Color.White);
                    }
                    else if (collidePotion)
                    {
                        if (currentSfxPlay != 3)
                        {
                            hudHover.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                            currentSfxPlay = 3;
                        }
                        screen.Draw(hAtlas, new Vector2(35, 350), new Rectangle(486, 24, 24, 24), Color.White);
                    }
                    screen.End();
                }
            }
            return 0;
        }

        private void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;
            var character = args.Character;
            if (gameState == (int)GameState.GAMEOVER)
            {                                                       //accepts letters only
                if (char.IsLetter(character) && name.Length < 5)    //caps the input to 5 letters    
                    name += character;
            }
            
            if (pressedKey == Keys.Back && name.Length >0)
            {
                name = name.Remove(name.Length - 1);
            }
            
        }

        public static sbyte CpuRandomChoice()
        {
            Random random = new Random();
            // Generate a random choice for the computer
            sbyte[] choices = { 1, 2, 3 };
            sbyte computerChoice = choices[random.Next(choices.Length)];  // gets the choice
            return computerChoice;
        }

        public static bool Battle(int attacker, int defender)
        {
            if (attacker == defender)                       //if the chose option is the same to the opposite gets damage
            {
                return true;
            }
            else if ((attacker == 1 && defender == 3) || (attacker == 2 && defender == 1) || (attacker == 3 && defender == 2))
            {                                               //rock = 1 **** paper = 2 **** scissors = 3
                return true;                                //paper wins over rock - scissors wins over paper - rock wins over scissors
            }
            else
            {
                return false;                               //if false no damage is cause  to the character attacked
            }
        }
    }
}