using System;

namespace Tests.Framework.MockData
{
	public class Metric
	{
		public DateTime Timestamp { get; set; }

		public long Accept { get; set; }

		public long Deny { get; set; }

		public string Host { get; set; }

		public float Response { get; set; }

		public string Service { get; set; }

		public long Total { get; set; }
	}
}
