using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace LMD_Scholar_and_Discerning.GUI
{
	public static class ZipManager
	{
		#region variables
		public const String path_brains= @"Brains\";
		public const String path_buffer = @"Compression\";
		public static Boolean LockBuffer;
		#endregion
		#region CompressionFile
		/// <summary>
		/// Сжимает файл, директорию
		/// </summary>
		/// <param name="path_file">Путь к файлу который нужно сжать</param>
		/// <param name="path_zip_file">Путь файла после сжатия, включает в себя имя файла с расширением *.zip</param>
		public static void CompressionFile(String path_file, String path_zip_file)
		{
			try
			{
				if (!Directory.Exists(path_file))
					Directory.CreateDirectory(path_file);
				ZipFile.CreateFromDirectory(path_file, path_zip_file, CompressionLevel.Optimal, true);
			}
			catch { System.Windows.Forms.MessageBox.Show("Файл с таким именем уже существует"); }
		}
		#endregion
		#region DecompressionFile
		/// <summary>
		/// Декомпрессия архива
		/// </summary>
		/// <param name="path_zip_file">Путь к архиву который нужно разжать</param>
		/// <param name="path_files">Путь куда нужно разжимать</param>
		public static void DecompressionFile(String path_zip_file, String path_files)
		{
			try
			{
				System.IO.Compression.ZipArchive zipFile = System.IO.Compression.ZipFile.OpenRead(path_zip_file);

				foreach (System.IO.Compression.ZipArchiveEntry list in zipFile.Entries)
				{
					using (System.IO.Stream stream = list.Open())
					{
						System.IO.StreamReader reader = new System.IO.StreamReader(stream);

						using (System.IO.Stream str = new System.IO.FileStream(path_files + list.Name, System.IO.FileMode.Create))
						{
							System.IO.StreamWriter writer = new System.IO.StreamWriter(str);

							writer.Write(reader.ReadToEnd());
							writer.Close();

							str.Close();
						}
					}
				}
			}
			catch (Exception ex)
			{
				LogManager.Write(ex);
				System.Windows.Forms.MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");
			}
		}
		#endregion
		#region CreateBuffer
		public static void CreateBuffer()
		{
			if (!Directory.Exists(ZipManager.path_buffer))
				Directory.CreateDirectory(ZipManager.path_buffer);
		}
		#endregion
		#region ClearBuffer
		public static void ClearBuffer()
		{
			try
			{
				Directory.Delete(path_buffer, true);
			}
			catch (Exception ex)
			{
				LogManager.Write(ex);
				System.Windows.Forms.MessageBox.Show("Не предвиденная ошибка.\nДанные ошибки записаны в лог.", "Ошибка!");
			}
		}
		#endregion
		#region WriteBuffer
		public static void WriteBuffer()
		{
		}
		#endregion
	}
}
