using CRM_Code.Readers;
using CRM_Code.Books;
using System;
using CRM_Code.Shared;
using Volo.Abp.AutoMapper;
using CRM_Code.Authors;
using AutoMapper;

namespace CRM_Code;

public class CRM_CodeApplicationAutoMapperProfile : Profile
{
    public CRM_CodeApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Author, AuthorDto>();

        CreateMap<Book, BookDto>();
        CreateMap<BookWithNavigationProperties, BookWithNavigationPropertiesDto>();
        CreateMap<Author, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Reader, ReaderDto>();
        CreateMap<ReaderWithNavigationProperties, ReaderWithNavigationPropertiesDto>();
        CreateMap<Book, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Title));
    }
}