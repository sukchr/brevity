using System;

namespace Brevity
{
    public class Stopwatch : IDisposable 
    {
        private readonly Action<TimeSpan> _elapsed;
        private readonly System.Diagnostics.Stopwatch _stopwatch = new System.Diagnostics.Stopwatch();

        public Stopwatch(Action<TimeSpan> elapsed)
        {
            if (elapsed == null) throw new ArgumentNullException("elapsed");
            _elapsed = elapsed;
            _stopwatch.Start();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _elapsed(_stopwatch.Elapsed);
        }
    }
}