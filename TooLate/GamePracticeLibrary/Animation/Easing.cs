using System;
using Microsoft.Xna.Framework;
using TooLateLibrary.Timers;

namespace TooLateLibrary.Animation
{
    /// <summary>
    /// Animates a value based on time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Easing<T> : Timer
    {
        #region Fields

        /// <summary>
        /// Stores the interpolation system.
        /// </summary>
        private IInterpolation<T> _interpolation;

        /// <summary>
        /// Stores the curve that will define the animation.
        /// </summary>
        private Curve _curve;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Easing class.
        /// </summary>
        /// <param name="i">The interpolation system that indicates the type of the value.</param>
        /// <param name="c">The curve that defines the variations of the value based on time.</param>
        public Easing(IInterpolation<T> i, Curve c) : base(TimeSpan.FromSeconds(c.Keys[c.Keys.Count - 1].Position))
        {
            _interpolation = i;
            _curve = c;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current value of the variable.
        /// </summary>
        public T Value
        {
            get { return _interpolation.Interpolate(_curve.Evaluate((float) Elapsed.TotalSeconds)); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cast implicitely this class to the value type.
        /// </summary>
        /// <param name="m">The Easing class that needs to be casted.</param>
        /// <returns>The contents of the value property of the Easing class.</returns>
        public static implicit operator T(Easing<T> m)
        {
            return m.Value;
        }

        #endregion
    }
}
