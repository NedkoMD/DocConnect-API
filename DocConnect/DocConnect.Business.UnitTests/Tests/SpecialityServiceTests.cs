using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Factories;
using DocConnect.Business.Models.Enums;
using DocConnect.Business.Profiles;
using DocConnect.Business.Services;
using DocConnect.Business.UnitTests.Utilities;
using DocConnect.Data.Abstraction.Repositories;
using Moq;
using NUnit.Framework;

namespace DocConnect.Business.UnitTests.Tests
{
    [TestFixture]
    public class SpecialityServiceTests
    {
        private IMapper _mapper;
        private IResultFactory _resultFactory;
        private Mock<ISpecialityRepository> _specialityRepositoryMock;
        private ISpecialityService _specialityService;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<DocConnectProfile>();
            });

            _mapper = config.CreateMapper();
            _resultFactory = new ResultFactory();
            _specialityRepositoryMock = new Mock<ISpecialityRepository>();

            _specialityService = new SpecialityService(
                _mapper,
                _resultFactory,
                _specialityRepositoryMock.Object
            );

            _specialityRepositoryMock
                .Setup(s => s.GetByIdAsync(SpecialityTestConstants.TestExistingSpecialityId))
                .ReturnsAsync(SpecialityTestConstants.TestExistingSpeciality);

            _specialityRepositoryMock
                .Setup(s => s.GetByIdAsync(SpecialityTestConstants.TestNonExistingSpecialityId))
                .ReturnsAsync(SpecialityTestConstants.TestNonExistingSpeciality);
        }

        [Test]
        [TestCase(SpecialityTestConstants.TestExistingSpecialityId, DocConnectStatusCode.OK)]
        [TestCase(SpecialityTestConstants.TestNonExistingSpecialityId, DocConnectStatusCode.NotFound)]
        public async Task GetByIdAsync_HasDifferentSpecialityIds_ReturnsExpectedHttpResult(uint specialityId, DocConnectStatusCode expectedStatusCode)
        {
            // Act
            var result = await _specialityService.GetByIdAsync(specialityId);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Test]
        public async Task AddAsync_HasValidDTO_ReturnsOkResult()
        {
            // Act
            var result = await _specialityService.AddAsync(SpecialityTestConstants.TestValidAddDTO);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        }

        [Test]
        public async Task UpdateAsync_HasValidDTO_ReturnsOkResult()
        {
            // Act
            var result = await _specialityService.UpdateAsync(SpecialityTestConstants.TestExistingSpecialityId, SpecialityTestConstants.TestValidSpecialtyUpdateDTO);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        }

        [Test]
        public async Task UpdateAsync_HasValidDTOAndNonExistingSpeciality_ReturnsNotFoundResult()
        {
            // Act
            var result = await _specialityService.UpdateAsync(SpecialityTestConstants.TestNonExistingSpecialityId, SpecialityTestConstants.TestValidSpecialtyUpdateDTO);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.NotFound));
        }

        [Test]
        public async Task UpdateAsync_HasValidDTOAndExistingSpeciality_ReturnsOkResult()
        {
            // Act
            var result = await _specialityService.UpdateAsync(SpecialityTestConstants.TestExistingSpecialityId, SpecialityTestConstants.TestValidSpecialtyUpdateDTO);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(DocConnectStatusCode.OK));
        }

        [Test]
        [TestCase(SpecialityTestConstants.TestNonExistingSpecialityId, DocConnectStatusCode.NotFound)]
        [TestCase(SpecialityTestConstants.TestExistingSpecialityId, DocConnectStatusCode.NoContent)]
        public async Task DeleteAsync_HasDifferentSpecialityIds_ReturnsExpectedResult(uint specialityId, DocConnectStatusCode expectedStatusCode)
        {
            // Act
            var result = await _specialityService.DeleteAsync(specialityId);

            // Assert
            Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        }
    }
}
