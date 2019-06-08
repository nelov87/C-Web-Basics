using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChronometerDemo
{
    public class Cronometer : IChronometer
    {
        private long milisecunds = 0;
       
        private bool IsRunning = false;
        private int count = 0;
        public string GetTime
        {
            get
            {
                return $"{(milisecunds / 6000):d2} : {(milisecunds / 1000):d2} : {(milisecunds % 1000):d2}";
            }
            
        }

        public List<string> Laps { get; private set; }
        

        public Cronometer()
        {
            this.Reset();
        }
        
        public string Lap()
        {
            string lap = $"{this.count}. {this.GetTime}";
            this.Laps.Add(lap);
            this.count++;

            return lap;
        }

        public void Reset()
        {
            this.Stop();
            this.Laps = new List<string>();
            this.milisecunds = 0;
            this.count = 0;
        }

        public void Start()
        {
            this.Reset();
            this.IsRunning = true;
            

            Task.Run(() => 
            {
                while (this.IsRunning)
                {
                    this.IncrementTime();
                    Thread.Sleep(1);
                }
            });
        }

        public void Stop()
        {

            this.IsRunning = false;
        }

        private void IncrementTime()
        {
            this.milisecunds++;
        }
    }
}
