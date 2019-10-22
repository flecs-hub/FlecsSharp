using System;
using System.Runtime.InteropServices;

namespace Flecs
{
	/// <summary>
	/// cross platform helper for fetching symbols out of the Flecs library
	/// </summary>
	static unsafe class InteropUtils
	{
		// windows
		[DllImport("kernel32.dll")]
		static extern IntPtr LoadLibrary(string filename);

		[DllImport("kernel32.dll")]
		static extern IntPtr GetProcAddress(IntPtr hModule, string procname);

		const int RTLD_NOW = 2;

		// linux
		[DllImport("libdl.so", EntryPoint = "dlopen")]
		static extern IntPtr dlopen_linux(string filename, int flags);

		[DllImport("libdl.so", EntryPoint = "dlsym")]
		static extern IntPtr dlsym_linux(IntPtr handle, string symbol);

		// macOS
		[DllImport("libdl.dylib", EntryPoint = "dlopen")]
		static extern IntPtr dlopen_macos(string filename, int flags);

		[DllImport("libdl.dylib", EntryPoint = "dlsym")]
		static extern IntPtr dlsym_macos(IntPtr handle, string symbol);

		public static Func<string, IntPtr> LoadSymbol { get; private set; }

		static InteropUtils()
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT || Environment.OSVersion.Platform.ToString().Contains("Win32"))
			{
				var library = LoadLibrary(ecs.NativeLibName + ".dll");
				LoadSymbol = symbol => GetProcAddress(library, symbol);
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				var library = dlopen_macos("lib" + ecs.NativeLibName + ".dylib", RTLD_NOW);
				LoadSymbol = symbol => dlsym_macos(library, symbol);
			}
			else
			{
				var library = dlopen_linux(ecs.NativeLibName + ".so", RTLD_NOW);
				LoadSymbol = symbol => dlsym_linux(library, symbol);
			}
		}

		public static T LoadTypedSymbol<T>(string symbol) => Marshal.PtrToStructure<T>(LoadSymbol(symbol));
	}
}
