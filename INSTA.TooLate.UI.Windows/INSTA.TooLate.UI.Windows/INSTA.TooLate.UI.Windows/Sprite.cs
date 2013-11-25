using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace INSTA.TooLate.UI.Windows
{
    class Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Vector2 _direction;
        private float _speed;

        /// <summary>
        /// Récupère ou dédinit l'image du sprite
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Récupère ou définit la position du Sprite
        /// </summary>
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        /// <summary>
        /// Récupère ou définit la direction du sprite.
        /// </summary>
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// Récupère ou définit la vitesse de déplacement du sprite.
        /// </summary>
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// Initialise les variables du Sprite.
        /// </summary>
        public virtual void Initialize()
        {
            _position = Vector2.Zero;
            _direction = Vector2.Zero;
            _speed = 0;
        }

        /// <summary>
        /// Charge l'image voulue grâce au ContentManager donné.
        /// </summary>
        /// <param name="content">Le ContentManager qui chargera l'image</param>
        /// <param name="assetName">L'asset name de l'image à charger pour ce Sprite</param>
        public virtual void LoadContent(ContentManager content, string assetName)
        {
            _texture = content.Load<Texture2D>(assetName);
        }

        /// <summary>
        /// Met à jour les variable du sprite.
        /// </summary>
        /// <param name="gameTime">Le GameTime associé à la frame.</param>
        public virtual void Update(GameTime gameTime)
        {
            _position += _direction * _speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
        }

        /// <summary>
        /// Permet de gérer les entrées du joueur.
        /// </summary>
        /// <param name="keyboardState">L'état du clavier</param>
        /// <param name="mouseState">L'état de la souris à tester</param>
        public virtual void HandleInput(KeyboardState keyboardState, MouseState mouseState)
        {

        }

        /// <summary>
        /// Dessine le sprite en utilisant ses attributs et le 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //SpriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}