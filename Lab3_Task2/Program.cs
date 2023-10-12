using System.Drawing;

namespace Lab3_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var filePaths = GetFilePaths("C:\\console\\Lab3_Task3\\Files");

			foreach (var file in filePaths)
			{
				try
				{
					CreateMirrored(file);
				}
				catch (Exception)
				{
				}
			}
		}
		
		static IEnumerable<string> GetFilePaths(string directoryPath)
		{
			return Directory.GetFiles(directoryPath);
		}

		static void CreateMirrored(string filePath)
		{
			Bitmap originalBitmap = new Bitmap(filePath);

			originalBitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);

			string mirroredFilePath = @$"{filePath}-mirrored.gif";
			originalBitmap.Save(mirroredFilePath);

			originalBitmap.Dispose();
		}
	}
}