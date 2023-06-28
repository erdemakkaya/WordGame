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
		public static List<SrtModel> ParseSRT(string srtFilePath, bool isTr = false)
		{
			int increaseCount;
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			var fileContent = isTr ? File.ReadAllLines(srtFilePath, Encoding.GetEncoding("Windows-1254")) : File.ReadAllLines(srtFilePath);
			if (fileContent.Length <= 0)
				return null;
			var model = new SrtModel();
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
	}
}
