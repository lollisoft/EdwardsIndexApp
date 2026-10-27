using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lollisoft.Common.Logging.Interfaces;

namespace Lollisoft.Common.Logging.Trace
{
    public static class DatabaseTracerFactory
    {
        static IDatabaseTracer Instance { get; set; }

        static public IDatabaseTracer GetTracer()
        {
            if (Instance == null)
                Instance = new NullDatabaseTracer();

            return Instance;
        }
		
		static public void SetTracer(IDatabaseTracer tracer) {
			Instance = tracer;
		}
    }
}
