using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TooLateLibrary.Animation;
using TooLateLibrary.Graphics;

namespace TooLate.Model
{
    public class Player : Base2DGameComponent
    {
        private int _life;

        private SimpleAnimationSprites _animationRun;


        public Player(Game1 game) : base(game)
        {
            
        }

    }
}
