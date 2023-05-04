using AutoMapper;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Works.Request;
using DogSitterMarketplaceBll.Models.Works.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Works;
using ILogger = NLog.ILogger;
using LogLevel = NLog.LogLevel;

namespace DogSitterMarketplaceBll.Services
{

    public class WorkService : IWorkService
    {
        private readonly IWorkAndLocationRepository _workAndLocationRepo;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public WorkService(IWorkAndLocationRepository workAndLocationRepo
            , IMapper mapper, ILogger logger)
        {
            _workAndLocationRepo = workAndLocationRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SitterWorkResponse> AddSitterWork(SitterWorkBaseRequest sitterWork)
        {
            SitterWorkResponse result = null;
            var sitterWorkUser = _workAndLocationRepo.GetSitterWorksUser(sitterWork.UserId, false);
            var newSitterWork = _mapper.Map<SitterWorkEntity>(sitterWork);
            if (!sitterWorkUser.Exists(sw => sw.UserId == newSitterWork.UserId && sw.WorkTypeId == newSitterWork.WorkTypeId))
            {
                result = _mapper.Map<SitterWorkResponse>(await _workAndLocationRepo.AddSitterWork(newSitterWork));
            }
            else
            {
                _logger.Log(LogLevel.Error, $"This service is already provided by the user.");
            }

            return result;
        }

        public async Task<SitterWorkResponse> UpdateSitterWork(SitterWorkRequest sitterWork)
        {
            SitterWorkResponse result = null;
            var newSitterWork = _mapper.Map<SitterWorkEntity>(sitterWork);
            var sitterWorkUser = _workAndLocationRepo.GetSitterWorksUser(sitterWork.UserId, false);
            if (!sitterWorkUser.Exists(sw => sw.UserId == newSitterWork.UserId && sw.WorkTypeId == newSitterWork.WorkTypeId && sw.Id != sitterWork.Id))
            {
                result = _mapper.Map<SitterWorkResponse>(await _workAndLocationRepo.UpdateSitterWork(newSitterWork));
            }
            else
            {
                _logger.Log(LogLevel.Error, $"This service is already provided by the userThis service is already provided by the user.");
            }

            return result;
        }

        public async Task<bool> ChangeIsDeletedSitterWork(int sitterWorkId, bool isDeleted)
        {
            bool result = false;

            result = await _workAndLocationRepo.ChangeIsDeletedSitterWork(sitterWorkId, isDeleted);

            return result;
        }

        public async Task<SitterWorkResponse> GetInfoSitterWork(int sitterWorkId, bool? isDeleted)
        {
            var t =  _mapper.Map<SitterWorkResponse>(await _workAndLocationRepo.GetInfoSitterWork(sitterWorkId, isDeleted));
            return t;
        }

        public List<SitterWorkResponse> GetSitterWorksUser(int userId,bool? workIsDeleted = null)
        {
            return _mapper.Map<List<SitterWorkResponse>>(_workAndLocationRepo.GetSitterWorksUser(userId,workIsDeleted));
        }

        //public List<SitterWorkResponse> GetSitterWorksUserByStatusIsDeleted(int userId, bool isDeleted = false)
        //{
        //    return _mapper.Map<List<SitterWorkResponse>>(_workAndLocationRepo.GetSitterWorksUser(userId, isDeleted));
        //}

        public List<SitterWorkResponse> GetSitterWorks(bool? isDeleted = null)
        {
            return _mapper.Map<List<SitterWorkResponse>>(_workAndLocationRepo.GetSitterWorks());
        }

        //public async Task<List<SitterWorkResponse>> GetSitterWorksByStatusIsDeleted(bool? isDeleted = null)
        //{
        //    return _mapper.Map<List<SitterWorkResponse>>(await _workAndLocationRepo.GetSitterWorks(isDeleted));
        //}

        public async Task<List<WorkTypeResponse>> GetWorkTypes(bool? isDeleted = null)
        {
            return _mapper.Map<List<WorkTypeResponse>>(await _workAndLocationRepo.GetAllWorkTypes(isDeleted));
        }
    }
}
