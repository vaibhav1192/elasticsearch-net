using System;
using System.IO;
using Nest;
using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.NodeSeeders
{
	public class MachineLearningSeeder
	{
		public const string MachineLearningTestsIndexTemplateName = "server-metrics";

		private IElasticClient Client { get; }
		public string RoamingFolder { get; }

		public MachineLearningSeeder(ElasticsearchNode node)
		{
			this.RoamingFolder = node.FileSystem.RoamingFolder;
			this.Client = node.Client;
		}

		// Sometimes we run against an manually started elasticsearch when
		// writing tests to cut down on cluster startup times.
		// If template exists assume this cluster is already seeded with the machine learning data.
		private bool AlreadySeeded() => this.Client.IndexTemplateExists(MachineLearningTestsIndexTemplateName).Exists;

		public void SeedNode()
		{
			if (!TestClient.Configuration.ForceReseed && AlreadySeeded()) return;
			// Ensure a clean slate by deleting everything regardless of whether they may already exist
			this.DeleteIndicesAndTemplates();
			// and now recreate everything
			this.CreateIndicesAndSeedIndexData();
		}

		public void DeleteIndicesAndTemplates()
		{
			if (this.Client.IndexTemplateExists(MachineLearningTestsIndexTemplateName).Exists)
				this.Client.DeleteIndexTemplate(MachineLearningTestsIndexTemplateName);
		}

		private void CreateIndicesAndSeedIndexData()
		{
			this.CreateIndices();
			this.SeedIndexData();
		}

		public void CreateIndices()
		{
			CreateIndexTemplate();
			CreateMetricIndex();
		}

		private void SeedIndexData()
		{
			Console.WriteLine("Bulk importing starting ...");
			this.Client.LowLevel.Bulk<BulkResponse>(File.ReadAllBytes(Path.Combine(this.RoamingFolder, "server-metrics_1.json")));
			this.Client.LowLevel.Bulk<BulkResponse>(File.ReadAllBytes(Path.Combine(this.RoamingFolder, "server-metrics_2.json")));
			this.Client.LowLevel.Bulk<BulkResponse>(File.ReadAllBytes(Path.Combine(this.RoamingFolder, "server-metrics_3.json")));
			this.Client.LowLevel.Bulk<BulkResponse>(File.ReadAllBytes(Path.Combine(this.RoamingFolder, "server-metrics_4.json")));
			Console.WriteLine("Bulk importing finished.");
		}

		private void CreateMetricIndex()
		{
			var createProjectIndex = this.Client.CreateIndex(MachineLearningTestsIndexTemplateName, c => c
				.Mappings(map => map
					.Map<Metric>(m => m
						.AutoMap()
						.Properties(props => props
							.Keyword(t => t
								.Name(p => p.Host)
							)
							.Keyword(t => t
								.Name(p => p.Service)
							)
						)
					)
				)
			);
			createProjectIndex.ShouldBeValid();
		}

		private class Metric
		{
			public DateTime Timestamp { get; set; }

			public long Accept { get; set; }

			public long Deny { get; set; }

			public string Host { get; set; }

			public float Response { get; set; }

			public string Service { get; set; }

			public long Total { get; set; }
		}

		private void CreateIndexTemplate()
		{
			var putTemplateResult = this.Client.PutIndexTemplate(new PutIndexTemplateRequest(MachineLearningTestsIndexTemplateName)
			{
				Template = "*",
				Settings = new IndexSettings
				{
					NumberOfShards = 1,
					NumberOfReplicas = 0
				}
			});
			putTemplateResult.ShouldBeValid();
		}
	}
}
