using AutoMapper;
using DocConnect.Business.Abstraction.Factories;
using DocConnect.Business.Models.DTOs.Appointments;
using DocConnect.Business.Models.Results;
using DocConnect.Business.Models.Utilities;
using DocConnect.Business.Services;
using DocConnect.Business.UnitTests.Utilities;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Models.Entities;
using DocConnect.Data.Repositories;
using Moq;
using NUnit.Framework;

namespace DocConnect.Business.UnitTests.Tests
{
    [TestFixture]
    public class AppointmentServiceTests
    {
        private AppointmentService _appointmentService;
        private Mock<IAppointmentRepository> _appointmentRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IResultFactory> _resultFactoryMock;
        private Mock<IPatientRepository> _patientRepositoryMock;
        private Mock<IDoctorRepository> _doctorRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
            _mapperMock = new Mock<IMapper>();
            _resultFactoryMock = new Mock<IResultFactory>();
            _patientRepositoryMock = new Mock<IPatientRepository>();
            _doctorRepositoryMock = new Mock<IDoctorRepository>();

            _appointmentService = new AppointmentService(
                _mapperMock.Object,
                _resultFactoryMock.Object,
                _appointmentRepositoryMock.Object,
                _patientRepositoryMock.Object,
                _doctorRepositoryMock.Object
            );

            _resultFactoryMock.Setup(factory =>
            factory.GetBadRequestResult<AppointmentResultDTO>(It.IsAny<string>()))
            .Returns(new BadRequestResult<AppointmentResultDTO>());

        }

        [Test]
        public async Task GetAllAsync_ReturnsAppointments()
        {
            // Arrange
            var appointments = new List<Appointment>();
            _appointmentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(appointments);

            var appointmentResultDTOs = new List<AppointmentResultDTO>();
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentResultDTO>>(appointments)).Returns(appointmentResultDTOs);

            // Act
            var result = await _appointmentService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllAsync_NoAppointments_ReturnsEmptyList()
        {
            // Arrange
            var emptyAppointments = new List<Appointment>();
            _appointmentRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(emptyAppointments);

            // Act
            var result = await _appointmentService.GetAllAsync();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllDetailedAppointmentsAsync_ReturnsAppointmentDetailedResultDTOs()
        {
            // Arrange
            uint userId = 123;

            var appointments = new List<AppointmentDetailedModel>();
            _appointmentRepositoryMock.Setup(repo => repo.GetAllDetailedAppointmentAsync(userId)).ReturnsAsync(appointments);

            var appointmentDetailedResultDTOs = new List<AppointmentDetailedResultDTO>();
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentDetailedResultDTO>>(appointments)).Returns(appointmentDetailedResultDTOs);

            // Act
            var result = await _appointmentService.GetAllDetailedAppointmentsAsync(userId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllDetailedAppointmentsAsync_ReturnsEmptyListWhenNoAppointments()
        {
            // Arrange
            uint userId = 456;

            var appointments = new List<AppointmentDetailedModel>();
            _appointmentRepositoryMock.Setup(repo => repo.GetAllDetailedAppointmentAsync(userId)).ReturnsAsync(appointments);

            // Act
            var result = await _appointmentService.GetAllDetailedAppointmentsAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllDetailedAppointmentsAsync_HandlesException()
        {
            // Arrange
            uint userId = 789;

            _appointmentRepositoryMock.Setup(repo => repo.GetAllDetailedAppointmentAsync(userId))
                .ThrowsAsync(new Exception(AppointmentTestConstants.SimulatedException));

            // Act and Assert
            Assert.ThrowsAsync<Exception>(async () => await _appointmentService.GetAllDetailedAppointmentsAsync(userId));
        }

        [Test]
        public async Task GetByIdAsync_InvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            long invalidAppointmentId = 999;
            _appointmentRepositoryMock.Setup(repo => repo.GetByIdAsync(invalidAppointmentId)).ReturnsAsync((Appointment)null);

            var expectedNotFoundResult = new NotFoundResult<AppointmentResultDTO>(AppointmentMessages.AppointmentNotFoundMessage);
            _resultFactoryMock.Setup(factory => factory.GetNotFoundResult<AppointmentResultDTO>(AppointmentMessages.AppointmentNotFoundMessage)).Returns(expectedNotFoundResult);

            // Act
            var result = await _appointmentService.GetByIdAsync(invalidAppointmentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IResult<AppointmentResultDTO>>(result);
            Assert.AreEqual(expectedNotFoundResult, result);
        }

        [Test]
        public async Task AddAsync_ValidAppointment_ReturnsOkResult()
        {
            // Arrange
            var appointmentAddDTO = new AppointmentAddDTO
            {
                TimeSlot = DateOnly.FromDateTime(DateTime.Now.AddHours(10)),
            };

            // Act
            var result = await _appointmentService.AddAsync(appointmentAddDTO);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task AddAsync_InvalidAppointment_ReturnsBadRequestResult()
        {
            // Arrange
            var appointmentAddDTO = new AppointmentAddDTO
            {
                TimeSlot = DateOnly.FromDateTime(DateTime.Now.AddHours(8)),
            };

            // Act
            var result = await _appointmentService.AddAsync(appointmentAddDTO);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllByDoctorIdAsync_ReturnsAppointmentDoctorResultDTO()
        {
            // Arrange
            uint doctorId = 1;
            var date = DateTime.UtcNow;
            var appointments = new List<Appointment>();

            _appointmentRepositoryMock.Setup(repo => repo.GetAllByDoctorIdAsync(doctorId, date, date.AddDays(30)))
                .ReturnsAsync(appointments);

            var appointmentResultDTOs = new List<AppointmentResultDTO>();
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentResultDTO>>(appointments))
                .Returns(appointmentResultDTOs);

            var appointmentDoctorResultDTO = new AppointmentDoctorResultDTO();
            _mapperMock.Setup(mapper => mapper.Map<AppointmentDoctorResultDTO>(appointmentResultDTOs))
                .Returns(appointmentDoctorResultDTO);

            appointmentDoctorResultDTO.DoctorId = doctorId;
            appointmentDoctorResultDTO.Appointments = appointmentResultDTOs;

            // Act
            var result = await _appointmentService.GetAllByDoctorIdAsync(doctorId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllByPatientIdAsync_ReturnsAppointmentResultDTOList()
        {
            // Arrange
            uint userId = 1;
            var appointments = new List<Appointment>();

            _appointmentRepositoryMock.Setup(repo => repo.GetAllByPatientIdAsync(userId))
                .ReturnsAsync(appointments);

            var appointmentResultDTOs = new List<AppointmentResultDTO>();
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<AppointmentResultDTO>>(appointments))
                .Returns(appointmentResultDTOs);

            // Act
            var result = await _appointmentService.GetAllByPatientIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task GetAllByPatientIdAsync_ValidUserId_NoAppointments_ReturnsEmptyList()
        {
            // Arrange
            uint userId = 123;
            var emptyAppointments = new List<Appointment>();
            _appointmentRepositoryMock.Setup(repo => repo.GetAllByPatientIdAsync(userId)).ReturnsAsync(emptyAppointments);

            // Act
            var result = await _appointmentService.GetAllByPatientIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetAllByPatientIdAsync_InvalidUserId_ReturnsEmptyList()
        {
            // Arrange
            uint userId = 999;
            _appointmentRepositoryMock.Setup(repo => repo.GetAllByPatientIdAsync(userId)).ReturnsAsync((List<Appointment>)null);

            // Act
            var result = await _appointmentService.GetAllByPatientIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }
    }
}
