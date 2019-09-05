using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ThemeFileEditor
{
    class ThemeTransition
    {
        private Timer timer = new Timer();

        public ThemeFile FromTheme { get; }
        public ThemeFile ToTheme { get; }

        private ThemeFile CurrentState { get; set; }

        public ThemeTransition(ThemeFile FromTheme, ThemeFile ToTheme)
        {
            this.FromTheme = FromTheme;
            this.ToTheme = ToTheme;
            timer.Elapsed += Tick;
        }

        public void Transition(int seconds, int stages)
        {
            timer.Interval = Math.Round(seconds * 1000.0 / stages);
            timer.Enabled = true;
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            //get transition state
            
            //tick event?

            //transition finisehd event
        }

        //GetCurrentState


    }
}
