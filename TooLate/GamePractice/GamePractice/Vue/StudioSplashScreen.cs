using System;
using System.CodeDom;
using System.Net.Mime;
using TooLateLibrary.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TooLateLibrary.Animation;

namespace TooLate.Screens
{
    public class StudioSplashScreen : Base2DGameComponent
    {
        #region Constants

        /// <summary>
        /// Stores the splash screen ratio (splash screen height / screen height).
        /// </summary>
        private const int _splashScreenRatio = 2;

        /// <summary>
        /// Stores the path of the splash screen texture asset.
        /// </summary>
        private const string _splashScreenTexturePath = "Textures/SplashScreen";

        /// <summary>
        /// Stores the path of the splash screen opacity curve asset.
        /// </summary>
        private const string _splashScreenOpacityCurvePath = "Transitions/SplashScreenOpacity";

        /// <summary>
        /// Stores the path of the splash screen size curve asset.
        /// </summary>
        private const string _splashScreenSizeCurvePath = "Transitions/SplashScreenSize";

        #endregion

        #region Fields

        /// <summary>
        /// Stores the Texture 2D that contains the splash screen.
        /// </summary>
        private Texture2D _texture;

        /// <summary>
        /// Stores the Easing that will handle the opacity.
        /// </summary>
        private Easing<Color> _opacity;

        /// <summary>
        /// Stores the Easing that will handle the size.
        /// </summary>
        private Easing<float> _size;

        /// <summary>
        /// Stores the current Game.
        /// </summary>
        private Game1 _game;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the StudioSplashScreen class.
        /// </summary>
        /// <param name="game">The game that will use this component</param>
        public StudioSplashScreen(Game1 game) : base(game)
        {
            _game = game;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position, in pixels, of the texture on the screen.
        /// </summary>
        private Vector2 Position
        {
            get
            {
                Vector2 position;

                // We want to put a sprite on middle on the screen,
                // that is independant from the screen resolution,
                // and screen aspect ration.

                // We arbitrarly decide that the texture of the
                // splash screen will always take one fourth of the
                // screen height, in pixels, Do not forget to take in
                // account the growing size of the transition.
                float height = (float)Game.Window.ClientBounds.Height / _splashScreenRatio * _size;

                // We now must respect the texture width / height ratio
                // by setting the width of the rectangle accordingly
                float width = height*_texture.Width/_texture.Height;

                // Now that we have the rendering width and height of
                // the texture, place it to the center of the
                // texture will be on the center of the screen.
                position.X = (Game.Window.ClientBounds.Width/2f) - (width/2f);
                position.Y = (Game.Window.ClientBounds.Height/2f) - (height/2f);

                return position;
            }
        }

        private float Size
        {
            get
            {
                return (float) Game.Window.ClientBounds.Height/_splashScreenRatio/_texture.Height * _size;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the StudioSplashScreen's related assets.
        /// </summary>
        protected override void LoadContent()
        {
            // Load the splash screen texture to memory.
            _texture = Content.Load<Texture2D>(_splashScreenTexturePath);

            // Load the opacity transition curve data.
            _opacity = new Easing<Color>(new ColorInterpolation(Color.Transparent, Color.White), Content.Load<Curve>(_splashScreenOpacityCurvePath));
            _opacity.Start();
            _opacity.Shutdown();
            _opacity.Stopped += new EventHandler(End);

            // Load the size transition curve data.
            _size = new Easing<float>(new FloatInterpolation(), Content.Load<Curve>(_splashScreenSizeCurvePath));
            _size.Start();
            _size.Shutdown();
            _size.Stopped += new EventHandler(End);

            base.LoadContent();
        }

        /// <summary>
        /// Unloads the StudioSplashScreen's related assets.
        /// </summary>
        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Stops the Game
        /// </summary>
        /// <param name="sender">The object that sent the event</param>
        /// <param name="e">The event parameters</param>
        private void End(object sender, EventArgs e)
        {
            // When the animation is finish, we set the CurrentGameState to MENU.
            ScreenState.CurrentGameState = ScreenState.GameState.MENU;

            _game.Transition = true;

            ScreenState.CurrentGameState = ScreenState.GameState.INGAME_1;

            //Game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            // Update the time frame of the transitions.
            _opacity.Update(gameTime);
            _size.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the StudioSplashScreen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Renderer.Begin();

            // Renders the splash screen in the middle of the screen.
            Renderer.Draw(_texture, Position, null, _opacity, 0, Vector2.Zero, _size, SpriteEffects.None, 0);

            Renderer.End();

            base.Draw(gameTime);
        }

        #endregion

    }
}
