using System;
using System.Net;

using BitMod.Configuration.Model;

namespace BitMod.Public;

public interface IConfigurationSystem : IBitModComponent
{

	/// <summary>
	/// Get the configuration object for
	/// the specified file.
	/// </summary>
	/// <returns></returns>
	IConfigObject Get(string name);

	/// <summary>
	/// Create a fake symbol with the provided string value.
	/// </summary>
	/// <param name="symbol"></param>
	/// <returns></returns>
	IConfigSymbol Fake(string symbol);
}
