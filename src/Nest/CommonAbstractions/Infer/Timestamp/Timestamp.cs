using System;
using System.Globalization;
using Elasticsearch.Net;

namespace Nest
{
	public class Timestamp : IUrlParameter, IEquatable<Timestamp>
	{
		private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

		private readonly double _secondsSinceEpoch;

		public Timestamp(DateTime dateTime) => _secondsSinceEpoch = (dateTime - Epoch).TotalSeconds;
		public Timestamp(DateTimeOffset dateTimeOffset) => _secondsSinceEpoch = (dateTimeOffset.DateTime - Epoch).TotalSeconds;
		public Timestamp(long secondsSinceEpoch) => _secondsSinceEpoch = secondsSinceEpoch;

		public static implicit operator Timestamp(DateTime dateTime) => new Timestamp(dateTime);
		public static implicit operator Timestamp(DateTimeOffset dateTimeOffset) => new Timestamp(dateTimeOffset);
		public static implicit operator Timestamp(long secondsSinceEpoch) => new Timestamp(secondsSinceEpoch);

		public string GetString(IConnectionConfigurationValues settings) => this.ToString();

		public override string ToString() =>
			_secondsSinceEpoch.ToString(CultureInfo.InvariantCulture);

		public bool Equals(Timestamp other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return _secondsSinceEpoch.Equals(other._secondsSinceEpoch);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Timestamp) obj);
		}

		public override int GetHashCode() => _secondsSinceEpoch.GetHashCode();

		public static bool operator ==(Timestamp left, Timestamp right) => Equals(left, right);

		public static bool operator !=(Timestamp left, Timestamp right) => !Equals(left, right);
	}
}
