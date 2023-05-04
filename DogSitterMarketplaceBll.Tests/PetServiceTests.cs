using AutoMapper;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Pets.Response;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceBll.Tests.TestCaseSource;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Models.Pets;
using DogSitterMarketplaceDal.Models.Users;
using FluentAssertions;
using Moq;
using NLog;

namespace DogSitterMarketplaceBll.Tests
{
    public class PetServiceTests
    {
        private PetService _petService;

        //private Mock<IOrderRepository> _mockOrderRepo;

        private Mock<IPetRepository> _mockPetRepo;

        private Mock<IUserRepository> _mockUserRepo;

        //  private Mock<IWorkAndLocationRepository> _mockWorkLocationRepo;

        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile<MapperBllOrderProfile>();
                cfg.AddProfile<MapperBllPetProfile>();
            }).CreateMapper();
            var logger = LogManager.Setup().GetCurrentClassLogger();
            // _mockOrderRepo = new Mock<IOrderRepository>();
            _mockPetRepo = new Mock<IPetRepository>();
            _mockUserRepo = new Mock<IUserRepository>();
            //_mockWorkLocationRepo = new Mock<IWorkAndLocationRepository>();
            _petService = new PetService(
                                            //_mockOrderRepo.Object,
                                            _mockPetRepo.Object,
                                            _mockUserRepo.Object,
                                            //_mockWorkLocationRepo.Object,
                                            _mapper,
                                            logger);
        }

        [TestCaseSource(typeof(PetServiceTestCaseSource), nameof(PetServiceTestCaseSource.AddPetTestCaseSource))]
        public async Task AddPetTestTest(PetEntity petEntity, PetEntity addPetEntity, PetRequest addPet, PetResponse expected)
        {
            _mockPetRepo.Setup(p => p.AddPet(It.Is<PetEntity>(p => Compare(p, petEntity)))).ReturnsAsync(addPetEntity);

            PetResponse actual = await _petService.AddPet(addPet);

            _mockPetRepo.Verify(p => p.AddPet(It.IsAny<PetEntity>()), Times.Once());

            actual.Should().BeEquivalentTo(expected);
        }

        [TestCaseSource(typeof(PetServiceTestCaseSource), nameof(PetServiceTestCaseSource.UpdatePetTestCaseSource))]
        public async Task UpdatePetTestTest(PetEntity petEntity, PetEntity updatePetEntity, int userId, UserEntity userEntity, PetUpdate petUpdate, PetResponse expected)
        {
            _mockUserRepo.Setup(u => u.GetUserWithRoleById(userId)).ReturnsAsync(userEntity);
            _mockPetRepo.Setup(p => p.UpdatePet(It.Is<PetEntity>(p => Compare(p, petEntity)))).ReturnsAsync(updatePetEntity);

            PetResponse actual = await _petService.UpdatePet(petUpdate);

            _mockUserRepo.Verify(u => u.GetUserWithRoleById(userId), Times.Once());
            _mockPetRepo.Verify(p => p.UpdatePet(It.IsAny<PetEntity>()), Times.Once());

            actual.Should().BeEquivalentTo(expected);
        }

        private bool Compare(PetEntity p, PetEntity petEntity)
        {
            try
            {
                p.Should().BeEquivalentTo(petEntity);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
