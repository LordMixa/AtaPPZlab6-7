using AutoMapper;
using DAL;
using DAL.DBEntities;
using DAL.Repository;
using System.Collections.Generic;
using System.IdentityModel.Metadata;

namespace BLL
{
    public class ShowService:IService
    {
        private readonly IMapper _mapper;
        private readonly IShowRepository _showRepository;
        public ShowService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ShowService(IMapper mapper, IShowRepository showRepository)
        {
            _mapper = mapper;
            _showRepository = showRepository;
        }
        public void AddEntity(IEntity show)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var showRepository = new ShowRepository(unitOfWork);
                var dbshow = _mapper.Map<DBShow>(show);
                showRepository.Create(dbshow);
                unitOfWork.Save();
            }
        }
        public void DeleteEntity(int id)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var showRepository = new ShowRepository(unitOfWork);
                showRepository.Delete(id);
                unitOfWork.Save();
            }
        }
        public void DeleteEntity(string name1, string name2)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var showRepository = new ShowRepository(unitOfWork);
                showRepository.Delete(name1, name2);
                unitOfWork.Save();
            }
        }
       
        public void UpdateEntity(string name1, IEntity show)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var showRepository = new ShowRepository(unitOfWork);
                var dbshow = _mapper.Map<DBShow>(show);
                showRepository.Update(name1, dbshow);
                unitOfWork.Save();
            }
        }
        public IEnumerable<IEntity> GetEntity()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var showRepository = new ShowRepository(unitOfWork);
                var dbshows = showRepository.GetAll();
                var shows = _mapper.Map<IEnumerable<Show>>(dbshows);
                return shows;
            }
        }
    }
}
