namespace TooLateLibrary.Animation
{
    public class FloatInterpolation : IInterpolation<float>
    {
        #region Fields

        /// <summary>
        /// Stores the minimum value.
        /// </summary>
        private float _min;

        /// <summary>
        /// Stores the maximum value.
        /// </summary>
        private float _max;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the FloatInterpolation class.
        /// </summary>
        public FloatInterpolation()
        {
            _min = 0;
            _max = 1;
        }

        /// <summary>
        /// Creates a new instance of the FloatInterpolation class.
        /// </summary>
        /// <param name="minimum">The minimum value for the interpolation.</param>
        /// <param name="maximum">The maximum value for the interpolation.</param>
        public FloatInterpolation(float minimum, float maximum)
        {
            _min = minimum;
            _max = maximum;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets of sets the minimum for the interpolation system.
        /// </summary>
        public float Min
        {
            get { return _min; }
            set { _min = value; }
        }

        /// <summary>
        /// Gets of sets the maximum value for the interpolation system.
        /// </summary>
        public float Max
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
        public float Interpolate(float value)
        {
            return _min + (_max - _min) * value;
        }

        #endregion
    }
}
