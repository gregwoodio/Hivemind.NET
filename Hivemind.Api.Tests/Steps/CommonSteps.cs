using System;
using System.Configuration;

namespace Hivemind.Api.Tests.Steps
{
    public class CommonSteps
    {
        protected Context _context;
        protected string _basePath;

        public CommonSteps(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _basePath = ConfigurationManager.AppSettings.Get("webApiPath");
        }
    }
}
