//
// mono-cil-strip
//
// Author(s):
//   Jb Evain (jbevain@novell.com)
//
// Copyright (C) 2008 Novell, Inc (http://www.novell.com)
//

using System;
using System.Reflection;

using Mono.Cecil;

namespace Mono.CilStripper {

	class Program {

		static int Main (string [] args)
		{
			Header ();

			if (args.Length == 0)
				Usage ();

			string file = args [0];
			string output = args.Length > 1 ? args [1] : file;
			bool replace = file == output;

			try {
				using (var assembly = AssemblyDefinition.ReadAssembly (file, new ReaderParameters { ReadWrite = replace })) {
					StripAssembly (assembly, replace ? null : output);
				}

				if (replace)
					Console.WriteLine ("Assembly {0} stripped", file);
				else
					Console.WriteLine ("Assembly {0} stripped out into {1}", file, output);
				return 0;
			} catch (TargetInvocationException tie) {
				Console.WriteLine ("Error: {0}", tie.InnerException);
			} catch (Exception e) {
				Console.WriteLine ("Error: {0}", e);
			}
			return 1;
		}

		static void StripAssembly (AssemblyDefinition assembly, string output)
		{
			AssemblyStripper.StripAssembly (assembly, output);
		}

		static void Header ()
		{
			Console.WriteLine ("Mono CIL Stripper");
			Console.WriteLine ();
		}

		static void Usage ()
		{
			Console.WriteLine ("Usage: mono-cil-strip file [output]");
			Environment.Exit (1);
		}
	}
}
