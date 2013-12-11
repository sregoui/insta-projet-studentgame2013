using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using TooLateLibrary.Animation;
using TooLateLibrary.Graphics;
using TooLateLibrary.Timers;

namespace TooLate.Model
{
    public class Player : Base2DGameComponent
    {
        #region Constants

        // Constants for controling horizontal movement.
        private const float MoveAcceleration = 13000.0f;
        private const float MaxMoveSpeed = 1750.0f;
        private const float GroundDragFactor = 0.48f;
        private const float AirDragFactor = 0.58f;

        // Constants for controlling vertical movement.
        private const float MaxJumpTime = 0.35f;
        private const float JumpLaunchVelocity = -3500.0f;
        private const float GravityAcceleration = 3400.0f;
        private const float MaxFallSpeed = 550.0f;
        private const float JumpControlPower = 0.14f;

        // Input configuration
        private const float MoveStickScale = 1.0f;
        private const float AccelerometerScale = 1.5f;
        private const Buttons JumpButton = Buttons.A;

        // Speed configuration
        private const int MinimumSpeed = 5;
        private const int MaximumSpeed = 40;

        #endregion

        #region Fields

        /// <summary>
        /// Stores player's life.
        /// </summary>
        private int _life;

        private SimpleAnimationSprites _animationWait;
        private SimpleAnimationSprites _animationWaitLeft;
        private SimpleAnimationSprites _animationRunRight;
        private SimpleAnimationSprites _animationRunLeft;
        private SimpleAnimationSprites _animationJumpLeft;
        private SimpleAnimationSprites _animationJumpRight;
        private SimpleAnimationSprites _currentAnimation;

        private int _classement;

        private bool _isAlive;
        private bool _isOnGround;
        private bool _isStopping;

        private Vector2 _position;
        private Vector2 _velocity;
        private float _movement;
        private Rectangle _localBounds;
        private int _speed;

        private GamePadState _gamePadState;
        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;
        private MouseState _mouseState;

        private Timer _timer;
        private Timer _timerDecelerate;
        private bool _finishCountdown;
        private bool _accelerate;

        /// <summary>
        /// Defines times to accelerate
        /// </summary>
        private TimeSpan _span1 = TimeSpan.FromMilliseconds(2);
        private TimeSpan _span2 = TimeSpan.FromMilliseconds(5);
        private TimeSpan _span3 = TimeSpan.FromMilliseconds(8);

        // Jumping state
        private bool isJumping;
        private bool wasJumping;
        private float jumpTime;

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
            Classement = 2;
            _speed = MinimumSpeed;
            _accelerate = false;
            _finishCountdown = true;
            IsStopping = false;

        }

        #endregion

        #region Properties

        public bool IsAlive
        {
            get { return _isAlive; }
        }

        // Physics state
        public Point Position
        {
            get { return _animationRunLeft.position; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public bool IsOnGround
        {
            get { return _isOnGround; }
            set { _isOnGround = value; }
        }

        /// <summary>
        /// Gets a rectangle which bounds this player in world space.
        /// </summary>
        public Rectangle BoundingRectangle
        {
            get
            {
                int left = (int) Math.Round(Position.X - _currentAnimation.Origin.X) + _localBounds.X;
                int top = (int) Math.Round(Position.Y - _currentAnimation.Origin.Y) + _localBounds.Y;

                return new Rectangle(left, top, _localBounds.Width, _localBounds.Height);
            }
        }

        public int Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int Classement
        {
            get { return _classement; }
            set { _classement = value; }
        }

        public bool IsStopping
        {
            get { return _isStopping; }
            set { _isStopping = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize animations,...
        /// </summary>
        public override void  Initialize()
        {
            _timerDecelerate = new Timer();

            _animationWait = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Wait",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(1, 1)
            });

            _animationWait.position.X = 400;
            _animationWait.position.Y = 400;

            _animationWaitLeft = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Wait_Left",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(1, 1)
            });

            _animationWaitLeft.position.X = 400;
            _animationWaitLeft.position.Y = 400;

            _animationRunRight = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Run",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(6, 1)
            });

            _animationRunRight.position.X = 400;
            _animationRunRight.position.Y = 400;

            _animationRunLeft = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Run_left",
                FrameRate = 15,
                FrameSize = new Point(112, 136),
                Loop = true,
                NbFrames = new Point(6, 1)
            });

            _animationRunLeft.position.X = 400;
            _animationRunLeft.position.Y = 400;

            _animationJumpLeft = new SimpleAnimationSprites(_game, new SimpleAnimationDefinition()
            {
                AssetName = "Sprites/Player/Jump_Left",
                FrameRate = 1500,
                FrameSize = new Point(112, 150),
                Loop = true,
                NbFrames = new Point(6, 1)
            });

            _animationJumpLeft.position.X = 400;
            _animationJumpLeft.position.Y = 400;

            _animationWait.Initialize();
            _animationWaitLeft.Initialize();
            _animationRunRight.Initialize();
            _animationRunLeft.Initialize();
            _animationJumpLeft.Initialize();

            _currentAnimation = _animationWait;

            // Calculate bounds within texture size.
            int width = (int) (_animationWait.FrameWidth*0.4);
            int left = (_animationWait.FrameWidth - width)/2;
            int height = (int) (_animationWait.FrameWidth*0.8);
            int top = _animationWait.FrameWidth - height;
            _localBounds = new Rectangle(left, top, width, height);

            _timer = new Timer();

            base.Initialize();
        }

        /// <summary>
        /// Load all we need for our player.
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);

            _animationWait.LoadContent(spriteBatch);
            _animationWaitLeft.LoadContent(spriteBatch);
            _animationRunRight.LoadContent(spriteBatch);
            _animationRunLeft.LoadContent(spriteBatch);
            _animationJumpLeft.LoadContent(spriteBatch);

            base.LoadContent();
        }

        /// <summary>
        /// Calcultate speed between input
        /// </summary>
        /// <param name="_t"></param>
        public void CalculateSpeed(Timer t)
        {
            if(t.Elapsed <= _span1)
            {
                if (_speed <= MaximumSpeed)
                {
                    _speed += 3;
                }
            }
            else if (t.Elapsed <= _span2)
            {
                if (_speed <= MaximumSpeed)
                {
                    _speed += 2;
                }
            }
            else if (t.Elapsed <= _span3)
            {
                if (_speed <= MaximumSpeed)
                {
                    _speed += 1;
                }
            }
            else
            {
                _speed--;
            }
                
        }

        public void CalculateDecelerate()
        {
            if (_speed > MinimumSpeed)
            {
                _speed -= 5;
            }
            _timerDecelerate.Shutdown();
            _finishCountdown = true;
        }

        /// <summary>
        /// Update Player's state.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        public override void Update(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(PlayerIndex.One);
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            _oldKeyboardState = _keyboardState;

            if (!IsStopping)
            {
                if (_finishCountdown)
                {
                    _timerDecelerate.Start();
                    _finishCountdown = false;
                }

                if (_timerDecelerate.Elapsed == TimeSpan.FromSeconds(1))
                {
                    CalculateDecelerate();
                }


                if (_keyboardState.IsKeyDown(Keys.Space) || _gamePadState.IsButtonDown(Buttons.A))
                {
                    //_animationJumpLeft.Update(gameTime);
                    //_currentAnimation = _animationJumpLeft;
                    if (_accelerate == false)
                    {
                        _timer.Start();
                        _accelerate = true;
                    }
                    else
                    {
                        CalculateSpeed(_timer);
                        _timer.Stop();
                        _timer.Reset();
                        _accelerate = false;
                    }


                    _animationRunRight.Update(gameTime);
                }
                // If the player goes on right.
                else if (_keyboardState.IsKeyDown(Keys.Right) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickRight) || _gamePadState.IsButtonDown(Buttons.DPadRight))
                {
                    _direction = 0;
                    _animationRunRight.Update(gameTime);
                    _currentAnimation = _animationRunRight;
                }
                // If the player goes on left.
                else if (_keyboardState.IsKeyDown(Keys.Left) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) || _gamePadState.IsButtonDown(Buttons.DPadLeft))
                {
                    _direction = 1;
                    _animationRunLeft.Update(gameTime);
                    _currentAnimation = _animationRunLeft;
                }
                // Else, the character wait
                else
                {
                    if (_direction == 0)
                    {
                        _animationWait.Update(gameTime);
                        _currentAnimation = _animationWait;
                    }
                    else if (_direction == 1)
                    {
                        _animationWaitLeft.Update(gameTime);
                        _currentAnimation = _animationWaitLeft;
                    }
                }
            }
            else
            {
                _animationWait.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the player.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        public override void Draw(GameTime gameTime)
        {
            _gamePadState = GamePad.GetState(PlayerIndex.One);
            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            Renderer.Begin();

            if (!IsStopping)
            {
                if (_keyboardState.IsKeyDown(Keys.Space) || _gamePadState.IsButtonDown(Buttons.A))
                {
                    //_animationJumpLeft.Draw(gameTime, false);
                    _animationRunRight.Draw(gameTime, false);
                }
                // If the player goes on right.
                else if (_keyboardState.IsKeyDown(Keys.Right) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickRight) || _gamePadState.IsButtonDown(Buttons.DPadRight))
                {
                    _animationRunRight.Draw(gameTime, false);
                }
                // If the player goes on left.
                else if (_keyboardState.IsKeyDown(Keys.Left) || _gamePadState.IsButtonDown(Buttons.LeftThumbstickLeft) || _gamePadState.IsButtonDown(Buttons.DPadLeft))
                {
                    _animationRunLeft.Draw(gameTime, false);
                }
                // Else, the character wait
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
            }
            else
            {
                _animationWait.Draw(gameTime, false);
            }

            Renderer.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}
