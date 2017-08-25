using Tests.Framework.ManagedElasticsearch.Nodes;

namespace Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks
{
	public class DownloadMachineLearningSampleDataDistribution : InstallationTaskBase
	{
		public override void Run(NodeConfiguration config, NodeFileSystem fileSystem)
		{
			var from = "https://download.elasticsearch.org/demos/machine_learning/gettingstarted/server_metrics.tar.gz";
			var to = Path.Combine(config.FileSystem.RoamingFolder, "server_metrics.tar.gz");
			if (File.Exists(to)) return;

			Console.WriteLine($"Download machine learning sample data from: {from}");
			this.DownloadFile(from, to);
			Console.WriteLine($"Downloaded machine learning sample data to: {to}");

			Console.WriteLine($"Unzipping machine learning sample data: {from} ...");
			ZipFile.ExtractToDirectory(to, fileSystem.RoamingFolder);
		}
	}
}
