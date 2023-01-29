using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WordGame.Application.Mapper.Base
{
	public static class MapperHelper
	{
     public static IMappingExpression<TSource, TDestination> DoNotValidate<TSource, TDestination>(
   this IMappingExpression<TSource, TDestination> map,
   Expression<Func<TSource, object>> selector)
        {
            map.ForSourceMember(selector, config => config.DoNotValidate());
            return map;
        }
    }
}
