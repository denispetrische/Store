using AutoMapper;
using Store.Web.Dtos.HistoryNote;
using Store.Web.Models;

namespace Store.Web.Profiles
{
    public class HistoryNoteProfiles : Profile
    {
        public HistoryNoteProfiles()
        {
            CreateMap<HistoryNote, HistoryNoteHistoryViewDto>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));
            CreateMap<HistoryNote, HistoryNoteStatisticsViewDto>()
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        }
    }
}
