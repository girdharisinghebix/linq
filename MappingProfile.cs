using AutoMapper;
using CoreProject.DataAccessLayer.Models;
using CoreProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject.ImplementInterfaceRepsitory.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerBLL>().
          //  ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).
            ForMember(dest => dest.Fname, opt => opt.MapFrom(src => src.FirstName)).
            ForMember(dest => dest.Lname, opt => opt.MapFrom(src => src.LastName)).
            ForMember(dest => dest.Mno, opt => opt.MapFrom(src => src.MobileNumber)).
            ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.EntryDate))
            .ReverseMap();

            CreateMap<Student, StudentBLL>().
        ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.StudentName)).
        ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.FatherName)).ReverseMap();

            CreateMap<StudentAddressDetail, StudentAddressDetailBLL>().ReverseMap();

               
            //  ForMember(dest => dest.StudentId, opts => opts.MapFrom(src=>src.Student.StudentId)).
          //  ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId)).
            //ForMember(dest => dest.PinCode, opt => opt.MapFrom(src => src.PinCode)).

            // ForMember(dest => dest.AddressDetail, opt => opt.MapFrom(src => src.AddressDetail)).ReverseMap();
         


        }
}

}
