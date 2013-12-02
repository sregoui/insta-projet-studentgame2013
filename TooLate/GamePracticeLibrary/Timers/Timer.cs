using Microsoft.Xna.Framework;
using System;

namespace TooLateLibrary.Timers
{
    /// <summary>
    /// Represent a timer that call a method every N seconds
    /// </summary>
    public class Timer : TimerBase
    {
        #region Fields

        /// <summary>
        /// The time elapsed since last tick
        /// </summary>
        private TimeSpan _passed;

        /// <summary>
        /// The duration of the countdown
        /// </summary>
        private TimeSpan _interval;

        /// <summary>
        /// Should the Timer shutdown on the next tick?
        /// </summary>
        private bool _shutdown;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Time class.
        /// </summary>
        public Timer() : base()
        {
            _interval = TimeSpan.Zero;
        }

        /// <summary>
        /// Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="i">The interval between each Tick event</param>
        public Timer(TimeSpan i) : base()
        {
            _interval = i;
        }

        /// <summary>
        /// Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="i">The interval between each Tick event</param>
        /// <param name="t">The method to trigger when the countdown hits the interval period</param>
        public Timer(TimeSpan i, EventHandler t) : base()
        {
            _interval = i;
            Tick += t;
        }

        /// <summary>
        /// Initializes a new instance of the Timer class.
        /// </summary>
        /// <param name="i">The interval between each Tick event</param>
        /// <param name="t">The method to trigger when the countdown hits the interval period</param>
        /// <param name="start">Set to true if the countdown should start immediately</param>
        public Timer(TimeSpan i, EventHandler t, bool start) : base(start)
        {
            _interval = i;
            Tick += t;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the time left before the next tick.
        /// </summary>
        public TimeSpan Left
        {
            get { return _interval - _passed; }
        }

        /// <summary>
        /// Gets or sets the interval between each tick.
        /// </summary>
        public TimeSpan Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }

        /// <summary>
        /// Gets the time elapsed since last tick.
        /// </summary>
        public TimeSpan Passed
        {
            get { return _passed; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Called whenever the Countdown reached it's end
        /// </summary>
        public event EventHandler Tick;

        #endregion

        #region Methods

        /// <summary>
        /// Updates the timer.
        /// </summary>
        /// <param name="gameTime">The global game time state</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // If the time is running, update the passed time.
            if (Enabled)
            {
                _passed += gameTime.ElapsedGameTime;
            }

            // If we passed the interval, call the tick method, and decrease the passed value
            while (Passed >= Interval)
            {
                _passed -= Interval;
                OnTicked();
            }
        }

        /// <summary>
        /// Event stub for Tick event.
        /// </summary>
        protected void OnTicked()
        {
            // Call the tick event if any method is associated.
            if (Tick != null)
            {
                Tick(this, EventArgs.Empty);
            }

            // The user requested a shutdown of the timer?
            if (_shutdown == true)
            {
                Stop();
            }
        }

        /// <summary>
        /// Stops the timer on the next tick.
        /// </summary>
        public void Shutdown()
        {
            _shutdown = true;
        }

        #endregion
    }
}
