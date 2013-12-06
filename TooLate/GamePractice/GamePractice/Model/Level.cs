using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TooLateLibrary.Graphics;
using TooLateLibrary.Timers;

namespace TooLate.Model
{
    public class Level : Base2DGameComponent
    {
        #region Fields

        private Player _player1;
        private Bus _bus;

        private SpriteFont _positionFont;
        private SpriteFont _defaultFont;

        private float _mapSize;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;
        private MouseState _mouseState;

        private ParallaxSideScroller _scroller;

        /// <summary>
        /// Stores the current game.
        /// </summary>
        private Game1 _game;

        /// <summary>
        /// Store the current component's collection
        /// </summary>
        private GameComponentCollection _components;

        #endregion

        #region Constructor

        public Level(Game1 game, float mapSize, GameComponentCollection components)
            : base(game)
        {
            _mapSize = mapSize;
            _game = game;
            _components = components;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize animations,...
        /// </summary>
        public override void Initialize()
        {
            _scroller = new ParallaxSideScroller(_game, false, true);
            _bus = new Bus(_game);
            _player1 = new Player(_game);

            // Load each layers for the scrolling
            _scroller.Add("Layers/Layer1");
            _scroller.Add("Layers/Layer2");
            _scroller.Add("Layers/Layer3");
            _scroller.Add("Layers/Layer5");
            _scroller.Add("Layers/Layer7");

            _positionFont = Content.Load<SpriteFont>("Font/Classement");
            _defaultFont = Content.Load<SpriteFont>("Font/Default");

            // Add components to the collection
            _components.Add(_scroller);
            _components.Add(_bus);
            _components.Add(_player1);
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

            CheckPosition();

            if (_keyboardState.IsKeyDown(Keys.Left) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) || _gamePadState.IsButtonDown(Buttons.DPadLeft))
            {
                _scroller.MoveBy(new Vector2(_player1.Speed, 0));
                _mapSize += 0.1f;   
            }
            else if (_keyboardState.IsKeyDown(Keys.Right) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickRight) || _gamePadState.IsButtonDown(Buttons.DPadRight))
            {
                _scroller.MoveBy(new Vector2(_player1.Speed*-1, 0));
                _mapSize -= 0.1f;
            }


            base.Update(gameTime);
        }

        public void CheckPosition()
        {
            if (_player1.Position.X > _bus.Position.X + 850)
            {
                _player1.Classement = 1;

                _bus.SwitchIA(_player1.Position.X);
            }
            else
            {
                _player1.Classement = 2;
            }
        }

        /// <summary>
        /// Draws the player.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        public override void Draw(GameTime gameTime)
        {
            string classement = _player1.Classement.ToString() + "nd";

            if (_player1.Classement == 1)
            {
                classement = _player1.Classement.ToString() + "st";
            }

            SpriteBatch spriteBatch = new SpriteBatch(_game.GraphicsDevice);

            spriteBatch.Begin();

            // Shows Position of player
            Vector2 positionP1 = new Vector2(100, 550);

            spriteBatch.DrawString(_positionFont, classement, positionP1, Color.Yellow);

            // Shows distance with school
            Vector2 positionMap = new Vector2(800, 50);

            spriteBatch.DrawString(_defaultFont, _mapSize.ToString("0000") + " Yards from School", positionMap, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion
    }
}
