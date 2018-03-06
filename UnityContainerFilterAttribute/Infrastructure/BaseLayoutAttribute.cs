using System;
using System.Web.Mvc;
using AutoMapper;
using UnityContainerFilterAttribute.ViewModels;

namespace UnityContainerFilterAttribute.Infrastructure
{
    public class BaseLayoutAttribute : ActionFilterAttribute
    {
        private IMapper _mapper;


        public BaseLayoutAttribute()
        {
        }

        public BaseLayoutAttribute(IMapper mapper)
        {
            _mapper = mapper;
        }

        public override void OnResultExecuting(ResultExecutingContext filtercontext)
        {
            if (filtercontext == null)
            {
                throw new ArgumentNullException(nameof(filtercontext));
            }

            if (!(filtercontext.Controller.ViewData.Model is BaseLayout viewModel))
            {
                return;
            }

            viewModel.BaseName = filtercontext.Controller.ToString();

        }

    }
}