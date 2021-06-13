using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Constants
{
	public static class EventBusConstants
	{
		public const string SignalEvenQueue = "feature-queue";
		public const string HostAddress = "amqp://guest:guest@localhost:5672";		 
	}
}
