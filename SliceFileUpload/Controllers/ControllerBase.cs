using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using SliceFileUpload.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SliceFileUpload.Controllers
{
    public class ControllerBase : Controller
    {
        private readonly IOptions<ConfigSettingModel> _ConfigSettings;
        private readonly ILogger<Controller> _logger;
        public ControllerBase(IOptions<ConfigSettingModel> configSettings,
            ILogger<Controller> logger)
        {
            _ConfigSettings = configSettings;
            _logger = logger;
        }
    }
}
