using CRM_Code.Readers;
using CRM_Code.Books;
using Volo.Abp.AutoMapper;
using CRM_Code.Authors;
using AutoMapper;

namespace CRM_Code.Web;

public class CRM_CodeWebAutoMapperProfile : Profile
{
    public CRM_CodeWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<AuthorDto, AuthorUpdateDto>();

        CreateMap<BookDto, BookUpdateDto>();

        CreateMap<ReaderDto, ReaderUpdateDto>().Ignore(x => x.BookIds);
    }
}