using AutoMapper;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Entities;
using WordGame.Core.Extensions;
using WordGame.Core.Repositories.CustomRepositories;
using WordGame.Core.Services;
using WordGame.Core.UnitOfWorks;

namespace WordGame.Application.Services
{
	public class WordService : IWordService
	{
		IUnitofWork _unitOfWork;
		IWordRepository _repository;
		IMapper _mapper;
		private readonly IValidator<WordDto> _validator;



		public WordService(IMapper mapper, IUnitofWork unitOfWork, IValidator<WordDto> validator, IWordRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
			_mapper = mapper;
			_validator = validator;
		}

		public async Task<WordDto> CreateAsync(WordDto dtoObject)
		{

			var validationResult = await _validator.ValidateAsync(dtoObject);
			if (!validationResult.IsValid)
			{
				return null;
			}
			if (dtoObject == null || dtoObject.WordName.IsNullOrEmpty())
			{
				return null;
			}
			var mappedEntity = _mapper.Map<Word>(dtoObject);
			var newEntity = await _repository.AddAsync(mappedEntity);

			await _unitOfWork.SaveChangesAsync();
			var newMappedModel = _mapper.Map<WordDto>(newEntity);
			return newMappedModel;
		}

		public async Task<WordDto> CreateOrUpdateAsync(WordDto dtoObject)
		{
			var validationResult = await _validator.ValidateAsync(dtoObject);
			if (!validationResult.IsValid)
			{
				return null;
			}
			bool isExist = await _repository.AnyAsync(x => x.Id.Equals(dtoObject.Id));
			if (isExist) return await UpdateAsync(dtoObject);

			return await CreateAsync(dtoObject);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var deletedEntity = await _repository.GetByIdAsync(id);
			if (deletedEntity == null) return false;

			await _repository.DeleteAsync(deletedEntity);
			await _unitOfWork.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<WordDto>> GetAsync()
		{
			var entities = await _repository.GetAllAsync();
			var mappedWordModels = _mapper.Map<IEnumerable<WordDto>>(entities);
			return mappedWordModels;
		}

		public async Task<WordDto> GetAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			var mappedModel = _mapper.Map<WordDto>(entity);
			return mappedModel;
		}

		public async Task<WordDto> UpdateAsync(WordDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Word>(dtoObject);
			var updatedEntity = await _repository.UpdateAsync(mappedEntity);
			await _unitOfWork.SaveChangesAsync();

			var updatedModel = _mapper.Map<WordDto>(updatedEntity);
			return updatedModel;
		}

		public async Task<WordDto> CreateOrIncreaseAsync(WordDto wordModel)
		{
			if (wordModel.Id != 0)
				return await UpdateAsync(wordModel);

			var existModel = await _repository.GetWordByNameAsync(wordModel.WordName);
			if (existModel == null) return await CreateAsync(wordModel);

			existModel.AddedCount++;
			var exisWordModel = await _repository.UpdateAsync(existModel);
			var newMappedModel = _mapper.Map<WordDto>(exisWordModel);
			return newMappedModel;
		}

		public async Task<IEnumerable<WordDto>> GetWordsByNameAsync(string wordName)
		{
			var wordEntities = await _repository.GetWordsByNameAsync(wordName);
			var mappedWordModels = _mapper.Map<IEnumerable<WordDto>>(wordEntities);
			return mappedWordModels;
		}


		public async Task<WordDto> GetWordByNameAsync(string wordName)
		{
			var wordEntity = await _repository.GetWordByNameAsync(wordName);
			return _mapper.Map<WordDto>(wordEntity);
		}

		public async Task<bool> IncreaseTrueOrFalseCount(int id, bool isCorrect)
		{
			var entity = await _repository.GetByIdAsync(id);
			if (entity == null) return false;

			if (isCorrect)
			{
				entity.TrueCount++;
			}
			else
			{
				entity.FalseCount++;
			}

			await _repository.UpdateAsync(entity);
			await _unitOfWork.SaveChangesAsync();

			return true;
		}
		public async Task<IEnumerable<WordDto>> GetWordsByStatisticAsync()
		{
			var wordEntities = await _repository.GetWordsByStatistic();
			var mappedWordModels = _mapper.Map<IEnumerable<WordDto>>(wordEntities);
			return mappedWordModels;
		}
	}
}