// *** This code works on desktop but crashes in WASM ***

namespace CoCrashWASM;

public class Program
{
	public static async Task Main()
	{
		try
		{
			CrashMe(); // Can't find .ctor, VTable error
			await CrashMeAsync(); // Generic parameter 0 error
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debugger.Break(); // TypeLoadException
			throw;
		}
	}

	static void CrashMe()
	{
		var x = new DerivedCrash<int>();
	}

	static async Task CrashMeAsync() => await CrashDifferent();
	static Task<DerivedCrash<int>> CrashDifferent() => throw new Exception();
}


public abstract class BaseProp
{
}

public class DerivedProp<T> : BaseProp
{
}

public interface ICrash<T>
{
	BaseProp MyProp { get; }
}

public abstract class BaseCrash
{
	public abstract BaseProp MyProp { get; }
}

public class DerivedCrash<T> : BaseCrash, ICrash<T>
{
	public override DerivedProp<T> MyProp { get; } = new();
}


