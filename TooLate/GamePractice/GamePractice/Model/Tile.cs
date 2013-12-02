using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TooLateLibrary.Graphics;

namespace TooLate.Model
{
    /// <summary>
    /// Controls the collision detection and response behavior of a tile.
    /// </summary>
    public enum TileCollision
    {
        /// <summary>
        /// A passable tile is one which does not hinder player motion at all.
        /// </summary>
        Passable = 0,

        /// <summary>
        /// An impassable tile is one which does not allow the player to move through
        /// it at all. It is completely solid.
        /// </summary>
        Impassable = 1,

        /// <summary>
        /// A platform tile is one which behaves like a passable tile except when the
        /// player is above it. A player can jump up through a platform as well as move
        /// past it to the left and right, but can not fall down through the top of it.
        /// </summary>
        Platform = 2,
    }

    public struct Tile
    {
        public const int width = 40;
        public const int height = 32;

        public TileCollision collision;
        public Sprite texture;

        public static readonly Vector2 size = new Vector2(width, height);

        /// <summary>
        /// Constructs a new tile.
        /// </summary>
        /// <param name="t">The texture.</param>
        /// <param name="c">The collision.</param>
        public Tile(Sprite t, TileCollision c)
        {
            texture = t;
            collision = c;
        }


    }
}
