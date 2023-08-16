using Serilog.Core;

namespace BitMod.Logging;

public interface ILogMetadataSource
{

	/// <summary>
	/// Provides a list of enrichers that will be provided to any
	/// logger constructed based off this type.
	/// </summary>
	/// <returns></returns>
	IEnumerable<ILogEventEnricher> GetMetadata();
}
