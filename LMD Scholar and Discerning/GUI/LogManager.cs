using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LMD_Scholar_and_Discerning.GUI
{
	public static class LogManager
	{
		#region variables
		public const String path_log = @"Log\";
		#endregion
		#region CreateLogDirectory
		public static void CreateLogDirectory()
		{
			if (!Directory.Exists(path_log))
				Directory.CreateDirectory(path_log);
		}
		#endregion
		#region Write
		public static void Write(Exception ex)
		{
			String name = System.DateTime.Now.ToLongDateString();
			String title = System.DateTime.Now.ToString();
			String msg = ex.ToString();
			String result = title + " - [" + ex.Message + "]. Message: " + msg + "\r\n";
			String str_line = "--";

			for (int i = 0; i < 10; i++)
				str_line += "---";

			CreateLogDirectory();

			if (!File.Exists(path_log + name + ".log"))
			{
				using (Stream stream = new FileStream(path_log + name + ".log", FileMode.Create))
				{
					stream.Close();
				}				
			}
			List<String> text = new List<String>();
			text.Add(title);
			text.Add("[" + ex.Message + "]");
			text.Add(msg);
			text.Add(str_line);

			File.AppendAllLines(path_log + name + ".log", text);
		}
		#endregion
		#region ReadToEnd
		public static String ReadToEnd(String path)
		{
			String result = File.ReadAllText(path);
			return result;
		}
		#endregion
	}
}
