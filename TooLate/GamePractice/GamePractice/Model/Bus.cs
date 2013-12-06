using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TooLateLibrary.Animation;
using TooLateLibrary.Graphics;

namespace TooLate.Model
{
    public class Bus : Base2DGameComponent
    {
        #region Fields

        private SimpleAnimationSprites _animationWait;
        private SimpleAnimationSprites _animationRun;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;
        private MouseState _mouseState;

        private float _speed;

        /// <summary>
        /// Stores the current game.
        /// </summary>
        private Game1 _game;

        #endregion

        #region Constructor

        public Bus(Game1 game) : base(game)
        {
            _speed = 0.1f;
            _game = game;
        }

        #endregion

        #region Properties

        // Physics state
        public Point Position
        {
            get { return _animationRun.position; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize animations,...
        /// </summary>
        public override void Initialize()
        {
            _animationWait = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Bus/Wait",
                FrameRate = 15,
                FrameSize = new Point(928, 232),
                Loop = true,
                NbFrames = new Point(1, 1)
            });

            _animationWait.position.X = 0;
            _animationWait.position.Y = 200;

            _animationRun = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Bus/Bus-Run",
                FrameRate = 15,
                FrameSize = new Point(928, 232),
                Loop = true,
                NbFrames = new Point(2, 1)
            });

            _animationRun.position.X = 0;
            _animationRun.position.Y = 220;


            _animationWait.Initialize();


            base.Initialize();
        }

        /// <summary>
        /// Load all we need for our bus.
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            _animationWait.LoadContent(spriteBatch);
            _animationRun.LoadContent(spriteBatch);

            base.LoadContent();
        }

        public void SwitchIA(int distance)
        {
            if (distance > 500)
            {
                _animationRun.position.X -= 10;
            }
        }

        /// <summary>
        /// Update Bus's state.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        public override void Update(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(PlayerIndex.One);
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            _animationRun.Update(gameTime);

            if (_keyboardState.IsKeyDown(Keys.Right) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickRight) || _gamePadState.IsButtonDown(Buttons.DPadRight))
            {
                _animationRun.position.X--;
            }
            else if (_keyboardState.IsKeyDown(Keys.Left) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) ||
                     _gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                _animationRun.position.X = _animationRun.position.X+10;
            }
            else
            {
                _animationRun.position.X++;
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the player.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        public override void Draw(GameTime gameTime)
        {
            _animationRun.Draw(gameTime);

            base.Draw(gameTime);
        }

        #endregion
    }
}
