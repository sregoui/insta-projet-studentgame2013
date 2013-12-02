using Microsoft.Xna.Framework;

namespace TooLateLibrary
{
    public class Vector4Interpolation : IInterpolation<Vector4>
    {
        #region Fields

        /// <summary>
        /// Stores the minimum value.
        /// </summary>
        private Vector4 _min;

        /// <summary>
        /// Stores the maximum value.
        /// </summary>
        private Vector4 _max;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the Vector2Interpolation class.
        /// </summary>
        /// <param name="minimum">The minimum value for the interpolation.</param>
        /// <param name="maximum">The maximum value for the interpolation.</param>
        public Vector4Interpolation(Vector4 minimum, Vector4 maximum)
        {
            _min = minimum;
            _max = maximum;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets of sets the minimum for the interpolation system.
        /// </summary>
        public Vector4 Min
        {
            get { return _min; }
            set { _min = value; }
        }

        /// <summary>
        /// Gets of sets the maximum value for the interpolation system.
        /// </summary>
        public Vector4 Max
        {
            get { return _max; }
            set { _max = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Interpolates linearly the float value in function of an input parameter.
        /// </summary>
        /// <param name="value">The input parameter, between 0 and 1</param>
        /// <returns>The interpolated value, between the minimum and the maximum</returns>
        public Vector4 Interpolate(float value)
        {
            return Vector4.Lerp(_min, _max, value);
        }

        #endregion
    }
}
