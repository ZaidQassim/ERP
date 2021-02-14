
using System.Collections.Generic;
using System.Linq;
using Appapi.Dtos;
using Appapi.Dtos.CustomerAccord;
using Appapi.Dtos.Employe;
using Appapi.Dtos.EmployeAccord;
using Appapi.Models;
using AutoMapper;


namespace Appapi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ChatDto, Chat>()
                   .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null && srcMember.ToString() != ""));
            CreateMap<RoomDto, Room>()
                  .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null && srcMember.ToString() != ""));
            CreateMap<AddMail, EmailService.EmailConfiguration>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null && srcMember.ToString() != ""));

            // users
            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForEditDto, User>();
            CreateMap<User, UserForListDto>();

            // employe
            CreateMap<EmployeForAddDto, Employe>();
            CreateMap<Employe, EmployeForListDto>();
            CreateMap<Employe, EmployeForDetailsDto>();

            // employe Attachment
            CreateMap<EmployeAttachmentForAddDto, EmployeAttachment>();
            CreateMap<EmployeAttachment, EmployeAttachmentForListDto>();

            // employe Email
            CreateMap<EmployeEmailForAddDto, EmployeEmail>();
            CreateMap<EmployeEmail, EmployeEmailForListDto>();

            // employe TelephoneNumbers
            CreateMap<EmployeTelephoneNumbersForAddDto, EmployeTelephoneNumber>();
            CreateMap<EmployeTelephoneNumber, EmployeTelephoneNumbersForListDto>();

            // Employe PaymentCard
            CreateMap<EmployePaymentCardForAddDto, EmployePaymentCard>();
            CreateMap<EmployePaymentCard, EmployePaymentCardForListDto>();

            // Employe Accord 
            CreateMap<EmployeAccordForAddDto, EmployeAccord>();
            CreateMap<EmployeAccord, EmployeAccordForListDto>();
            CreateMap<EmployeAccord, EmployeAccordForDetailsDto>();
            CreateMap<EmployeAccordAttachment, EmployeAccordAttachmentForListDto>();
            CreateMap<EmployeAccordAttachmentForAddDto, EmployeAccordAttachment>();


            // Customer Accord           
            CreateMap<CustomerAccordForAddDto, Customer>();
            CreateMap<CustomerAccordForAddDto, CustomerAccord>();
            CreateMap<Customer, CustomersForListDto>();
            CreateMap<CustomerAccord, CustomerAccordForDetailsDto>();
            CreateMap<CustomerAccordAttachment, CustomerAccordAttachmentForListDto>();


        }
    }
}