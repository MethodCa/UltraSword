using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_game
{
    enum Animation
    {
        IDLE,               // 0
        ATTACK,             // 1
        DEFEND,             // 2
        DAMAGE,             // 3
        DEAD                // 4
    }
    enum GameState
    {
        START,              // 0
        CHARACTER_SELECT,   // 1
        HISCORE,            // 2
        BATTLE,             // 3
        HELP_S,             // 4
        GAMEOVER            // 5
    }
    enum AnimationType
    {
        STATIC,             // 0
        LOOP,               // 1
        ONE_TIME,           // 2
    }
    enum Factory
    {
        KNIGHT,             // 0
        SAMURAI,            // 1
        SKELETON,           // 2
        SKELETON2,          // 3
        BIRD,               // 4
    }
}
