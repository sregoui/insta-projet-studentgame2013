using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TooLateLibrary.Graphics
{
    /// <summary>
    /// This represet the base DrawableGameComponent for the game.
    /// </summary>
    public class Base2DGameComponent : DrawableGameComponent
    {

        #region Fields

        /// <summary>
        /// Stores the class will do the rendering.
        /// </summary>
        private SpriteBatch _renderer;

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the Base2DGameComponent class.
        /// </summary>
        /// <param name="game">The game that will use this DrawableGameComponent.</param>
        public Base2DGameComponent(Game game) : base(game)
        {
            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the ContentManager that will provide assets loading functions.
        /// </summary>
        public ContentManager Content
        {
            get { return Game.Content; }
        }

        public SpriteBatch Renderer
        {
            get { return _renderer; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Load the component assets and graphics objects.
        /// </summary>
        protected override void LoadContent()
        {
            // Initializes the Sprite batch that will render 2D stuff on screen.
            _renderer = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }

        #endregion
    }
}
