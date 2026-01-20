using AutoMapper;
using GymManagementBLL.ViewModels.MemberSessionViewModels;
using GymManagementBLL.ViewModels.MembershipViewModels;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.PlanViewModel;
using GymManagementBLL.ViewModels.SessionViewModel;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagmentDAL.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.MappingProfiles;

public class MappingProfiles : Profile
{

    public MappingProfiles()
    {
        MappingSession();
        MappingPlan();
        MappingMember();
        MappingTrainer();
        MappingMembership();
        MappingMamberSession();
    }
    

    private void MappingSession()
    {
        CreateMap<Session, SessionViewModel>()
          .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(s => s.SessionTrainer.Name))
          .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(s => s.Category.CategoryName))
          .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());


        CreateMap<CreateSessionViewModel, Session>();
        CreateMap<Session, UpdateSessionViewModel>().ReverseMap();

        CreateMap<Trainer, TrainerSelectViewModel>();

        CreateMap<Category, CategorySelectViewModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(c => c.CategoryName));
    }

    private void MappingPlan()
    {
        CreateMap<Plan , PlanViewModel>();
        CreateMap<Plan, UpdatePlanViewModel>();
        CreateMap<UpdatePlanViewModel, Plan>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src=>DateTime.Now));

    }

    private void MappingMember()
    {

        CreateMap<CreateMemberViewModel, Member>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                BuildingNumber = src.BuildingNumber,
                Street = src.Street,
                City = src.City,
            }))
            .ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealthRecordViewModel));



        CreateMap<Member, MemberViewModel>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"));

        CreateMap<HealthRecord, HealthRecordViewModel>().ReverseMap();

        CreateMap<Member, MemberUpdateViewModel>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

        CreateMap<MemberUpdateViewModel, Member>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .ForMember(dest => dest.Phone, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.Street = src.Street;
                dest.Address.City = src.City;
                dest.UpdatedAt = DateTime.Now;

            });

    }


    private void MappingTrainer()
    {
        CreateMap<CreateTrainerViewModel, Trainer>()
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
            {
                BuildingNumber = src.BuildingNumber,
                City = src.City,
                Street = src.Street,
            }));

        CreateMap<Trainer, TrainerViewModel>()
            .ForMember(dest => dest.Specialties, opt => opt.MapFrom(src => src.Specialties.ToString()));

        CreateMap<Trainer , TrainerDetailsViewModel>()
             .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"))
             .ForMember(dest => dest.DateOfBirth ,opt=>opt.MapFrom(src=>src.DateOfBirth.ToShortDateString()) );

        CreateMap<Trainer, TrainerUpdateViewModel>()
             .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));


        CreateMap<TrainerUpdateViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.Street = src.Street;
                dest.UpdatedAt = DateTime.Now;
            });


       

    }

    private void MappingMembership()
    {
        CreateMap<MemberPlan, MembershipViewModel>()
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
            .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.Name))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.UpdatedAt));


        CreateMap<CreateMembershipViewModel, MemberPlan>();

        CreateMap<Plan, PlanSelectViewModel>();
        CreateMap<Member, MemberSelectViewModel>();

    }

    private void MappingMamberSession()
    {
        CreateMap<MemberSession, MembersForUpcomingSessionViewModel>()
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => src.CreatedAt));



        CreateMap<MemberSession, MembersForOngoingSessionsViewModel>()
          .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name));


        CreateMap<CreateMemberSessionViewModel, MemberSession>();
            
    }


}
