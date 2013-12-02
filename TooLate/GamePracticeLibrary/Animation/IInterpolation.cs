using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TooLateLibrary
{
    /// <summary>
    /// Provides a system to linearly interpolate two values of a type T.
    /// </summary>
    /// <typeparam name="T">The return value type of the interpolation system</typeparam>
    public interface IInterpolation<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Minimum value for this interpolation system.
        /// </summary>
        T Min { get; set; }

        /// <summary>
        /// Gets or sets the Maximum value for this interpolation system.
        /// </summary>
        T Max { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Interpolates linearly the middle value between the Min and the Max values.
        /// </summary>
        /// <param name="value">The interpolation value, between 0 and 1.</param>
        /// <returns>The interpolated value between Min and Max.</returns>
        T Interpolate(float value);

        #endregion
    }
}
