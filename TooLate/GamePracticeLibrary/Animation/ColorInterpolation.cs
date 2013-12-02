using Microsoft.Xna.Framework;

namespace TooLateLibrary.Animation
{
    public class ColorInterpolation : IInterpolation<Color>
    {
        #region Fields

        /// <summary>
        /// Stores the minimum value.
        /// </summary>
        private Color _min;

        /// <summary>
        /// Stores the maximum value.
        /// </summary>
        private Color _max;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the FloatInterpolation class.
        /// </summary>
        /// <param name="minimum">The minimum value for the interpolation.</param>
        /// <param name="maximum">The maximum value for the interpolation.</param>
        public ColorInterpolation(Color minimum, Color maximum)
        {
            _min = minimum;
            _max = maximum;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets of sets the minimum for the interpolation system.
        /// </summary>
        public Color Min
        {
            get { return _min; }
            set { _min = value; }
        }

        /// <summary>
        /// Gets of sets the maximum value for the interpolation system.
        /// </summary>
        public Color Max
        {
            get { return _max; }
            set { _max = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Interpolates linearly the Color value in function of an input parameter.
        /// </summary>
        /// <param name="value">The input parameter, between 0 and 1</param>
        /// <returns>The interpolated value, between the minimum and the maximum</returns>
        public Color Interpolate(float value)
        {
            return Color.Lerp(_min, _max, MathHelper.Clamp(value, 0, 1));
        }

        #endregion
    }
}
