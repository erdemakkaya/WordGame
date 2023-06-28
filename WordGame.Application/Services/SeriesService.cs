using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordGame.Core.Dto;
using WordGame.Core.Entities;
using WordGame.Core.Repositories.Base.Interfaces;
using WordGame.Core.Services;
using WordGame.Core.UnitOfWorks;

namespace WordGame.Application.Services
{
	public class SeriesService : ISeriesService
	{
		IUnitofWork _unitOfWork;
		IRepository<Series, int> _repository;
		IMapper _mapper;


		public SeriesService(IMapper mapper, IUnitofWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = _unitOfWork.GetDefaultRepo<Series, int>();
			_mapper = mapper;

		}
		public async Task<SeriesDto> CreateAsync(SeriesDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Series>(dtoObject);
			var newEntity = await _repository.AddAsync(mappedEntity);
			await _unitOfWork.SaveChangesAsync();
			var addedModel = _mapper.Map<SeriesDto>(newEntity);
			return addedModel;
		}

		public async Task<SeriesDto> CreateOrUpdateAsync(SeriesDto dtoObject)
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

		public async Task<SeriesDto> GetAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			var mappedModel = _mapper.Map<SeriesDto>(entity);
			return mappedModel;
		}

		public async Task<IEnumerable<SeriesDto>> GetAsync()
		{
			var entities = await _repository.GetAllAsync();
			var mappedGrammerModels = _mapper.Map<IEnumerable<SeriesDto>>(entities);
			return mappedGrammerModels;
		}

		public async Task<SeriesDto> UpdateAsync(SeriesDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Series>(dtoObject);
			var updatedEntity = await _repository.UpdateAsync(mappedEntity);

			await _unitOfWork.SaveChangesAsync();
			var updatedModel = _mapper.Map<SeriesDto>(updatedEntity);
			return updatedModel;
		}
	}
}
