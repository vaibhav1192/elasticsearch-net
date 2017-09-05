using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.XPack.MachineLearning
{
	public class DetectorSerializationTests : SerializationTestBase
	{
		[U]
		public void CanSerializeAndDeserializeAllDetectors()
		{
			var processorTypes =
				from t in typeof(IDetector).Assembly().Types()
				where typeof(IDetector).IsAssignableFrom(t) && !t.IsAbstract()
				select t;

			var detectors = processorTypes
				.Select(processorType => (IDetector)Activator.CreateInstance(processorType))
				.ToList();

			var analysisConfig = new AnalysisConfig { Detectors = detectors };
			var deserialized = this.Deserialize<AnalysisConfig>(this.Serialize(analysisConfig));

			deserialized.Detectors.Should().HaveCount(detectors.Count);
			deserialized.Detectors.Select(p => p.Function).Distinct().Should().HaveCount(detectors.Count);
		}
	}
}
