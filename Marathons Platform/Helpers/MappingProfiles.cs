using AutoMapper;
using Marathons_Platfom.Core.Entities;
using Marathons_Platfom.UI2.Infrastructure.DTOs;

namespace Marathons_Platform.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Badge, BadgeDto>().ReverseMap();
            CreateMap<Badge, BadgeCreateDto>().ReverseMap();
            CreateMap<BadgeDto, BadgeCreateDto>().ReverseMap();
            CreateMap<Badge, BadgeUpdateDto>().ReverseMap();
            CreateMap<BadgeDto, BadgeUpdateDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, RoleCreateDto>().ReverseMap();
            CreateMap<ThemeDto, ThemeCreateDto>().ReverseMap();
            CreateMap<Theme, ThemeDto>().ReverseMap();
            CreateMap<Theme, ThemeCreateDto>().ReverseMap();
            CreateMap<Marathon, MarathonDto>().ReverseMap();
            CreateMap<Marathon, MarathonCreateDto>().ReverseMap();
            CreateMap<Marathon, MarathonUpdateDto>().ReverseMap();
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<Exercise, ExerciseCreateDto>().ReverseMap();
            CreateMap<Exercise, ExerciseUpdateDto>().ReverseMap();
            CreateMap<ExerciseDto, ExerciseUpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserLogDto>().ReverseMap();
            CreateMap<Progress, ProgressDto>().ReverseMap();
            CreateMap<Progress, ProgressCreateDto>().ReverseMap();
            CreateMap<User_Badge, UserBadgeDto>().ReverseMap();
            CreateMap<User_Badge, UserBadgeCreateDto>().ReverseMap();
            CreateMap<UserBadgeDto, User_Badge>()
  .ForMember(dest => dest.WhenClaimed, opt => opt.MapFrom(src => src.Claimed));
            CreateMap<User_Badge, UserBadgeDto >()
 .ForMember(dest => dest.Claimed, opt => opt.MapFrom(src => src.WhenClaimed));
            CreateMap<UserBadgeCreateDto, User_Badge>()
    .ForMember(dest => dest.WhenClaimed, opt => opt.MapFrom(src => src.Claimed));
            CreateMap<UserRoleInMarathon, UserRoleInMarathonDto>().ReverseMap();
            CreateMap<UserRoleInMarathon, UserRoleInMarathonCreateDto>().ReverseMap();
            CreateMap<UserExerciseStatus, UserExerciseStatusesDto>().ReverseMap();
            CreateMap<UserExerciseStatusesDto, UserExerciseStatusesCreateDto>().ReverseMap();
            CreateMap<UserExerciseStatus, UserExerciseStatusesCreateDto>().ReverseMap();

            CreateMap<BadgeMarathon, BadgeMarathonDto>().ReverseMap();
            CreateMap<BadgeMarathon, BadgeMarathonCreateDto>().ReverseMap();
        }
    }
}
