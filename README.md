# UltraSword PyGame
RPG videogame written in C# using Monogame Framework[^1]. Battle logic is based in the popular Rock, paper, scissors game. 
Player and CPU need to wait until its time bar is complete, when the bar is full its you turn to Attack!

<p align="center">
    <img src="https://github.com/MethodCa/UltraSword/assets/15893276/a0734897-6525-418d-b318-753d079830ea" alt="UltraSword">
</p>

The game heavily relies on visual cues to alert or inform the player of what's happening in-game, a large number of animations and sound effects were put in place to enhance the gameplay.
<p align="center">
    <img src="https://github.com/MethodCa/UltraSword/assets/15893276/9d69b330-a1bb-40d9-8d69-a47823e18315" alt="UltraSword">
    <img src="https://github.com/MethodCa/UltraSword/assets/15893276/b41ede1a-92e9-4719-ae6d-07415fe4946e" alt="UltraSword">
    <img src="https://github.com/MethodCa/UltraSword/assets/15893276/06eb1bfe-ceda-4518-b0ec-8658ab373301" alt="UltraSword">
</p>

The animations are achieved using a custom Class written for UltraSword called AnimatedSprite. AnimatedSprite is a GameObject that contains:
```c# 
public AnimatedSprite(Texture2D atlas, bool horizontalLoaging, Rectangle firstFramePosition,
                      byte animationType, short frameDuration, byte totalAnimationFrames)
```
AnimatedSprite renders and iterate the animation frames, Animations can be type LOOP, ONE_TIME or STATIC.

```c#
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
```
To render a frame AnimatedSprite iterates through the Texture2D atlas and selects the frame that should be rendered, frames can be stored in the texture atlas using Horizontal 
or Vertical alignment.
- Horizontal Aligment example.
![Animations](https://github.com/MethodCa/UltraSword/assets/15893276/37481580-d1a8-46e3-bd3f-eda9aeb61caf)

```c#
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
```

> [!NOTE]
> This game was ported to Python using PyGame! have a look here [UltraSword PyGame](https://github.com/MethodCa/UltraSword_PyGame)
> 
[^1]: [MonoGame](https://monogame.net/) Open-source implementation of the Microsoft XNA Framework that supports all gaming platforms. 
