using AutoMapper;
using WebApi.Models;
using WebApi.Models.UserModelExtensions;
using WebApi.Models.ContentModelExtensions;
using DAL.Entities;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Create a map using CreateMap - Source object -> Destination object
        /// </summary>
        public AutoMapperProfile()
        {
            CreateContentMappings();

            CreateUserContentMappings();

            CreateMap<UsersTbl, UserModel>()
                .ForMember(src => src.Password, opt => opt.Ignore()) //Password is not passed from business layer to model layer
                .ForSourceMember(src => src.UserDetailsTbl, opt => opt.Ignore());
            CreateMap<UserModel, UsersTbl>()
                .ForMember(dest => dest.UserDetailsTbl, opt => opt.Ignore());

            CreateMap<UserDetailsBaseAdminModel, UserDetailsTbl>()
                .ForMember(dest => dest.ContentTblCreatedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ContentTblModifiedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.InverseCreatedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.InverseModifiedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserContentTbl, opt => opt.Ignore())
                .ForMember(dest => dest.UserType, opt => opt.Ignore());
            CreateMap<UserDetailsTbl, UserDetailsBaseAdminModel>()
                .ForSourceMember(src => src.ContentTblCreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.ContentTblModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.CreatedBy, opt => opt.Ignore())
                .ForSourceMember(src => src.CreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.CreatedDate, opt => opt.Ignore())
                .ForSourceMember(src => src.InverseCreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.InverseModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.IsDeleted, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedBy, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedDate, opt => opt.Ignore())
                .ForSourceMember(src => src.Role, opt => opt.Ignore())
                .ForSourceMember(src => src.User, opt => opt.Ignore())
                .ForSourceMember(src => src.UserContentTbl, opt => opt.Ignore())
                .ForSourceMember(src => src.UserType, opt => opt.Ignore());

            //Save user
            //CreateMap<UserSaveModel, UsersTbl>()
            //    .ForSourceMember(src => src.RoleId, opt => opt.Ignore())
            //    .ForSourceMember(src => src.UserEmail, opt => opt.Ignore())
            //    .ForSourceMember(src => src.UserFirstName, opt => opt.Ignore())
            //    .ForSourceMember(src => src.UserLastName, opt => opt.Ignore())
            //    .ForSourceMember(src => src.UserTypeId, opt => opt.Ignore())
            //    .ForMember(dest => dest.UserDetailsTbl, opt => opt.Ignore());
            //CreateMap<UserSaveModel, UserDetailsTbl>()
            //    .ForSourceMember(src => src.Username, opt => opt.Ignore())
            //    .ForSourceMember(src => src.Password, opt => opt.Ignore())
            //    .ForMember(dest => dest.ContentTblCreatedByNavigation, opt => opt.Ignore())
            //    .ForMember(dest => dest.ContentTblModifiedByNavigation, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedByNavigation, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            //    .ForMember(dest => dest.InverseCreatedByNavigation, opt => opt.Ignore())
            //    .ForMember(dest => dest.InverseModifiedByNavigation, opt => opt.Ignore())
            //    .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            //    .ForMember(dest => dest.ModifiedBy, opt => opt.Ignore())
            //    .ForMember(dest => dest.ModifiedByNavigation, opt => opt.Ignore())
            //    .ForMember(dest => dest.ModifiedDate, opt => opt.Ignore())
            //    .ForMember(dest => dest.Role, opt => opt.Ignore())
            //    .ForMember(dest => dest.User, opt => opt.Ignore())
            //    .ForMember(dest => dest.UserContentTbl, opt => opt.Ignore())
            //    .ForMember(dest => dest.UserType, opt => opt.Ignore());

            //Get user list
            //CreateMap<UserDetailsTbl, UserListModel>()
            //    .ForSourceMember(src => src.ContentTblCreatedByNavigation, opt => opt.Ignore())
            //    .ForSourceMember(src => src.ContentTblModifiedByNavigation, opt => opt.Ignore())
            //    .ForSourceMember(src => src.CreatedByNavigation, opt => opt.Ignore())
            //    .ForSourceMember(src => src.InverseCreatedByNavigation, opt => opt.Ignore())
            //    .ForSourceMember(src => src.InverseModifiedByNavigation, opt => opt.Ignore())
            //    .ForSourceMember(src => src.ModifiedByNavigation, opt => opt.Ignore())
            //    .ForSourceMember(src => src.Role, opt => opt.Ignore())
            //    .ForSourceMember(src => src.User, opt => opt.Ignore())
            //    .ForSourceMember(src => src.UserContentTbl, opt => opt.Ignore())
            //    .ForSourceMember(src => src.UserType, opt => opt.Ignore())
            //    .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedByNavigation.UserFirstName))
            //    .ForMember(dest => dest.ModifiedByName, opt => opt.MapFrom(src => src.ModifiedByNavigation.UserFirstName))
            //    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))
            //    .ForMember(dest => dest.UserTypeName, opt => opt.MapFrom(src => src.UserType.UserTypeDisplayName));

            CreateMap<UserDetailsTbl, UserDetailsModel>()
                .ForSourceMember(src => src.ContentTblCreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.ContentTblModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.CreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.InverseCreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.InverseModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.Role, opt => opt.Ignore())
                .ForSourceMember(src => src.User, opt => opt.Ignore())
                .ForSourceMember(src => src.UserContentTbl, opt => opt.Ignore())
                .ForSourceMember(src => src.UserType, opt => opt.Ignore());
            CreateMap<UserDetailsModel, UserDetailsTbl>()
                .ForMember(dest => dest.ContentTblCreatedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ContentTblModifiedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.InverseCreatedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.InverseModifiedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.ModifiedByNavigation, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserContentTbl, opt => opt.Ignore())
                .ForMember(dest => dest.UserType, opt => opt.Ignore());

            CreateMap<RolesTbl, RoleModel>()
                .ForSourceMember(src => src.UserDetailsTbl, opt => opt.Ignore());
            CreateMap<RoleModel, RolesTbl>()
                .ForMember(dest => dest.UserDetailsTbl, opt => opt.Ignore());

            CreateMap<UserTypesTbl, UserTypesModel>()
                .ForSourceMember(src => src.UserDetailsTbl, opt => opt.Ignore());
            CreateMap<UserTypesModel, UserTypesTbl>()
                .ForMember(dest => dest.UserDetailsTbl, opt => opt.Ignore());
        }

        private void CreateContentMappings()
        {
            CreateMap<ContentTbl, ContentModel>()
                .ForSourceMember(src => src.CreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.UserContentTbl, opt => opt.Ignore());

            CreateMap<ContentTbl, ContentBaseModel>()
                .ForSourceMember(src => src.IsDeleted, opt => opt.Ignore())
                .ForSourceMember(src => src.CreatedDate, opt => opt.Ignore())
                .ForSourceMember(src => src.CreatedBy, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedDate, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedBy, opt => opt.Ignore())
                .ForSourceMember(src => src.CreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.UserContentTbl, opt => opt.Ignore());
            CreateMap<ContentBaseModel, ContentTbl>()
                .ForMember(src => src.IsDeleted, opt => opt.Ignore())
                .ForMember(src => src.CreatedDate, opt => opt.Ignore())
                .ForMember(src => src.CreatedBy, opt => opt.Ignore())
                .ForMember(src => src.ModifiedDate, opt => opt.Ignore())
                .ForMember(src => src.ModifiedBy, opt => opt.Ignore())
                .ForMember(src => src.CreatedByNavigation, opt => opt.Ignore())
                .ForMember(src => src.ModifiedByNavigation, opt => opt.Ignore())
                .ForMember(src => src.UserContentTbl, opt => opt.Ignore());

            //Content list
            CreateMap<ContentTbl, ContentListModel>()
                .ForSourceMember(src => src.CreatedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.ModifiedByNavigation, opt => opt.Ignore())
                .ForSourceMember(src => src.UserContentTbl, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.CreatedByNavigation.UserFirstName))
                .ForMember(dest => dest.ModifiedByName, opt => opt.MapFrom(src => src.ModifiedByNavigation.UserFirstName));
        }

        private void CreateUserContentMappings()
        {
            CreateMap<UserContentTbl, UserContentListModel>()
                .ForSourceMember(src => src.Content, opt => opt.Ignore())
                .ForSourceMember(src => src.User, opt => opt.Ignore())
                .ForMember(dest => dest.ContentUrl, opt => opt.MapFrom(src => src.Content.ContentUrl))
                .ForMember(dest => dest.ContentName, opt => opt.MapFrom(src => src.Content.ContentName))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.Content.ContentType));
        }
    }
}