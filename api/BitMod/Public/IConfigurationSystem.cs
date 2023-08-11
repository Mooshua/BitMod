using System.Net;

namespace BitMod.Public;

public interface IConfigurationSystem : IBitModComponent
{

	/// <summary>
	/// Get the configuration class
	/// </summary>
	/// <param name="makeDefault"></param>
	/// <typeparam name="T"></typeparam>
	/// <returns></returns>
	T Get<T>(Func<T> makeDefault)
		where T: class;

}
