using System.Net;

using BitMod.Configuration.Model;

namespace BitMod.Whitelist.Adapter;

public class WhitelistConfigAdapter
{
	private IConfigObject _whitelist;

	public WhitelistConfigAdapter(IConfigObject whitelist)
	{
		_whitelist = whitelist;
	}

	public IEnumerable<IPAddress> GetAllowedAddresses()
		=> _whitelist.AsList()
			.Select(model => model as IConfigSymbol)
			.Select(symbol => symbol?.AsAddress)
			.Where(symbol => symbol != null)!;

}
