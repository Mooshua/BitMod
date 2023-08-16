namespace BitMod.Configuration.Model;

public interface IConfigObject : IConfigModel, IReadOnlyCollection<KeyValuePair<IConfigSymbol, IConfigModel>>
{
	/// <summary>
	/// Get the configuration model at the specified key.
	/// The model is boxed into IConfigModel, and can
	/// be dynamically unboxed to learn it's type.
	/// </summary>
	/// <param name="key"></param>
	/// <returns></returns>
	IConfigModel? Get(string key);

	/// <summary>
	/// Get the configuration model at the specified key,
	/// if it exists.
	/// </summary>
	/// <param name="key"></param>
	/// <typeparam name="TModel"></typeparam>
	/// <returns></returns>
	TModel? Get<TModel>(string key)
		where TModel : class, IConfigModel;

	/// <summary>
	/// Gets a symbol based on the string key provided.
	/// </summary>
	/// <param name="key"></param>
	/// <param name="fallback"></param>
	/// <returns></returns>
	IConfigSymbol? GetSymbol(string key, string? fallback = null);

	/// <summary>
	/// Gets a model based on it's string index.
	/// This is internally the same as <see cref="Get"/>.
	/// </summary>
	/// <param name="index"></param>
	IConfigModel? this[string index] => Get(index);

	/// <summary>
	/// Interpret all entries as keys and values in a list
	/// </summary>
	/// <returns></returns>
	IEnumerable<IConfigModel> AsList();
}
