using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IPostJobDataRequest
	{
		[JsonIgnore]
		IEnumerable<object> Data { get; set; }
	}

	/// <summary>
	///
	/// </summary>
	[JsonConverter(typeof(PostJobDataConverter))]
	public partial interface IPostJobDataRequest<T> : IPostJobDataRequest
	{
		[JsonIgnore]
		IEnumerable<T> Data { get; set; }
	}

	/// <inheritdoc />
	public partial class PostJobDataRequest<T> : IPostJobDataRequest<T>
	{
		IEnumerable<object> IPostJobDataRequest.Data
		{
			get => Data.Cast<object>();
			set => Data = value.Cast<T>();
		}

		/// <inheritdoc />
		public IEnumerable<T> Data { get; set; }
	}

	/// <inheritdoc />
	[DescriptorFor("XpackMlPostData")]
	public partial class PostJobDataDescriptor<T> : IPostJobDataRequest<T>
	{
		IEnumerable<T> IPostJobDataRequest<T>.Data { get; set; }
		IEnumerable<object> IPostJobDataRequest.Data { get; set; }

		/// <inheritdoc />
		public PostJobDataDescriptor<T> Data(IEnumerable<T> data) => Assign(a => a.Data = data);
	}

	internal class PostJobDataConverter : JsonConverter
	{
		public override bool CanRead => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var request = (IPostJobDataRequest) value;
			if (request == null)
			{
				writer.WriteNull();
				return;
			}

			var settings = serializer.GetConnectionSettings();
			var elasticsearchSerializer = settings.Serializer;
			foreach (var data in request.Data)
			{
				var bodyJson = elasticsearchSerializer.SerializeToString(data, SerializationFormatting.None);
				writer.WriteRaw(bodyJson + "\n");
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			throw new NotSupportedException();

		public override bool CanConvert(Type objectType) => true;
	}
}
