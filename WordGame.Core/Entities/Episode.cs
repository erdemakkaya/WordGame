using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Entities.Base;

namespace WordGame.Core.Entities
{
	public  class Episode: EntityBase<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public int Season { get; set; }
		public int Order { get; set; }

		public int Duration { get; set; }

		public int SeriesId { get; set; }
		public Series Series { get; set; }

		public ICollection<Subtitle> Subtitles { get; set; }


	}
}
