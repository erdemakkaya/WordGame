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
	public  class SubtitleService:ISubtitleService
	{
		IUnitofWork _unitOfWork;
		IRepository<Subtitle, int> _repository;
		IMapper _mapper;
		public SubtitleService(IMapper mapper, IUnitofWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_repository = _unitOfWork.GetDefaultRepo<Subtitle, int>();
			_mapper = mapper;
		}


		public async Task<SubtitleDto> CreateAsync(SubtitleDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Subtitle>(dtoObject);
			var newEntity = await _repository.AddAsync(mappedEntity);
			await _unitOfWork.SaveChangesAsync();
			var addedModel = _mapper.Map<SubtitleDto>(newEntity);
			return addedModel;
		}

		public async Task<SubtitleDto> CreateOrUpdateAsync(SubtitleDto dtoObject)
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

			public async Task<SubtitleDto> UpdateAsync(SubtitleDto dtoObject)
		{
			var mappedEntity = _mapper.Map<Subtitle>(dtoObject);
			var updatedEntity = await _repository.UpdateAsync(mappedEntity);

			await _unitOfWork.SaveChangesAsync();
			var updatedModel = _mapper.Map<SubtitleDto>(updatedEntity);
			return updatedModel;
		}

		public async Task<SubtitleDto> GetAsync(int id)
		{
			var entity = await _repository.GetByIdAsync(id);

			var mappedModel = _mapper.Map<SubtitleDto>(entity);
			return mappedModel;
		}

		public async Task<IEnumerable<SubtitleDto>> GetAsync()
		{
			var result = await _repository.GetAllAsync();

			var mappedModel = _mapper.Map<IEnumerable<SubtitleDto>>(result);
			return mappedModel;
		}
		public async Task<IEnumerable<SubtitleDto>> GetByEpisodeId(int episodeId)
		{
			var result = await _repository.GetAsync(x => x.EpisodeId == episodeId);

			var mappedModel = _mapper.Map<IEnumerable<SubtitleDto>>(result);
			return mappedModel;
		}

	}
}
