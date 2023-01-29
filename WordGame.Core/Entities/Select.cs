using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Entities.Base;
using WordGame.Core.Enumerations;

namespace WordGame.Core.Entities
{
	public class Select : EntityBase<int>
	{
		[MaxLength(1000)]
		public string Name { get; set; }
		[MaxLength(300)]
		public string Value { get; set; }
		[MaxLength(300)]
		public string Color { get; set; }

	}
}
