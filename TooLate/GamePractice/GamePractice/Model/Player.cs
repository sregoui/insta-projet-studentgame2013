using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TooLateLibrary.Animation;
using TooLateLibrary.Graphics;

namespace TooLate.Model
{
    public class Player : Base2DGameComponent
    {
        #region Fields

        /// <summary>
        /// Stores player's life.
        /// </summary>
        private int _life;

        private SimpleAnimationSprites _animationWait;
        private SimpleAnimationSprites _animationWaitLeft;
        private SimpleAnimationSprites _animationRunRight;
        private SimpleAnimationSprites _animationRunLeft;

        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;
        private MouseState _mouseState;

        private int _direction;

        /// <summary>
        /// Stores the current game.
        /// </summary>
        private Game1 _game;

        #endregion

        #region Constructor

        /// <summary>
        /// Initialize a new instance of a Player.
        /// </summary>
        /// <param name="game">The game that will use this component.</param>
        public Player(Game1 game) : base(game)
        {
            _game = game;
            _direction = 0;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        public override void  Initialize()
        {
            _animationWait = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Wait",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(1, 1)
            });

            _animationWait.position.X = 400;
            _animationWait.position.Y = 300;

            _animationWaitLeft = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Wait_Left",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(1, 1)
            });

            _animationWaitLeft.position.X = 400;
            _animationWaitLeft.position.Y = 300;

            _animationRunRight = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Run",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(6, 1)
            });

            _animationRunRight.position.X = 400;
            _animationRunRight.position.Y = 300;

            _animationRunLeft = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Run_left",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(6, 1)
            });

            _animationRunLeft.position.X = 400;
            _animationRunLeft.position.Y = 300;

            _animationWait.Initialize();
            _animationWaitLeft.Initialize();
            _animationRunRight.Initialize();
            _animationRunLeft.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            _animationWait.LoadContent(spriteBatch);
            _animationWaitLeft.LoadContent(spriteBatch);
            _animationRunRight.LoadContent(spriteBatch);
            _animationRunLeft.LoadContent(spriteBatch);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {

            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            _oldKeyboardState = _keyboardState;

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _direction = 0;
                _animationRunRight.Update(gameTime);
            }
            else if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _direction = 1;
                _animationRunLeft.Update(gameTime);
            }
            else
            {
                if (_direction == 0)
                {
                    _animationWait.Update(gameTime);
                }
                else if (_direction == 1)
                {
                    _animationWaitLeft.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            Renderer.Begin();

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _animationRunRight.Draw(gameTime, false);
            }
            else if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _animationRunLeft.Draw(gameTime, false);
            }
            else
            {
                if (_direction == 0)
                {
                    _animationWait.Draw(gameTime, false);
                }
                else if (_direction == 1)
                {
                    _animationWaitLeft.Draw(gameTime, false);
                }
            }

            Renderer.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}
