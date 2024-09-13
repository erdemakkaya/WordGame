using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text.Json;
using WordGame.Application.Mapper.Base;
using WordGame.Core.Dto;
using WordGame.Core.Entities;
using WordGame.Core.Extensions;

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
          
CreateMap<Word, WordDto>()
            .ForMember(t => t.Tags, m => m.MapFrom(u => MapFromJson(u.Tags)))
            .ForMember(t => t.FamiliarWords, m => m.MapFrom(u => u.FamiliarWords)); // new mapping

        CreateMap<WordDto, Word>()
            .DoNotValidate(x => x.Id)
            .ForMember(t => t.Tags, m => m.MapFrom(u => MapToJson(u.Tags)))
            .ForMember(t => t.FamiliarWords, m => m.MapFrom(u => u.FamiliarWords)); // new mapping


            CreateMap<Grammer, GrammerDto>()
   .ForMember(t => t.Tags, m => m.MapFrom(u => MapFromJson(u.Tags)));

            CreateMap<GrammerDto, Grammer>()
                .DoNotValidate(x => x.Id)
                .ForMember(t => t.Tags, m => m.MapFrom(u => MapToJson(u.Tags)));


            CreateMap<Episode, EpisodeDto>().ReverseMap();
            CreateMap<Subtitle, SubtitleDto>().ReverseMap();

            CreateMap<SrtModel, SubtitleDto>()
                .ForMember(d=> d.Section ,m => m.MapFrom(u=>u.Segment.ConvertToInteger()))
                .ForMember(d=> d.EpisodeId,opt => opt.MapFrom(
					(src, dst, arg3, context) => (int)context.Items["EpisodeId"]
				));



            CreateMap<Select, SelectModel>().ReverseMap();
            CreateMap<Series, SeriesDto>()
.ForMember(t => t.Tags, m => m.MapFrom(u => MapFromJson(u.Tags)));

            CreateMap<SeriesDto, Series>()
                .DoNotValidate(x => x.Id)
                .ForMember(t => t.Tags, m => m.MapFrom(u => MapToJson(u.Tags)));

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
