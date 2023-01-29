using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Core.Entities.Base.Interfaces
{
    public interface IEntityBase<TId>:IEntity,IFullAudited
    {
        TId Id { get; }
    }
}
