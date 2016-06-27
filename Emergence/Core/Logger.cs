using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Emergence.Core {
	public static class Logger {
		private static StreamWriter logFile;
		private static int relativePathStart;

		public static void Initialize() {
			// Determine how much to trim off the beginning of all file paths
			// based on where they begin to differ from the path to the .exe
			string filePath = new StackTrace(true).GetFrame(0).GetFileName();
			string executingPath = Assembly.GetEntryAssembly().Location;
			relativePathStart = 0;
			while(filePath[relativePathStart] == executingPath[relativePathStart]) {
				relativePathStart += 1;
				if(relativePathStart == filePath.Length || relativePathStart == executingPath.Length) {
					relativePathStart = 0;
					break;
				}
			}

			// Create the log file and print a header with the date and time.
			// NOTE: Log files are over-written if the program is run multiple
			//       times in one day, given the current naming scheme.
			var dateString = DateTime.Now.ToString("MM-dd");
			var timeString = DateTime.Now.ToString("HH:mm:ss");
			logFile = new StreamWriter($"emergence-log-{dateString}.txt");
			WriteLine("=====================", false);
			WriteLine("==  Emergence Log  ==", false);
			WriteLine($"== {dateString}  {timeString} ==", false);
			WriteLine("=====================", false);
			Newline();
		}
		public static void Close() {
			logFile.Close();
		}

		public static void Newline() {
			WriteLine("", false);
		}
		public static void Message(string message) {
			var timeString = DateTime.Now.ToString("HH:mm:ss");
			WriteLine($"[{timeString}]: {message}");
		}
		public static void Warning(string message) {
			var timeString = DateTime.Now.ToString("HH:mm:ss");
			WriteLine($"[{timeString}] Warning: {message}");
		}
		public static void Error(string message) {
			var timeString = DateTime.Now.ToString("HH:mm:ss");
			WriteLine($"[{timeString}] Error: {message}");
		}

		private static void WriteLine(string message, bool printDebugData = true) {
			Console.WriteLine(message);
			logFile.WriteLine(message);
			if(printDebugData) {
				PrintDebugData();
			}
		}

		[Conditional("DEBUG")]
		private static void PrintDebugData() {
			var stackTrace = new StackTrace(true);
			// Start at 3 to jump out of Logger portion of stack trace
			for(int i = 3; i < stackTrace.FrameCount; ++i) {
				var stackFrame = stackTrace.GetFrame(i);
				var fileName = stackFrame.GetFileName();
				if(fileName == null) {
					break; // If there's no file name we've reached code outside the project and can stop digging.
				} else {
					fileName = fileName.Substring(relativePathStart);
				}

				var traceString = $"  @ line {stackFrame.GetFileLineNumber()} in method {stackFrame.GetMethod().Name} in file {fileName}";
				Console.WriteLine(traceString);
				logFile.WriteLine(traceString);
			}
			Newline();
		}
	}
}