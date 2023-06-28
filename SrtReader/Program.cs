// See https://aka.ms/new-console-template for more information
using SrtReader;
using WordGame.Core.Dto;
using WordGame.Core.Extensions;

Console.WriteLine("Hello, World!");
var files = Directory.GetFiles(@"C:\Users\z004b4rd\Desktop\Subtitles").ToList();



	var engSubtitles =  FileHelper.ParseSRT(files[0]);
	var trSubtitles = FileHelper.ParseSRT(files[1],true);


var dic = new Dictionary<SrtModel, SrtModel>();

foreach (var sub in engSubtitles)
{

	var res = trSubtitles.FirstOrDefault(x =>
	(x.StartTime.IsBetween(sub.StartTime,sub.EndTime) || sub.StartTime.IsBetween(x.StartTime, x.EndTime)));
	dic.Add(sub, res);
}


//foreach (var dc in dic)
//{
//	Console.WriteLine($"{dc.Key.Text}  ->  {dc.Value.Text}");
//}

var newDic = dic.Where(x => x.Value != null).ToDictionary(x=> x.Key, x=> x.Value);

var lookup = newDic.ToLookup(x => x.Value, x => x.Key).Where(x => x.Count() > 1).ToList();

bool isFirstItem = true;
var model = new SrtModel();

foreach (var group in lookup)
{
	foreach (var item in group)
	{
		if (isFirstItem)
		{
			model = item;
			isFirstItem = false;
		}
		else
		{
			model.Text += " " + item.Text;
			model.EndTime = item.EndTime;
			newDic.Remove(item);
		}
	}
	isFirstItem = true;
	model = new SrtModel();
}
foreach (var dc in newDic)
{
	Console.WriteLine($"{dc.Key.Text}  ->  {dc.Value.Text}");
}

