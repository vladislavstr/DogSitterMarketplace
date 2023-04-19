﻿using AutoMapper;

using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Models.Appeals.Request;
using DogSitterMarketplaceBll.Models.Appeals.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Appeals;
using DogSitterMarketplaceDal.Models.Users;
using DogSitterMarketplaceDal.Repositories;


namespace DogSitterMarketplaceBll.Services
{
    public class AppealService : IAppealService
    {
        private readonly IMapper _mapper;
        private readonly IAppealRepository _appealRepository;

        public AppealService(IAppealRepository appealRepository, IMapper mapper)
        {
            _mapper = mapper;
            _appealRepository = appealRepository;
        }

        public IEnumerable<AppealResponse> GetAllAppeals() 
        {
            var allappealsEntitys = _appealRepository.GetAllAppeals();
            var appealResponse = _mapper.Map<IEnumerable<AppealResponse>>(allappealsEntitys);

            return appealResponse;
        }

        public IEnumerable<AppealResponse> GetAllNotDeletedAppeals()
        {
            var allappealsEntitys = _appealRepository.GetAllAppeals();
            var appealsEntitys = allappealsEntitys
                           .Where(u => !u.IsDeleted);
            var appealResponse = _mapper.Map<IEnumerable<AppealResponse>>(appealsEntitys);

            return appealResponse;
        }

        public AppealResponse GetAppealById(int id)
        {
            var appealsEntitys = _appealRepository.GetAppealById(id);
            var appealResponse = _mapper.Map<AppealResponse>(appealsEntitys);

            return appealResponse;
        }

        public AppealResponse AddAppeal(AppealRequest appeal)
        {
            var appealEntity = _mapper.Map<AppealEntity>(appeal);
            var addAppealEntity = _appealRepository.AddAppeal(appealEntity);
            var addAppealResponse = _mapper.Map<AppealResponse>(addAppealEntity);

            return addAppealResponse;
        }

        public AppealStatusResponse AddAppealStatus(AppealStatusRequest appealStatus)
        {
            var appealStatusEntity = _mapper.Map<AppealStatusEntity>(appealStatus);
            var addAppealStatusEntity = _appealRepository.AddAppealStatus(appealStatusEntity);
            var addAppealStatusResponse = _mapper.Map<AppealStatusResponse>(addAppealStatusEntity);

            return addAppealStatusResponse;
        }

        public void DeleteAppealById(int id)
        {
            _appealRepository.DeleteAppealById(id);
        }
    }
}
