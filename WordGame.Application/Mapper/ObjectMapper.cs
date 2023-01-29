using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text.Json;
using WordGame.Application.Mapper.Base;
using WordGame.Core.Dto;
using WordGame.Core.Entities;

namespace WordGame.Application.Mapper
{
	public class ObjectMapper
	{
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<WordGameDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;


     
    }

    public class WordGameDtoMapper : Profile
    {
        public WordGameDtoMapper()
        {
          
            CreateMap<Word, WordModel>()
               .ForMember(t => t.Tags, m => m.MapFrom(u => MapFromJson(u.Tags)));

            CreateMap<WordModel, Word>()
				.DoNotValidate(x => x.Id)
				.ForMember(t => t.Tags, m => m.MapFrom(u => MapToJson(u.Tags)));

            CreateMap<Grammer, GrammerModel>()
   .ForMember(t => t.Tags, m => m.MapFrom(u => MapFromJson(u.Tags)));

            CreateMap<GrammerModel, Grammer>()
                .DoNotValidate(x => x.Id)
                .ForMember(t => t.Tags, m => m.MapFrom(u => MapToJson(u.Tags)));

            CreateMap<Select, SelectModel>().ReverseMap();

        }

        private string MapToJson(List<string> source)
		{
            var result = JsonSerializer.Serialize(source);
            return result;
        }


        private List<string> MapFromJson(string source)
        {
            var result = JsonSerializer.Deserialize<List<string>>(source);
            return result;
        }
    }

}
