using BitMod.Internal;

using Lilikoi.Attributes.Static;
using Lilikoi.Attributes.Typed;
using Lilikoi.Compiler.Public;
using Lilikoi.Context;

using Serilog;

namespace BitMod.Attributes.Mutators;

public class LogAttribute : LkMutatorAttribute
{
	public class LogInjector : LkTypedParameterAttribute<ILogger, object>
	{

		private Type _type;
		private string _method;

		private string _metadata;

		public LogInjector(Type type, string method)
		{
			_type = type;
			_method = method;

			_metadata = $"{_type.FullName}::{_method}";
		}

		public override ILogger Inject(Mount context, object input)
		{
			var logger = context.Get<ILogger>()
				.ForContext("Method", _metadata);

			return logger;
		}
	}

	public override void Mutate(LilikoiMutator mutator)
		=> mutator.Wildcard<ILogger>(new LogInjector(mutator.Host, mutator.Method.Name));
}
