using AutoMapper;
using DocConnect.Business.Models.DTOs.Appointments;
using DocConnect.Business.Models.DTOs.City;
using DocConnect.Business.Models.DTOs.Doctor;
using DocConnect.Business.Models.DTOs.Location;
using DocConnect.Business.Models.DTOs.Speciality;
using DocConnect.Business.Models.DTOs.Token;
using DocConnect.Business.Models.DTOs.User;
using DocConnect.Data.Models.Entities;
using DocConnect.Data.Models.Models;
using DocConnect.Data.Repositories;

namespace DocConnect.Business.Profiles
{
    public class DocConnectProfile : Profile
    {
        public DocConnectProfile()
        {
            CreateMap<SpecialityAddDTO, Speciality>()
                .ReverseMap();

            CreateMap<SpecialityUpdateDTO, Speciality>()
                .ReverseMap();

            CreateMap<Speciality, SpecialityResultDTO>()
                .ReverseMap();

            CreateMap<UserRegistrationDTO, User>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => Guid.NewGuid().ToString()))
                .ReverseMap();

            CreateMap<User, UserResultDTO>()
                .ReverseMap();

            CreateMap<User, UserEmailCredentialsDTO>()
                .ReverseMap();

            CreateMap<TokenAddDTO, Token>()
                .ReverseMap();

            CreateMap<Token, TokenResultDTO>()
                .ReverseMap();

            CreateMap<UserResultDTO, Token>()
                .ReverseMap();

            CreateMap<TokenResultDTO, TokenAddDTO>()
                .ReverseMap();

            CreateMap<UserResultDTO, TokenGenerateDTO>()
                .ReverseMap();

            CreateMap<User, TokenGenerateDTO>()
                .ReverseMap();

            CreateMap<CityResultDTO, City>()
                .ReverseMap();

            CreateMap<DoctorAddDTO, Doctor>()
                .ReverseMap();

            CreateMap<DoctorUpdateDTO, Doctor>()
                .ReverseMap();

            CreateMap<Doctor, DoctorResultDTO>()
                .ReverseMap();

            CreateMap<Doctor, User>()
                .ForMember(d => d.DoctorId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.PasswordHash, o => o.Ignore())
                .ReverseMap();

            CreateMap<DetailedDoctorInfo, DetailedDoctorInfoResultDTO>()
                .ReverseMap();

            CreateMap<DoctorSearchModel, DoctorSearchResultDTO>()
                .ReverseMap();

            CreateMap<User, UserForgotPasswordDTO>()
                .ReverseMap();

            CreateMap<User, ValidateResetTokenDTO>()
                .ReverseMap();

            CreateMap<Location, LocationResultDTO>()
                .ReverseMap();

            CreateMap<LocationDetailedModel, LocationDetailedResultDTO>()
                .ReverseMap();

            CreateMap<AppointmentAddDTO, Appointment>()
                .ForMember(s => s.TimeSlot, o => o.MapFrom(a => new DateTime(a.TimeSlot.Year, a.TimeSlot.Month, a.TimeSlot.Day, a.Hour, default, default)))
                .ReverseMap();

            CreateMap<AppointmentUpdateDTO, Appointment>()
                .ReverseMap();

            CreateMap<Appointment, AppointmentResultDTO>()
                .ReverseMap();

            CreateMap<AppointmentDetailedModel, AppointmentDetailedResultDTO>()
                .ReverseMap();

            CreateMap<List<AppointmentResultDTO>, AppointmentDoctorResultDTO>()
                .ReverseMap();

            CreateMap<AppointmentDetailedModel, AppointmentDetailedEmailDTO>()
                .ForMember(s => s.Email, o => o.MapFrom(p => p.Patient.User.Email))
                .ReverseMap();
        }
    }
}
