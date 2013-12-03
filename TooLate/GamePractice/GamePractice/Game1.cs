using System;
using System.Collections.Generic;
using System.Linq;
using TooLate.Model;
using TooLate.Screens;
using TooLateLibrary.Graphics;
using TooLateLibrary.Timers;
using TooLateLibrary.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TooLate
{
    /// <summary>
    /// Type principal pour votre jeu
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region Fields

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private bool _transition;

        private KeyboardState _keyboardState;
        private KeyboardState _oldKeyboardState;
        private MouseState _mouseState;

        private Player _player;

        private ParallaxSideScroller _scroller;

        /// <summary>
        /// Stores the Countdown that will enable the game to auto-exit after N seconds
        /// </summary>
        private Countdown _countdown;

        #endregion

        #region Construtors

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 660;
            graphics.PreferredBackBufferWidth = 1056;
            graphics.IsFullScreen = false;
        }

        #endregion

        #region Properties

        public bool Transition
        {
            get { return _transition; }
            set { _transition = value; }
        }

        #endregion

        /// <summary>
        /// Permet au jeu d’effectuer l’initialisation nécessaire pour l’exécution.
        /// Il peut faire appel aux services et charger tout contenu
        /// non graphique. Calling base.Initialize énumère les composants
        /// et les initialise.
        /// </summary>
        protected override void Initialize()
        {
            // Initializes the countdown, with 10 seconds delay,
            // triggering the EndGame method, and starting immediately
            _countdown = new Countdown(TimeSpan.FromSeconds(60), EndGame, true);

            _transition = false;

            Components.Add(new StudioSplashScreen(this));
            
            _scroller = new ParallaxSideScroller(this, false, true);

            _scroller.Add("Layers/Layer1");
            _scroller.Add("Layers/Layer2");
            _scroller.Add("Layers/Layer3");
            _scroller.Add("Layers/Layer5");
            _scroller.Add("Layers/Layer7");

            //Components.Add(_scroller);

            base.Initialize();
        }

        void EndGame(object sender, EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// LoadContent est appelé une fois par partie. Emplacement de chargement
        /// de tout votre contenu.
        /// </summary>
        protected override void LoadContent()
        {
            // Créer un SpriteBatch, qui peut être utilisé pour dessiner des textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: utilisez this.Content pour charger votre contenu de jeu ici
        }

        /// <summary>
        /// UnloadContent est appelé une fois par partie. Emplacement de déchargement
        /// de tout votre contenu.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Permet au jeu d’exécuter la logique de mise à jour du monde,
        /// de vérifier les collisions, de gérer les entrées et de lire l’audio.
        /// </summary>
        /// <param name="gameTime">Fournit un aperçu des valeurs de temps.</param>
        protected override void Update(GameTime gameTime)
        {
            // Permet au jeu de se fermer
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            _keyboardState = Keyboard.GetState();
            _mouseState = Mouse.GetState();

            if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _scroller.MoveBy(new Vector2(10,0));
            }
            else if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _scroller.MoveBy(new Vector2(-10, 0));
            }

            // Switch between methods when the game state change.
            switch (ScreenState.CurrentGameState)
            {
                case ScreenState.GameState.SPLASH_SCREEN:

                break;

                case ScreenState.GameState.MENU:

                    // Clear StudioSplashScreen's component when we switch to MENU.
                    Components.Clear();

                    break;

                case ScreenState.GameState.INGAME_1:

                    // Check if we are in transition task
                    if (_transition == true)
                    {
                        // Clear StudioSplashScreen's component when we switch to MENU.
                        Components.Clear();

                        // Add Scroller's component after clear.
                        Components.Add(_scroller);

                        Components.Add(new Player(this));

                        // Disable transition
                        _transition = false;
                    }

                break;
            }

            // Updates the countdown
            _countdown.Update(gameTime);

            // Show the time left in the windows's title bar
            Window.Title = _countdown.Left.ToString();

            base.Update(gameTime);
        }

        /// <summary>
        /// Appelé quand le jeu doit se dessiner.
        /// </summary>
        /// <param name="gameTime">Fournit un aperçu des valeurs de temps.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Switch between methods when the game state change.
            switch (ScreenState.CurrentGameState)
            {
                case ScreenState.GameState.SPLASH_SCREEN:

                    GraphicsDevice.Clear(Color.White);

                break;

                case ScreenState.GameState.MENU:

                    GraphicsDevice.Clear(Color.Green);

                break;

                case ScreenState.GameState.INGAME_1:

                GraphicsDevice.Clear(Color.LightSkyBlue);

                break;

                default:

                    GraphicsDevice.Clear(Color.White);

                break;
            }

            base.Draw(gameTime);
        }
    }
}
