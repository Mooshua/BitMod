using System;
using System.Collections.Generic;

using Lilikoi.Compiler.Public;

namespace BitMod.Router;

public abstract class BaseRouter
{
	public abstract Type Search { get; }

	public abstract Type Result { get; }

	public bool Is<TResult, TSearch>()
		=> (typeof(TSearch) == Search && typeof(TResult) == Result);

	public Router<TResult, TSearch>? As<TResult, TSearch>()
		where TResult : class
	{
		if (Is<TResult, TSearch>())
			return this as Router<TResult, TSearch>;

		return null;
	}


	internal abstract void Add(string name, IEnumerable<LilikoiContainer> containers);

	internal void Add(string name, LilikoiContainer containers)
		=> Add(name, new LilikoiContainer[1] { containers });

	internal abstract void Remove(string name);
}
