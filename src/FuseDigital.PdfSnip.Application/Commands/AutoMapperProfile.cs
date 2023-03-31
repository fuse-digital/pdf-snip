using AutoMapper;
using FuseDigital.PdfSnip.Commands.Dto;
using FuseDigital.PdfSnip.Documents.Dto;

namespace FuseDigital.PdfSnip.Commands;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<SplitOptions, SplitInput>()
            .ReverseMap();
        
        CreateMap<JoinOptions, JoinInput>()
            .ReverseMap();
    }
}