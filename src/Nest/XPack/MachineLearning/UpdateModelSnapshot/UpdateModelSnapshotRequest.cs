using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A request to update a Machine Learning model snapshot.
	/// </summary>
	public partial interface IUpdateModelSnapshotRequest
	{
		/// <summary>
		/// An optional description of the model snapshot. For example, "Before black friday".
		/// </summary>
		[JsonProperty("description")]
		string Description { get; set; }

		/// <summary>
		/// If true, this snapshot will not be deleted during automatic cleanup of snapshots older than model_snapshot_retention_days.
		/// Note that this snapshot will still be deleted when the job is deleted.
		/// </summary>
		[JsonProperty("retain")]
		bool Retain { get; set; }
	}

	/// <inheritdoc />
	public partial class UpdateModelSnapshotRequest
	{
		/// <inheritdoc />
		string Description { get; set; }

		/// <inheritdoc />
		bool Retain { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlUpdateModelSnapshot")]
	public partial class UpdateModelSnapshotDescriptor
	{
		string IUpdateModelSnapshotRequest.Description { get; set; }
		bool IUpdateModelSnapshotRequest.Retain { get; set; }

		/// <inheritdoc />
		public UpdateModelSnapshotDescriptor Description(string description) =>
			Assign(a => a.Description = description);

		/// <inheritdoc />
		public UpdateModelSnapshotDescriptor Retain(bool retain) =>
			Assign(a => a.Retain = retain);
	}
}
