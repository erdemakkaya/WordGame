using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto.Base;
using WordGame.Core.Enumerations;

namespace WordGame.Core.Dto
{
	public class SelectModel: DtoBase<int>
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public string Color { get; set; }
	}
}
