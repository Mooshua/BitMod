using BitMod.Extensions.Commands.Lilikoi;

using Lilikoi.Attributes;
using Lilikoi.Context;

namespace BitMod.Extensions.Commands.Attributes;

public class ArgAttribute : LkParameterAttribute
{
	public ArgAttribute(int index)
	{
		_index = index;
	}

	private int _index;

	private static List<Type> _types = new List<Type>()
	{
		typeof(bool),
		typeof(byte),

		typeof(char),
		typeof(string),

		typeof(float),
		typeof(double),

		typeof(long),
		typeof(int),
		typeof(short),
		typeof(ulong),
		typeof(uint),
		typeof(ushort),
	};

	public override bool IsInjectable<TParameter, TInput>(Mount mount)
	{
		if (typeof(TInput) != typeof(CommandInput))
			return false;

		foreach (Type type in _types)
		{
			if (typeof(TInput).IsAssignableTo(type))
				return true;
		}
		return false;
	}

	public override TParameter Inject<TParameter, TInput>(Mount context, TInput input)
	{
		if (!(input is CommandInput))
			throw new InvalidOperationException("Input to ArgAttribute must be command input!");

		var command = input as CommandInput;
		var param = command.Args[_index];
		var type = typeof(TParameter);

		if (type == typeof(string))
			return param as TParameter;

		if (type == typeof(bool))
			return Convert.ToBoolean(param) as TParameter;

		if (type == typeof(byte))
			return Convert.ToByte(param) as TParameter;

		if (type == typeof(char))
			return Convert.ToChar(param) as TParameter;

		if (type == typeof(float))
			return Convert.ToSingle(param) as TParameter;

		if (type == typeof(double))
			return Convert.ToDouble(param) as TParameter;

		if (type == typeof(long))
			return Convert.ToDouble(param) as TParameter;

		if (type == typeof(int))
			return Convert.ToDouble(param) as TParameter;

		if (type == typeof(short))
			return Convert.ToDouble(param) as TParameter;

		if (type == typeof(ulong))
			return Convert.ToDouble(param) as TParameter;

		if (type == typeof(uint))
			return Convert.ToDouble(param) as TParameter;

		if (type == typeof(ushort))
			return Convert.ToDouble(param) as TParameter;

		throw new InvalidCastException($"Unable to find parser for type {type.FullName} in arg {_index}");
	}
}
