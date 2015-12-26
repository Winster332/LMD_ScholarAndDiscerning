using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Windows.Forms;

namespace LMD_Scholar_and_Discerning.GUI.SAD_MENU.NET
{
	#region TinyWebDBAPI
	public class TinyWebDBAPI
	{
		#region конструктор
		public TinyWebDBAPI()
		{
		}
		#endregion

		#region writevalue
		/// <summary>
		/// Пишем новые данные в ячейку. Если указанной ячейки не существует, то создается новая
		/// </summary>
		/// <param name="Tag">Ячейка</param>
		/// <param name="Value">Значение в ячейке</param>
		/// <returns></returns>
		public Boolean writevalue(String Tag, String Value)
		{
			try
			{
				String PD = "tag=" + Tag + "&value=" + Value + "&fmt=html";
				HttpWebRequest HWR = (HttpWebRequest)WebRequest.Create("http://appinvtinywebdb.appspot.com/storeavalue");
				HWR.Method = "POST";
				HWR.Credentials = CredentialCache.DefaultCredentials; 
				UTF8Encoding encoding = new UTF8Encoding(); 
				Byte[] bytes = encoding.GetBytes(PD);
				HWR.ContentType = "application/x-www-form-urlencoded"; 
				HWR.ContentLength = bytes.Length;

				using (Stream newStream = HWR.GetRequestStream()) 
				{
					newStream.Write(bytes, 0, bytes.Length);
					newStream.Close();
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion
		#region radvalue
		/// <summary>
		/// Читает данные из ячейки Tag
		/// </summary>
		/// <param name="Tag">Ячейка из которой будем читать</param>
		/// <returns></returns>
		public String readvalue(String Tag)
		{
			String PD = "tag=" + Tag + "&fmt=html";
			HttpWebRequest HWR = (HttpWebRequest)WebRequest.Create("http://appinvtinywebdb.appspot.com/getvalue");
			HWR.Method = "POST"; // Метод тот же.
			HWR.Credentials = CredentialCache.DefaultCredentials; 
			UTF8Encoding encoding = new UTF8Encoding();
			Byte[] bytes = encoding.GetBytes(PD);
			HWR.ContentType = "application/x-www-form-urlencoded";
			HWR.ContentLength = bytes.Length;

			using (Stream newStream = HWR.GetRequestStream())
			{
				newStream.Write(bytes, 0, bytes.Length);
				newStream.Close();
			}

			HttpWebResponse HWRES = (HttpWebResponse)HWR.GetResponse();
			Stream S = HWRES.GetResponseStream();
			StreamReader SR = new StreamReader(S);
			String STR = SR.ReadToEnd();

			STR = STR.Substring(STR.IndexOf("[\"VALUE\", \"" + Tag + "\", \"") + ("[\"VALUE\", \"" + Tag + "\", \"").Length, 
				STR.IndexOf("\"]") - STR.IndexOf("[\"VALUE\", \"" + Tag + "\", \"") - ("[\"VALUE\", \"" + Tag + "\", \"").Length);

			return STR;
		}
		#endregion
	}
	#endregion
	#region LDBAPI
	public class LDBAPI
	{
		#region variables
		/// <summary>
		/// Хостинг на котором работает DB
		/// </summary>
		public String ldbhost;
		#endregion

		#region конструктор
		public LDBAPI(String ldbhost) // конструктор
		{
			this.ldbhost = ldbhost;
		}
		#endregion

		#region request
		/// <summary>
		/// Читает всю HTML страничку сайта и возвращает ее
		/// </summary>
		/// <param name="request">Хост с которого читаем</param>
		/// <returns></returns>
		public String request(String request)
		{
			WebRequest req = WebRequest.Create(request);
			WebResponse resp = req.GetResponse();
			Stream stream = resp.GetResponseStream();
			StreamReader sr = new StreamReader(stream);
			String answer = sr.ReadToEnd();
			sr.Close();
			return answer;
		}
		#endregion
		#region cutstring
		public String cutstring(String text, String start, String end)
		{
			String resp = text;
			resp = resp.Substring(resp.IndexOf(start) + start.Length);
			resp = resp.Substring(0, resp.IndexOf(end));
			return resp;
		}
		#endregion
		#region cutsreinglastend
		public String cutstringlastend(String text, String start, String end)
		{
			String resp = text;
			resp = resp.Substring(resp.IndexOf(start) + start.Length);
			resp = resp.Substring(0, resp.LastIndexOf(end));
			return resp;
		}
		#endregion

		#region writevalue
		/// <summary>
		/// Пишем новые данные в ячейку. Если указанной ячейки не существует, то создается новая
		/// </summary>
		/// <param name="Tag">Ячейка</param>
		/// <param name="Value">Значение в ячейке</param>
		/// <returns></returns>
		public Boolean writevalue(String Tag, String Value)
		{
			String reqstring = ldbhost + "api/writevalue.php?tag=[TAG]&value=[VALUE]";
			reqstring = reqstring.Replace("[TAG]", Tag).Replace("[VALUE]", Value);
			String resp = cutstring(request(reqstring), "result[", "]");
			String result = resp;
			if (result == "OK")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
		#region readvalue
		/// <summary>
		/// Читает данные из ячейки Tag
		/// </summary>
		/// <param name="Tag">Ячейка из которой будем читать</param>
		/// <returns></returns>
		public String readvalue(String Tag)
		{
			String reqstring = ldbhost + "api/readvalue.php?tag=[TAG]";
			reqstring = reqstring.Replace("[TAG]", Tag);
			String resp = request(reqstring);
			String result = cutstring(resp, "result[", "]");

			if (result == "OK")
			{
				return cutstringlastend(resp, "value[", "]");
			}
			else
			{
				return null;
			}
		}
		#endregion
		#region gettags
		/// <summary>
		/// Возвращает массив всех существующих тэгов
		/// </summary>
		/// <returns></returns>
		public String[] gettags()
		{
			String reqstring = ldbhost + "api/gettags.php?";
			String resp = request(reqstring);

			String sl = resp;
			sl = sl.Substring(0, sl.IndexOf("<br>"));
			sl = sl.Substring(sl.LastIndexOf("\n"));
			Int32 length = Convert.ToInt32(sl);

			String[] tags = new String[length];
			for (Int32 i = 0; i < length; i++)
			{
				String tg = resp;
				tg = tg.Substring(tg.IndexOf((i + 1).ToString() + "[") + ((i + 1).ToString() + "[").Length);
				tg = tg.Substring(0, tg.IndexOf("]"));
				tags[i] = tg;
			}

			return tags;
		}
		#endregion
		#region deltag
		/// <summary>
		/// Удаляет тэг
		/// </summary>
		/// <param name="Tag">Тэг который нужно удалить</param>
		/// <returns></returns>
		public Boolean deltags(String Tag)
		{
			String reqstring = ldbhost + "api/deltag.php?tag=[TAG]";
			reqstring = reqstring.Replace("[TAG]", Tag);
			String resp = request(reqstring);
			String result = cutstring(resp, "result[", "]");

			if (result == "OK")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
		#region tagexists
		/// <summary>
		/// Проверяет на существование тэга в БД
		/// </summary>
		/// <param name="Tag">Тэг который нужно проверить</param>
		/// <returns></returns>
		public Boolean tagexists(String Tag)
		{
			String reqstring = ldbhost + "api/tagexists.php?tag=[TAG]";
			reqstring = reqstring.Replace("[TAG]", Tag);
			String resp = request(reqstring);
			String result = cutstringlastend(resp, "info[", "]");

			if (result == "true")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
	}
	#endregion
}
