using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using $safeprojectname$.Resources;

namespace $safeprojectname$._.Controllers
{
    [ApiController]
    public class ApiControllerBase<T> : ControllerBase
    {
        protected readonly IStringLocalizer<Resource> localizer;
        protected readonly ILogger<T> logger;

        public ApiControllerBase(IStringLocalizer<Resource> localizer, ILogger<T> logger)
        {
            this.localizer = localizer;
            this.logger = logger;
        }
    }
}
