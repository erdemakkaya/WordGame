using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Entities;
using WordGame.Core.Enumerations;
using WordGame.Core.Repositories.Base.Interfaces;
using WordGame.Core.Services;
using WordGame.Core.UnitOfWorks;

namespace WordGame.Application.Services
{
	public class OptionService : IOptionsService
	{
		IUnitofWork _unitOfWork;
		IRepository<Select, int> _repository;
		IMapper _mapper;


		public OptionService(IMapper mapper, IUnitofWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = _unitOfWork.GetDefaultRepo<Select, int>();
			_mapper = mapper;

		}
		public async Task<SelectModel> CreateAsync(SelectModel dtoObject)
		{
			var mappedEntity = _mapper.Map<Select>(dtoObject);
			var newEntity = await _repository.AddAsync(mappedEntity);
			await _unitOfWork.SaveChangesAsync();
			var addedModel = _mapper.Map<SelectModel>(newEntity);
			return addedModel;
		}

		public async Task<SelectModel> CreateOrUpdateAsync(SelectModel dtoObject)
		{
			bool isExist = await _repository.AnyAsync(x => x.Id.Equals(dtoObject.Id));
			if (isExist)
				return await UpdateAsync(dtoObject);

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

		public async Task<SelectModel> GetAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			var mappedModel = _mapper.Map<SelectModel>(entity);
			return mappedModel;
		}

		public async Task<IEnumerable<SelectModel>> GetAsync()
		{
			var entities = await _repository.GetAllAsync();
			var mappedGrammerModels = _mapper.Map<IEnumerable<SelectModel>>(entities);
			return mappedGrammerModels;
		}

		public async Task<SelectModel> UpdateAsync(SelectModel dtoObject)
		{
			var mappedEntity = _mapper.Map<Select>(dtoObject);
			var updatedEntity = await _repository.UpdateAsync(mappedEntity);

			await _unitOfWork.SaveChangesAsync();
			var updatedModel = _mapper.Map<SelectModel>(updatedEntity);
			return updatedModel;
		}
	}
}