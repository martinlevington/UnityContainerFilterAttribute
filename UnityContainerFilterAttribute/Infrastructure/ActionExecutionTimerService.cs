using System.Diagnostics;

namespace UnityContainerFilterAttribute.Infrastructure
{
    public class ActionExecutionTimerService : IActionExecutionTimerService
    {

        private readonly Stopwatch _stopWatch = new Stopwatch();

        public void Reset()
        {
            _stopWatch.Reset();
        }

        public void Start()
        {
            _stopWatch.Start();
        }

        public void Stop()
        {
            _stopWatch.Stop();
        }

        public long ElapsedMilliseconds => _stopWatch.ElapsedMilliseconds;


    }
}