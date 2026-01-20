using AutoMapper;
using GymManagementBLL.services.AttachmentService;
using GymManagementBLL.services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.services.Classes;

public class MemberServices : IMemberServices
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAttachmentService _attachmentService;

    public MemberServices(IUnitOfWork unitOfWork , IMapper mapper , IAttachmentService attachmentService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _attachmentService = attachmentService;
    }

    public bool CreateMember(CreateMemberViewModel createMember)
    {
        try
        {
            //check mail 

            //check phone 

            if (CheckEmail(createMember.Email) || CheckPhone(createMember.Phone)) return false;

            #region manualMap
            //var newMember = new Member()
            //{
            //    Name = createMember.Name,
            //    Email = createMember.Email,
            //    Phone = createMember.Phone,
            //    Gender = createMember.Gender,
            //    DateOfBirth = createMember.DateOfBith,
            //    Address = new Address()
            //    {
            //        City = createMember.City,
            //        Street = createMember.Street,
            //        BuildingNumber = createMember.BuildingNumber,
            //    },
            //    HealthRecord = new HealthRecord()
            //    {
            //        Height = createMember.HealthRecordViewModel.Height,
            //        Weight = createMember.HealthRecordViewModel.Weight,
            //        BloodType = createMember.HealthRecordViewModel.BloodType,
            //        Note = createMember.HealthRecordViewModel.Note
            //    }

            //};
            #endregion
            var photoName = _attachmentService.Upload("Members" , createMember.PhotoFile);
            if (string.IsNullOrEmpty(photoName)) return false;
            var newMember = _mapper.Map<Member>(createMember);
            newMember.Photo = photoName;
            _unitOfWork.GetReporitory<Member>().Add(newMember);
            var isCreated = _unitOfWork.SaveChanges() > 0;
            if (!isCreated)
            {
                _attachmentService.Delete(photoName , "Members");
                return false;
            }

            return isCreated;
        }
        catch (Exception)
        {

            return false;
        }

    }

    public IEnumerable<MemberViewModel> GetAllMembers()
    {
        var member = _unitOfWork.GetReporitory<Member>().GetAll();

        if(member is null || !member.Any()) return [];


        #region ManualMap
        //var memberViewModel = member.Select(m => new MemberViewModel
        //{
        //    Id = m.Id,
        //    Name = m.Name,
        //    Email = m.Email,
        //    Phone = m.Phone,
        //    Photo = m.Photo,
        //    Gender = m.Gender.ToString(),

        //});

        //return memberViewModel;
        #endregion


        return _mapper.Map<IEnumerable< MemberViewModel>>(member);
    }

    public HealthRecordViewModel? GetHealthrecordMemberDetails(int id)
    {
        var memberHealthRecord = _unitOfWork.GetReporitory<HealthRecord>().GetById(id);

        if (memberHealthRecord is null) return null;

        #region ManualMap
        //return new HealthRecordViewModel()
        //{
        //    Height = memberHealthRecord.Height,
        //    Weight = memberHealthRecord.Weight,
        //    BloodType = memberHealthRecord.BloodType,
        //    Note = memberHealthRecord.Note,
        //};
        #endregion
        return _mapper.Map<HealthRecordViewModel>(memberHealthRecord);
    }

    public MemberViewModel? GetMemberDetails(int id)
    {
        var member = _unitOfWork.GetReporitory<Member>().GetById(id);

        if (member is null) return null;

        #region ManualMap
        //var memberView = new MemberViewModel()
        //{
        //    Name = member.Name,
        //    Email = member.Email,
        //    Phone = member.Phone,
        //    Photo = member.Photo,
        //    Gender = member.Gender.ToString(),
        //    Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}",
        //    DateOfBirth = member.DateOfBirth.ToShortDateString(),


        //};
        #endregion

       var memberView = _mapper.Map<MemberViewModel>(member);

        var ActivememberPlan = _unitOfWork.GetReporitory<MemberPlan>().GetAll(x => x.MemberId == id && x.Status == "Active").FirstOrDefault();

        if(ActivememberPlan is not null)
        {
            memberView.MemberShipStartDate = ActivememberPlan.CreatedAt.ToShortDateString();
            memberView.MemberShipEndDate = ActivememberPlan.EndDate.ToShortDateString();

            var plan = _unitOfWork.GetReporitory<Plan>().GetById(ActivememberPlan.PlanId);

            memberView.PlanName = plan.Name;
        }


        return memberView;
    }

    public MemberUpdateViewModel? GetMemberToUpdate(int id)
    {
        var member = _unitOfWork.GetReporitory<Member>().GetById(id);

        if (member is null) return null;

        #region manualMap
        //return new MemberUpdateViewModel()
        //{
        //    Email = member.Email,
        //    Phone = member.Phone,
        //    Photo = member.Photo,
        //    Name = member.Name,
        //    City = member.Address.City,
        //    BuildingNumber = member.Address.BuildingNumber,
        //    Street = member.Address.Street
        //};

        #endregion

        return _mapper.Map<MemberUpdateViewModel>(member);


    }

    public bool RemoveMember(int id)
    {
        var member = _unitOfWork.GetReporitory<Member>().GetById(id);

        if (member is null) return false;

        //get all active sessions with this member 

        var SessionIds = _unitOfWork.GetReporitory<MemberSession>()
            .GetAll(x=>x.MemberId == id ).Select(s=>s.SessionId);


        var HasActive = _unitOfWork.GetReporitory<Session>()
            .GetAll(x=> SessionIds.Contains(x.Id) && x.StartDate > DateTime.Now).Any();


        // if he has active session can not delete 
        if(HasActive) return false;

        var memberPlans = _unitOfWork.GetReporitory<MemberPlan>()
                                            .GetAll(x => x.MemberId == id);

        try
        {
            if (memberPlans.Any())
            {
                foreach (var memberSessions in memberPlans)
                {
                    _unitOfWork.GetReporitory<MemberPlan>().Delete(memberSessions);
                }
            }

             _unitOfWork.GetReporitory<Member>().Delete(member);
            var isDeleted =  _unitOfWork.SaveChanges() > 0;
            if (isDeleted)
            {
                _attachmentService.Delete(member.Photo , "Members");
            }
          
                return isDeleted;
           
        }
        catch (Exception)
        {
            return false;
        }



    }

    public bool UpdateMember(int id, MemberUpdateViewModel updateMember)
    {
        try
        {
            var checkMail = _unitOfWork.GetReporitory<Member>().GetAll(x => x.Email == updateMember.Email && x.Id != id).Any();
            var checkPhone = _unitOfWork.GetReporitory<Member>().GetAll(x => x.Phone == updateMember.Phone && x.Id != id).Any();

            if (checkMail || checkPhone) return false;
           

            var member = _unitOfWork.GetReporitory<Member>().GetById(id);

            if (member is null) return false;
            #region manualMap
            //member.Phone = updateMember.Phone;
            //member.Email = updateMember.Email;
            //member.Address.BuildingNumber = updateMember.BuildingNumber;
            //member.Address.City = updateMember.City;
            //member.Address.Street = updateMember.Street;
            //member.UpdatedAt = DateTime.Now;
            #endregion

            _mapper.Map(updateMember, member);



            _unitOfWork.GetReporitory<Member>().Update(member) ;
            return _unitOfWork.SaveChanges() > 0;  

        }
        catch (Exception)
        {

            return false;
        }
    }


    private bool CheckEmail(string email) // memberId
    {
        return _unitOfWork.GetReporitory<Member>().GetAll(x => x.Email == email).Any();

        // x.id != memberId

    }
    private bool CheckPhone(string phone)
    {
        return _unitOfWork.GetReporitory<Member>().GetAll(x => x.Phone == phone).Any();

    }





}
