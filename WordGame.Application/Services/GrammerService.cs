using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Entities;
using WordGame.Core.Repositories;
using WordGame.Core.Repositories.CustomRepositories;
using WordGame.Core.Services;
using WordGame.Core.UnitOfWorks;

namespace WordGame.Application.Services
{
	public class GrammerService : IGrammerService
	{
		IUnitofWork _unitOfWork;
		IGrammerRepository _repository;
		IMapper _mapper;


		public GrammerService( IMapper mapper, IUnitofWork unitOfWork, IGrammerRepository repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<GrammerDto> CreateAsync(GrammerDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Grammer>(dtoObject);
			var newEntity = await _repository.AddAsync(mappedEntity);
			await _unitOfWork.SaveChangesAsync();
			var newMappedModel = _mapper.Map<GrammerDto>(newEntity);
			return newMappedModel;
		}

		public async Task<GrammerDto> CreateOrUpdateAsync(GrammerDto dtoObject)
		{
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

		public async Task<IEnumerable<GrammerDto>> GetAsync()
		{
			var entities = await _repository.GetAllAsync();
			var mappedGrammerModels = _mapper.Map<IEnumerable<GrammerDto>>(entities);
			return mappedGrammerModels;
		}

		public async Task<GrammerDto> GetAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			var mappedModel = _mapper.Map<GrammerDto>(entity);
			return mappedModel;
		}

		public async Task<GrammerDto> UpdateAsync(GrammerDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Grammer>(dtoObject);
			var updatedEntity = await _repository.UpdateAsync(mappedEntity);

			await _unitOfWork.SaveChangesAsync();
			var updatedModel = _mapper.Map<GrammerDto>(updatedEntity);
			return updatedModel;
		}


		public async Task<GrammerDto> GetGrammerByNameAsync(string name)
		{
			var entity = await _repository.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name));
			if (entity == null) return null;

			var mappedModel = _mapper.Map<GrammerDto>(entity);
			return mappedModel;
		}

		public async Task<IEnumerable<GrammerDto>> GetGrammersByNameAsync(string grammerName)
		{
			var entities = await _repository.GetAsync(x => x.Name.ToLower().Equals(grammerName));
			if (entities == null) return null;

			var mappedGrammers = _mapper.Map<IEnumerable<GrammerDto>>(entities);
			return mappedGrammers;
		}
	}
}