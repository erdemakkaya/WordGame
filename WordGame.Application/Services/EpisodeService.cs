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
	public class EpisodeService:IEpisodeService
	{

		IUnitofWork _unitOfWork;
		IRepository<Episode, int> _repository;
		IMapper _mapper;


		public EpisodeService(IMapper mapper, IUnitofWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = _unitOfWork.GetDefaultRepo<Episode, int>();
			_mapper = mapper;

		}
		public async Task<EpisodeDto> CreateAsync(EpisodeDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Episode>(dtoObject);
			var newEntity = await _repository.AddAsync(mappedEntity);
			await _unitOfWork.SaveChangesAsync();
			var addedModel = _mapper.Map<EpisodeDto>(newEntity);
			return addedModel;
		}

		public async Task<EpisodeDto> CreateOrUpdateAsync(EpisodeDto dtoObject)
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

		public async Task<EpisodeDto> GetAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			var mappedModel = _mapper.Map<EpisodeDto>(entity);
			return mappedModel;
		}

		public async Task<IEnumerable<EpisodeDto>> GetBySeriesId(int seriesId)
		{
			var entities = await _repository.GetAsync(x=> x.SeriesId == seriesId);
			var mappedGrammerModels = _mapper.Map<IEnumerable<EpisodeDto>>(entities);
			return mappedGrammerModels;
		}

		public async Task<IEnumerable<EpisodeDto>> GetAsync()
		{
			var entities = await _repository.GetAllAsync();
			var mappedGrammerModels = _mapper.Map<IEnumerable<EpisodeDto>>(entities);
			return mappedGrammerModels;
		}

		public async Task<EpisodeDto> UpdateAsync(EpisodeDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Episode>(dtoObject);
			var updatedEntity = await _repository.UpdateAsync(mappedEntity);

			await _unitOfWork.SaveChangesAsync();
			var updatedModel = _mapper.Map<EpisodeDto>(updatedEntity);
			return updatedModel;
		}

	}
}
