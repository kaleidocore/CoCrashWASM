// *** This code works on desktop but crashes in WASM ***

namespace CoCrashWASM;

public class Program
{
	public static async Task Main(string[] args)
	{
		try
		{
			CrashMe(); // Can't find .ctor, VTable error
			await CrashMeAsync(); // Generic parameter 0 error
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debugger.Break(); // TypeLoadException
		}
	}

	static void CrashMe()
	{
		var x = new DerivedCrash<int>();
	}

	static async Task CrashMeAsync() => await CrashDifferent();
	static Task<DerivedCrash<int>> CrashDifferent() => throw new Exception();
}


public abstract class Prop
{
}

public class Prop<T> : Prop
{
}

public interface ICrash<T>
{
	Prop MyProp { get; }
}

public abstract class BaseCrash
{
	public abstract Prop MyProp { get; }
}

public class DerivedCrash<T> : BaseCrash, ICrash<T>
{
	public override Prop<T> MyProp { get; } = new();
}


