using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;

namespace TooLateLibrary.Timers
{
    /// <summary>
    /// Represent a timer interface within the game
    /// </summary>
    public abstract class TimerBase : IUpdateable
    {
        #region Fields

        /// <summary>
        /// Stores the internal time value
        /// </summary>
        private TimeSpan _elapsed;

        /// <summary>
        /// Stores a value indicating whenever the timer is running or not
        /// </summary>
        private bool _enabled;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TimerBase class
        /// </summary>
        public TimerBase()
        {
            _elapsed = TimeSpan.Zero;
            _enabled = false;
        }

        /// <summary>
        /// Initializes a new instance of the TimerBase class
        /// </summary>
        /// <param name="start">Set to true if the timer should start immediately</param>
        public TimerBase(bool start)
        {
            _elapsed = TimeSpan.Zero;
            _enabled = false;

            if (start)
            {
                Start();
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the total _elapsed time measured by the current instance.
        /// </summary>
        public TimeSpan Elapsed
        {
            get { return _elapsed; }
        }

        /// <summary>
        /// Gets a value indicating whether the timer is running.
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            private set
            {
                _enabled = value;
                OnEnableChange();
            }
        }

        /// <summary>
        /// Gets the update order of the Timer
        /// </summary>
        public int UpdateOrder
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Events

        /// <summary>
        /// Event called whenever the _enabled status has changed.
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged;

        /// <summary>
        /// Update order is not used at the moment, this event will never be called.
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;

        /// <summary>
        /// Event called whenever the timer starts.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Event called whenever the timer stops.
        /// </summary>
        public event EventHandler Stopped;

        #endregion

        #region Methods

        /// <summary>
        /// Stops time interval measurement and resets the _elapsed time to zero.
        /// </summary>
        public void Reset()
        {
            Enabled = false;
            _elapsed = TimeSpan.Zero;
        }

        /// <summary>
        /// Starts, or resumes, measuring _elapsed time for an interval.
        /// </summary>
        public void Start()
        {
            Enabled = true;
            OnStart();
        }

        /// <summary>
        /// Stops measuring _elapsed time for an interval.
        /// </summary>
        public void Stop()
        {
            Enabled = false;
            OnStop();
        }

        /// <summary>
        /// Updates the timer.
        /// </summary>
        /// <param name="gameTime">The global game time state</param>
        public virtual void Update(GameTime gameTime)
        {
            // Only update internals if the timer is running
            if (_enabled)
            {
                _elapsed += gameTime.ElapsedGameTime;
            }
        }

        /// <summary>
        /// Method stub for EnabledChanged event
        /// </summary>
        protected virtual void OnEnableChange()
        {
            if (EnabledChanged != null)
            {
                EnabledChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Event stub for Start event.
        /// </summary>
        protected virtual void OnStart()
        {
            if (Started != null)
            {
                Started(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Event stub for Stop event.
        /// </summary>
        protected virtual void OnStop()
        {
            if (Stopped != null)
            {
                Stopped(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
