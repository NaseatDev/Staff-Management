using AutoMapper;
using StaffManagement.Service.Dtos.Staff;
using StaffManagement.Service.Models;

namespace StaffManagement.Service.AutoMapper
{
    public class StaffMapper : Profile
    {
        public StaffMapper()
        {
            AllowNullDestinationValues = null;
            CreateMap<StaffDto, Staff>()
                .ForMember(des => des.Birthday, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.Birthday)));
            CreateMap<Staff, StaffDto>()
                .ForMember(des => des.Birthday, opt => opt.MapFrom(src => src.Birthday.ToDateTime(new TimeOnly())));
            CreateMap<Staff, StaffReadDto>();

        }

    }
}
