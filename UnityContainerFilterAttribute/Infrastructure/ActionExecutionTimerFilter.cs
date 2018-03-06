using System;
using System.Web.Mvc;
using UnityContainerFilterAttribute.ViewModels;

namespace UnityContainerFilterAttribute.Infrastructure
{
    public class ActionExecutionTimerFilter : IActionFilter
    {

        private readonly IActionExecutionTimerService _actionExecutionTimerService ;


        public ActionExecutionTimerFilter()
        {

        }

        public ActionExecutionTimerFilter(IActionExecutionTimerService actionExecutionTimer)
        {
            _actionExecutionTimerService = actionExecutionTimer;
        }

        public  void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _actionExecutionTimerService.Reset();
            _actionExecutionTimerService.Start();
        }

        public  void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _actionExecutionTimerService.Stop();

            if (filterContext == null)
            {
                throw new ArgumentNullException(nameof(filterContext));
            }

            if (!(filterContext.Controller.ViewData.Model is BaseLayout viewModel))
            {
                return;
            }

            viewModel.ExecutionTime = _actionExecutionTimerService.ElapsedMilliseconds;;

        }
    }
}