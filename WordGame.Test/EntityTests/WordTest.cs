using AutoMapper;
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordGame.Application.Mapper;
using WordGame.Application.Services;
using WordGame.Core.Dto;
using WordGame.Core.Entities;
using WordGame.Core.Repositories.CustomRepositories;
using WordGame.Core.UnitOfWorks;
using Xunit;

namespace WordGame.Test.EntityTests
{
	public class WordTest
	{
		IMapper _mapper;
		private Mock<IWordRepository> _wordRepository;
		private Mock<IUnitofWork> _unitOfWork;
		WordService wordService;

		List<Word> words = new List<Word>
				{
					new Word
					{
					  WordName = "Make",
					  TurkishTranslator="yapmak",
					  AddedCount=0,
					  TrueCount=5,
					  FalseCount=0,
					},

					new Word
					{
					  WordName = "save",
					  TurkishTranslator="biriktirmek",
					  AddedCount=0,
					  TrueCount=8,
					  FalseCount=0,
					},

					new Word
					{
					  WordName = "intimate",
					  TurkishTranslator="samimi olmak",
					  AddedCount=0,
					  TrueCount=7,
					  FalseCount=1,
					},

					new Word
					{
					  WordName = "Sip",
					  TurkishTranslator="Yudumlamak",
					  AddedCount=0,
					  TrueCount=1,
					  FalseCount=2
					}
				};

		IReadOnlyCollection<Word> readOnlyWords => words.AsReadOnly();
		private void Update(Word word)
		{
			var updatedWord = words.FirstOrDefault(x => x.Id == word.Id);
			updatedWord = word;
		}

		public WordTest()
		{
			if (_mapper == null)
			{
				var mappingConfig = new MapperConfiguration(mc =>
				{
					mc.AddProfile(new WordGameDtoMapper());
				});
				IMapper mapper = mappingConfig.CreateMapper();
				_mapper = mapper;

				_unitOfWork = new Mock<IUnitofWork>();

				

				_wordRepository = new Mock<IWordRepository>();

				_wordRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(words);

				_wordRepository.Setup(p => p.AddAsync(It.IsAny<Word>()))
				.Returns<Word>(arg => Task.FromResult(arg))//<-- returning the input value from task.
				.Callback<Word>(arg => words.Add(arg)); //<-- use call back to perform function

				_wordRepository.Setup(p => p.UpdateAsync(It.IsAny<Word>()))
				.Returns<Word>(arg => Task.FromResult(arg))//<-- returning the input value from task.
				.Callback<Word>(arg => Update(arg)); //<-- use call back to perform function

				_wordRepository.Setup(p => p.UpdateAsync(It.IsAny<Word>()))
				.Returns<Word>(arg => Task.FromResult(arg))//<-- returning the input value from task.
				.Callback<Word>(arg => Update(arg)); //<-- use call back to perform function

				_wordRepository.Setup(p => p.GetWordByNameAsync(It.IsAny<string>()))
					.ReturnsAsync((string name) => words.FirstOrDefault(x => name.ToLower().Equals(x.WordName.ToLower())));

				_unitOfWork.Setup(p => p.GetCustomRepository<IWordRepository>())
					.Returns(_wordRepository.Object);
				_unitOfWork.Setup(p => p.SaveChangesAsync(It.IsAny<bool>())).ReturnsAsync(1);

				wordService = new WordService(_mapper, _unitOfWork.Object);
			}
		}


		[Fact]
		public async Task New_Word_Can_Be_Added()
		{
			var word = new WordModel
			{
				WordName = "Test",
				TurkishTranslator = "deneme",
				Type = "noun",
				AddedCount = 1,
				Id = 5,
			};

			var result = await wordService.CreateAsync(word);

			Assert.Equal(5, result.Id);
			Assert.Equal(5, words.Count);

		}

		[Fact]
		public async Task Null_Word_Cannot_Be_Added()
		{
			int currentCount = words.Count;

			WordModel word = null;

			var result = await wordService.CreateAsync(word);

			Assert.Null(result);
			Assert.Equal(currentCount, words.Count);

		}

		[Fact]
		public async Task Word_With_Empty_Name_Cannot_Be_Added()
		{
			int currentCount = words.Count;
			var word = new WordModel
			{
				WordName = string.Empty,
				TurkishTranslator = "deneme",
				Type = "noun",
				AddedCount = 1,
				Id = 9,
			};


			var result = await wordService.CreateAsync(word);

			Assert.Null(result);
			Assert.Equal(currentCount, words.Count);

		}

		[Fact]
		public async Task If_New_Word_Added_Count_Should_Be_One()
		{
			var word = new WordModel
			{
				WordName = "newTest6",
				TurkishTranslator = "deneme6",
				Type = "noun6",
				AddedCount = 1,
				Id = 6
			};

			var firstResult = await wordService.CreateOrIncreaseAsync(word);
			Assert.Equal(1, firstResult.AddedCount);
		}

		[Fact]
		public async Task If_Exist_Word_Added_Count_Should_Be_Two()
		{
			var word = new WordModel
			{
				WordName = "newTest7",
				TurkishTranslator = "deneme7",
				Type = "noun7",
				AddedCount = 1,
				
			};

			var firstResult=await wordService.CreateOrIncreaseAsync(word);
			var secondResult = await wordService.CreateOrIncreaseAsync(word);

			Assert.Equal(2, secondResult.AddedCount);
			Assert.Equal(firstResult.Id, secondResult.Id);
		}
	}
}
