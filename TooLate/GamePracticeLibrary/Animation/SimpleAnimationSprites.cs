using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TooLateLibrary.Animation
{
    public class SimpleAnimationSprites
    {
        #region Fields

        public Point position;
        protected Game game;
        protected SimpleAnimationDefinition definition;
        protected SpriteBatch spriteBatch;
        protected Texture2D sprite;
        protected Point currentFrame;
        protected bool finishedAnimation = false;

        protected double timeBetweenFrame = 47; // 60 fps

        protected double lastFrameUpdatedTime = 0;

        private int _framerate = 60;

        #endregion

        #region Constructor

        public SimpleAnimationSprites(Game g, SimpleAnimationDefinition d)
        {
            game = g;
            definition = d;
            position = new Point();
            currentFrame = new Point();
        }

        #endregion

        #region Properties

        public int Framerate
        {
            get { return _framerate; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Framerate can't be less or equal to 0");
                }
                if (_framerate != value)
                {
                    _framerate = value;
                    timeBetweenFrame = 1000.0d/(double) _framerate;
                }
            }
        }

        /// <summary>
        /// Gets a texture origin at the bottom center of each frame.
        /// </summary>
        public Vector2 Origin
        {
            get { return new Vector2(FrameWidth / 2.0f, FrameHeight); }
        }

        /// <summary>
        /// Gets the width of a frame in the animation.
        /// </summary>
        public int FrameWidth
        {
            // Assume square frames.
            get { return currentFrame.X; }
        }

        /// <summary>
        /// Gets the height of a frame in the animation.
        /// </summary>
        public int FrameHeight
        {
            get { return currentFrame.Y; }
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            _framerate = definition.FrameRate;
        }

        public void LoadContent(SpriteBatch sb = null)
        {
            sprite = game.Content.Load<Texture2D>(definition.AssetName);

            if (sb == null)
            {
                spriteBatch = new SpriteBatch(game.GraphicsDevice);
            }
            else
            {
                spriteBatch = sb;
            }
        }

        /// <summary>
        /// Réinitialise l'animation
        /// </summary>
        public void Reset()
        {
            currentFrame = new Point();
            finishedAnimation = false;
            lastFrameUpdatedTime = 0;
        }

        /// <summary>
        /// Choisie l'image à afficher en comparant le temps d'affichage
        /// de la dernière frame est plus grand que le temps entre les frames.
        /// </summary>
        /// <param name="time"></param>
        public void Update(GameTime time)
        {
            if (finishedAnimation) return;

            lastFrameUpdatedTime += time.ElapsedGameTime.Milliseconds;

            if (lastFrameUpdatedTime > timeBetweenFrame)
            {
                lastFrameUpdatedTime = 0;

                if (definition.Loop)
                {
                    currentFrame.X ++;

                    if (currentFrame.X >= definition.NbFrames.X)
                    {
                        currentFrame.X = 0;
                        currentFrame.Y ++;

                        if (currentFrame.Y >= definition.NbFrames.Y)
                        {
                            currentFrame.Y = 0;
                        }
                    }
                }
                else
                {
                    currentFrame.X ++;

                    if (currentFrame.X >= definition.NbFrames.X)
                    {
                        currentFrame.X = 0;
                        currentFrame.Y ++;

                        if (currentFrame.Y >= definition.NbFrames.Y)
                        {
                            currentFrame.X = definition.NbFrames.X - 1;
                            currentFrame.Y = definition.NbFrames.Y - 1;
                            finishedAnimation = true;
                        }
                    }
                }
            }
        }

        public void Draw(GameTime time, bool doBeginEnd = true)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(sprite, 
                                new Rectangle(position.X, position.Y, definition.FrameSize.X, definition.FrameSize.Y ),
                                new Rectangle(currentFrame.X * definition.FrameSize.X, currentFrame.Y * definition.FrameSize.Y, definition.FrameSize.X, definition.FrameSize.Y),
                                Color.White);

            spriteBatch.End();
        }

        #endregion
    }
}
