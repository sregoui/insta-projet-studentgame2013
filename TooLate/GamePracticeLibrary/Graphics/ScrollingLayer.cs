using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TooLateLibrary.Graphics
{
    public class ScrollingLayer
    {
        #region Fields

        /// <summary>
        /// Stores the depth of the layer.
        /// </summary>
        private float _depth;

        /// <summary>
        /// Stores the top left position of the texture.
        /// </summary>
        private Vector2 _position;

        /// <summary>
        /// Stores the scroller object needed to compute the size of the scrolling layer.
        /// </summary>
        private ParallaxSideScroller _scroller;

        /// <summary>
        /// Stores the texture for the layer.
        /// </summary>
        private Texture2D _texture;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ScrollingLayer class.
        /// </summary>
        /// <param name="scroller">The ParallaxSideScroller object that contains this ScrollingLayer</param>
        /// <param name="texture">The texture for the layer</param>
        /// <param name="depth">The depth of the layer. Should not equals to 0</param>
        public ScrollingLayer(ParallaxSideScroller scroller, Texture2D texture, float depth)
        {
            _depth = depth;
            _scroller = scroller;
            _texture = texture;
            _position = Middle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the depth of the layer.
        /// </summary>
        public float Depth
        {
            get { return _depth; }
        }

        /// <summary>
        /// Gets the height of the tiled texture.
        /// </summary>
        public int Height
        {
            get { return ((_scroller.Game.Window.ClientBounds.Height/_texture.Height) + 2)*_texture.Height; }
        }

        /// <summary>
        /// Gets the position of the texture to set it in middle of the sceen, in pixels
        /// </summary>
        private Vector2 Middle
        {
            get
            {
                float x = Modulate((_scroller.Game.Window.ClientBounds.Width - _texture.Width)/2f, _texture.Width);
                float y = Modulate((_scroller.Game.Window.ClientBounds.Height - _texture.Height)/2f, _texture.Height);
                return  new Vector2(x, y);
            }
        }

        /// <summary>
        /// Gets the position of the tiled texture.
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
        }

        /// <summary>
        /// Gets the texture of the layer.
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
        }

        /// <summary>
        /// Gets the width of the tiled texture.
        /// </summary>
        public int Width
        {
            get { return ((_scroller.Game.Window.ClientBounds.Width/_texture.Width) + 2)*_texture.Width; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Modulates coordinates so the coordinates are in the [-modulo;0] insterval
        /// </summary>
        /// <param name="value">The value to modulate</param>
        /// <param name="modulo">The interval</param>
        /// <returns></returns>
        private static float Modulate(float value, float modulo)
        {
            return ((value - modulo) % modulo);
        }

        /// <summary>
        /// Move the layer in a direction, wrapping where necessary
        /// </summary>
        /// <param name="direction">The direction of the movement</param>
        public void MoveBy(Vector2 direction)
        {
            _position += direction*_depth;

            if (_scroller.ConstraintOnX)
            {
                _position.X = MathHelper.Clamp(Position.X, Middle.X + (Middle.X*_depth), Middle.X - (Middle.X*_depth));
            }

            if (_scroller.ConstraintOnY)
            {
                _position.Y = MathHelper.Clamp(Position.Y, Middle.Y + (Middle.Y*_depth), Middle.Y - (Middle.Y*_depth));
            }

            // Re-position the texture with a modulo
            _position.X = (Position.X - Texture.Width)%Texture.Width;
            _position.Y = (Position.Y - Texture.Height)%Texture.Height;
        }

        #endregion
    }
}
