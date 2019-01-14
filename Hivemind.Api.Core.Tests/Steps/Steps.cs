using System;
using System.Configuration;

namespace Hivemind.Api.Core.Tests.Steps
{
    public class Steps : TechTalk.SpecFlow.Steps
    {
        protected Context _context;
        protected string _basePath;

        public Steps(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _basePath = ConfigurationManager.AppSettings.Get("webApiPath");
        }
    }
}
