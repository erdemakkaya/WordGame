using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto.Base.Interfaces;

namespace WordGame.Core.Dto
{
	public  class SrtModel : IDto
	{
		public string Segment { get; set; }
		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
		public string Text { get; set; }
	}
}


//private static void ParseSRT(string srtFilePath)
//{
//    var fileContent = File.ReadAllLines(srtFilePath);
//    if (fileContent.Length <= 0)
//        return;

//    var content = new List<SrtContent>();
//    var segment = reac1;
//    for (int item = 0; item < fileContent.Length; item++)
//    {
//        if (segment.ToString() == fileContent[item])
//        {
//            content.Add(new SrtContent
//            {
//                Segment = segment.ToString(),
//                StartTime = fileContent[item + 1].Substring(0, fileContent[item + 1].LastIndexOf("-->")).Trim(),
//                EndTime = fileContent[item + 1].Substring(fileContent[item + 1].LastIndexOf("-->") + 3).Trim(),
//                Text = fileContent[item + 2]

//            });
//            // The block numbers of SRT like 1, 2, 3, ... and so on
//            segment++;
//            // Iterate one block at a time
//            item += 3;
//        }
//    }
//}