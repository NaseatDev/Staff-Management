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
            CreateMap<StaffDto, Staff>();
            CreateMap<Staff, StaffDto>();
            CreateMap<Staff, StaffReadDto>();

        }

    }
}
