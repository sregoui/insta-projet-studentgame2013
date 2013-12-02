using Microsoft.Xna.Framework;

namespace TooLateLibrary
{
    public class Vector3Interpolation : IInterpolation<Vector3>
    {
        #region Fields

        /// <summary>
        /// Stores the minimum value.
        /// </summary>
        private Vector3 _min;

        /// <summary>
        /// Stores the maximum value.
        /// </summary>
        private Vector3 _max;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the Vector2Interpolation class.
        /// </summary>
        /// <param name="minimum">The minimum value for the interpolation.</param>
        /// <param name="maximum">The maximum value for the interpolation.</param>
        public Vector3Interpolation(Vector3 minimum, Vector3 maximum)
        {
            _min = minimum;
            _max = maximum;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets of sets the minimum for the interpolation system.
        /// </summary>
        public Vector3 Min
        {
            get { return _min; }
            set { _min = value; }
        }

        /// <summary>
        /// Gets of sets the maximum value for the interpolation system.
        /// </summary>
        public Vector3 Max
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
        public Vector3 Interpolate(float value)
        {
            return Vector3.Lerp(_min, _max, value);
        }

        #endregion
    }
}
