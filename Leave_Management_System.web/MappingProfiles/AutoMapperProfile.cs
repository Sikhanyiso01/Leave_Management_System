using AutoMapper;
using Leave_Management_System.web.Data;
using Leave_Management_System.web.Models.LeaveTypes;


namespace Leave_Management_System.web.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.name));

            CreateMap<LeaveTypeCreateViewModel, LeaveType>();
            CreateMap<LeaveTypeEditViewModel, LeaveType >().ReverseMap();



        }
       
    }
}
