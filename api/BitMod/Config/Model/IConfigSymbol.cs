using System.Net;

namespace BitMod.Configuration.Model;

public interface IConfigSymbol : IConfigModel
{

	/// <summary>
	/// The symbol, expressed as a string
	/// </summary>
	string Symbol { get; }

	/// <summary>
	/// The symbol, expressed as an IPAddress.
	/// Will be null if parsing fails.
	/// </summary>
	IPAddress? AsAddress { get; }

	/// <summary>
	/// The symbol, parsed into a number.
	/// Will be null if parsing fails.
	/// </summary>
	long? AsInt { get; }

	/// <summary>
	/// The symbol, parsed into a floating-point number.
	/// Will be null if parsing fails
	/// </summary>
	double? AsFloat { get; }

	/// <summary>
	/// The symbol, parsed into a boolean.
	/// </summary>
	bool? AsBoolean { get; }

}
