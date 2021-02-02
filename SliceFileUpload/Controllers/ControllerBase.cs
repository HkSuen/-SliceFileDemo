using Microsoft.AspNetCore.Http;
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


        protected IEnumerable<IFormFile> _GetFiles
        {
            get
            {
                return Request.Form.Files;
            }
        }

        protected Dictionary<string, object> _GetFormData
        {
            get
            {
                Dictionary<string, object> formDatas = null;
                if (Request.Form.Keys.Count > 0)
                {
                    formDatas = new Dictionary<string, object>();
                    foreach (string _key in Request.Form.Keys)
                    {
                        formDatas.Add(_key,Request.Form[_key]);
                    }
                }
                return formDatas;
            }
        }

    }
}
