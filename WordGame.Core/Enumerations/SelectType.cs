using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Enumerations.Base;

namespace WordGame.Core.Enumerations
{
	public class SelectType:Enumeration
	{
		public static SelectType None = new (0, nameof(None));
		public static SelectType Word = new (1, nameof(None));
		public static SelectType Grammar = new (2, nameof(None));

		public SelectType(int id, string name) : base(id, name)
		{
		}
	}
}
