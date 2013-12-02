using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TooLateLibrary.Graphics
{
    public class ParallaxSideScroller : Base2DGameComponent
    {
        #region Fields

        /// <summary>
        /// Does the scrolling component allow to loop texture horizontally?
        /// </summary>
        private bool _constraintOnX;

        /// <summary>
        /// Does the scrolling component allo to loop texture vertically?
        /// </summary>
        private bool _constraintOnY;

        /// <summary>
        /// Stores the list of layers
        /// </summary>
        private List<ScrollingLayer> _layers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ParallaxSideScroller class.
        /// </summary>
        /// <param name="game">The game class that will use the ParallaxSideScroller</param>
        /// <param name="cX">Should the X movement be constrained?</param>
        /// <param name="cY">Should the Y movement be constrained?</param>
        public ParallaxSideScroller(Game game, bool cX, bool cY) : base(game)
        {
            _layers = new List<ScrollingLayer>();

            _constraintOnX = cX;
            _constraintOnY = cY;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whenever the component allow a texture loop on X-Axis.
        /// </summary>
        public bool ConstraintOnX
        {
            get { return _constraintOnX; }
        }

        /// <summary>
        /// Gets a value indicating whenever the component allow a texture loop on Y-Axis.
        /// </summary>
        public bool ConstraintOnY
        {
            get { return _constraintOnY; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new layer to the parallax side scroller.
        /// </summary>
        /// <param name="asset">The texture asset name</param>
        public void Add(string asset)
        {
            // Add the layer
            _layers.Add(new ScrollingLayer(this, Content.Load<Texture2D>(asset), 1f / (float)Math.Pow(2, _layers.Count)));

            // Sort the layers by Depth property
            _layers.Sort(delegate(ScrollingLayer a, ScrollingLayer b)
            {
                return a.Depth > b.Depth ? 1 : a.Depth < b.Depth ? -1 : 0;
            });
        }

        /// <summary>
        /// Draws all layers
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of game times values.</param>
        public override void Draw(GameTime gameTime)
        {
            Renderer.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            // Loop each layer and renders them
            foreach (ScrollingLayer layer in _layers)
            {
                Renderer.Draw(layer.Texture, layer.Position, new Rectangle(0, 0, layer.Width, layer.Height), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 1.0f);
            }

            // Stop the rendering sequence.
            Renderer.End();
        }

        public void MoveBy(Vector2 direction)
        {
            foreach (ScrollingLayer layer in _layers)
            {
                layer.MoveBy(direction);
            }
        }

        #endregion
    }
}
