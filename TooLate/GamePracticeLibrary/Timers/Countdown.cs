using Microsoft.Xna.Framework;
using System;

namespace TooLateLibrary.Timers
{
    /// <summary>
    /// Represent a timer that decrease over time, and triggers an event when time is up
    /// </summary>
    public class Countdown : TimerBase
    {
        #region Fields

        /// <summary>
        /// The _duration of the countdown
        /// </summary>
        private TimeSpan _duration;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the countdown class.
        /// </summary>
        public Countdown()
            : base()
        {
            _duration = TimeSpan.Zero;
        }

        /// <summary>
        /// Initializes a new instance of the countdown class.
        /// </summary>
        /// <param name="d">The _duration before the countdown triggers the end event</param>
        public Countdown(TimeSpan d) : base()
        {
            _duration = d;
        }

        /// <summary>
        /// Initializes a new instance of the countdown class.
        /// </summary>
        /// <param name="d">The _duration before the countdown triggers the end event</param>
        /// <param name="end">The method to trigger when the countdown hits the end</param>
        public Countdown(TimeSpan d, EventHandler end)
            : base()
        {
            _duration = d;
            End += end;
        }

        /// <summary>
        /// Initializes a new instance of the countdown class.
        /// </summary>
        /// <param name="d">The _duration before the countdown triggers the end event</param>
        /// <param name="end">The method to trigger when the countdown hits the end</param>
        /// <param name="start">Set to true if the countdown should start immediately</param>
        public Countdown(TimeSpan d, EventHandler end, bool start)
            : base(start)
        {
            _duration = d;
            End += end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the time left before the countdown expires.
        /// </summary>
        public TimeSpan Left
        {
            get { return _duration - Elapsed; }
        }

        /// <summary>
        /// Gets or sets the total time the user had before the countdown expires.
        /// </summary>
        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Called whenever the Countdown reached it's end
        /// </summary>
        public event EventHandler End;

        #endregion

        #region Methods

        /// <summary>
        /// Updates the timer.
        /// </summary>
        /// <param name="gameTime">The global game time state</param>
        public override void Update(GameTime gameTime)
        {
            // First, do the basic tasks
            base.Update(gameTime);

            // If there is no time left, trigger the End event and stop the countdown
            if (Elapsed >= Duration)
            {
                OnEnded();
                Reset();
            }
        }

        /// <summary>
        /// Event stub for End event
        /// </summary>
        protected void OnEnded()
        {
            if (End != null)
            {
                End(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}