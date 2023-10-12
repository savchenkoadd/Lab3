using System.Text;

namespace Lab3_Task1
{
	internal class Program
	{
		private const string FILES_PATH = "C:\\console\\Lab3\\Files";
		private static List<string> _noFileList = new List<string>();
		private static List<string> _badFileList = new List<string>();
		private static List<string> _overflowList = new List<string>();

		async static Task Main(string[] args)
		{
			var fileNames = GetFileNames();

			await HandleInput(fileNames);
			await CreateUltimateFiles();
		}

		async static Task CreateUltimateFiles()
		{
			using (StreamWriter stream = new StreamWriter(new FileStream("no_file.txt", FileMode.CreateNew)))
			{
				foreach (var item in _noFileList)
				{
					await stream.WriteLineAsync(item + "\n");
				}
			}

			using (StreamWriter stream = new StreamWriter(new FileStream("bad_data.txt", FileMode.CreateNew)))
			{
				foreach (var item in _badFileList)
				{
					await stream.WriteLineAsync(item + "\n");
				}
			}

			using (StreamWriter stream = new StreamWriter(new FileStream("overflow.txt", FileMode.CreateNew)))
			{
				foreach (var item in _overflowList)
				{
					await stream.WriteLineAsync(item + "\n");
				}
			}
		}

		async static Task HandleInput(IEnumerable<string> fileNames)
		{
			foreach (var fileName in fileNames)
			{
				try
				{
					List<object> values = await GetValues(fileName);
				}
				catch (ArgumentNullException)
				{
					_badFileList.Add(fileName);
				}
				catch (ArgumentOutOfRangeException)
				{
					_badFileList.Add(fileName);
				}
				catch (FormatException)
				{
					_badFileList.Add(fileName);
				}
				catch (OverflowException)
				{
					_overflowList.Add(fileName);
				}
				catch (FileNotFoundException)
				{
					_noFileList.Add(fileName);
				}
			}
		}

		async static Task<List<object>> GetValues(string filePath)
		{
			using (StreamReader sr = new StreamReader(filePath))
			{
				List<object> result = new List<object>() { "g", "g" };

				int i = 0;
				while (!sr.EndOfStream)
				{
					result[i] = (int.Parse(await sr.ReadLineAsync()!));
					i++;
				}

				Console.WriteLine((Convert.ToInt32(result[0]) + Convert.ToInt32(result[1])) / 2);

				return result.ToList();
			}
		}

		static List<string> GetFileNames()
		{
			List<string> filePaths = new List<string>();

			for (int i = 10; i < 30; i++)
			{
				filePaths.Add($"C:\\console\\Lab3\\Files\\{i}.txt");
			}

			return filePaths;
		}
	}
}