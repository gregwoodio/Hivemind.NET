using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Hivemind.Api.Tests.Steps
{
    public class CommonSteps
    {
        protected Context _context;
        protected string _basePath;

        public CommonSteps(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _basePath = Properties.Resource.webApiPath;
        }
    }
}
