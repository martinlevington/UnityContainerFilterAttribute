namespace UnityContainerFilterAttribute.Infrastructure
{
    public interface IActionExecutionTimerService
    {
        void Reset();
        void Start();
        void Stop();
        long ElapsedMilliseconds { get; }
    }
}