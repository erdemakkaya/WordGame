using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Extensions;

namespace WordGame.Core.Helpers
{
	public static class FileHelpers
	{
		private static List<SrtModel> ParseSRT(string[] fileContent, bool isTr = false)
		{
			int increaseCount;
			if (fileContent.Length <= 0)
				return null;
			var list = fileContent.ToList();
			list.RemoveAll(x => string.IsNullOrEmpty(x));
			var model = new SrtModel();
			fileContent = list.ToArray();
			int length = fileContent.Length;

			var content = new List<SrtModel>();
			var segment = 1;
			for (int item = 0; item < length; item++)
			{

				increaseCount = 3;
				if (segment.ToString().Equals(fileContent[item]))
				{
					model = new SrtModel();
					model.Segment = segment.ToString();
					model.StartTime = TimeSpan.Parse(fileContent[item + 1].Substring(0, fileContent[item + 1].LastIndexOf("-->")).Trim());
					model.EndTime = TimeSpan.Parse(fileContent[item + 1].Substring(fileContent[item + 1].LastIndexOf("-->") + 3).Trim());
					model.Text = fileContent[item + 2];

					segment++;
					while ((item + increaseCount) < length && fileContent[item + increaseCount] != segment.ToString())
					{
						if (fileContent[item + increaseCount].HasValue())
						{
							model.Text += " " + fileContent[item + increaseCount];
						}
						increaseCount++;
					}
					content.Add(model);

					// The block numbers of SRT like 1, 2, 3, ... and so on
					// Iterate one block at a time
					item += increaseCount - 1;
				}
			}
			return content;
		}

		private static List<string> CombineContent(List<string> list)
		{
			 list.RemoveAll(x=> string.IsNullOrEmpty(x));
			return null;
		}

		public static List<SrtModel> ParseSRTByFilePath(string filePath, bool isTr = false)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			var fileContent = isTr ? File.ReadAllLines(filePath, Encoding.GetEncoding("Windows-1254")) : File.ReadAllLines(filePath);

			return ParseSRT(fileContent);
		}

		public static List<SrtModel> ParseSRTBySB(StringBuilder file, bool isTr = false)
		{
			string[] lines = file.ToString().Split(Environment.NewLine.ToCharArray());
			return ParseSRT(lines);
		}

	}
}
