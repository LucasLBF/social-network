using AutoMapper;
using SocialNetwork.API.Models;
using SocialNetwork.Data.Entities;

namespace SocialNetwork.API.Profiles
{
    public class UserProfile : AutoMapper.Profile
    {
        private const int DEFAULT_LIKES = 0;
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .ForMember(
                dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<PostUserModel, User>();

            CreateMap<ProfileModel, Data.Entities.Profile>()
                .ForMember(
                dest => dest.Likes,
                opt => opt.MapFrom(src => DEFAULT_LIKES));

            CreateMap<Data.Entities.Profile, ProfileModel>()
                .ForMember(
                dest => dest.Likes,
                opt => opt.MapFrom(src => src.Likes))
                .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => src.Description));

            CreateMap<PostPersonalUserModel, PersonalUser>()
                .ForSourceMember(src => src.FirstName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.LastName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Email, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Password, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Profile, opt => opt.DoNotValidate());

            CreateMap<PostEnterpriseUserModel, EnterpriseUser>()
                .ForSourceMember(src => src.FirstName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.LastName, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Email, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Password, opt => opt.DoNotValidate())
                .ForSourceMember(src => src.Profile, opt => opt.DoNotValidate());

            CreateMap<(User user, PersonalUser personalUser), PersonalUserModel>()
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.user.FirstName} {src.user.LastName}"))
                .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.user.Email))
                .ForMember(dest => dest.Profile,
                opt => opt.MapFrom(src => src.user.Profile))
                .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.personalUser.Status))
                .ForMember(dest => dest.Genre,
                opt => opt.MapFrom(src => src.personalUser.Genre))
                .ForMember(dest => dest.Age,
                opt => opt.MapFrom(src => src.personalUser.Age));

            CreateMap<(User user, EnterpriseUser enterpriseUser), EnterpriseUserModel>()
                .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.user.FirstName} {src.user.LastName}"))
                .ForMember(dest => dest.Email,
                opt => opt.MapFrom(src => src.user.Email))
                .ForMember(dest => dest.Profile,
                opt => opt.MapFrom(src => src.user.Profile))
                .ForMember(dest => dest.Category,
                opt => opt.MapFrom(src => src.enterpriseUser.Category));
        }
    }
}
