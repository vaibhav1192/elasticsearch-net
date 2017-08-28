using System;
using System.Linq;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Nodes;
using Tests.Framework.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.ManagedElasticsearch.Plugins;
using Tests.Framework.ManagedElasticsearch.Tasks.InstallationTasks;

namespace Tests.Framework.ManagedElasticsearch.Clusters
{
	[RequiresPlugin(ElasticsearchPlugin.XPack)]
	public class XPackMachineLearningCluster : XPackCluster
	{
		protected override void SeedNode()
		{
			base.SeedNode();
			new MachineLearningSeeder(this.Node).SeedNode();
		}

		protected override InstallationTaskBase[] AdditionalInstallationTasks => new InstallationTaskBase[]
		{
			new DownloadMachineLearningSampleDataDistribution()
		};
	}
}
