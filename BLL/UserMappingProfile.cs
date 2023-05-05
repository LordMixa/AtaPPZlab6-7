using AutoMapper;
using DAL.DBEntities;

namespace BLL
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Show, DBShow>()
                .ForMember(dest => dest.DBShowId, opt => opt.Ignore());

            CreateMap<DBShow, Show>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre))
                .ForMember(dest => dest.CountSeats, opt => opt.MapFrom(src => src.CountSeats))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

            CreateMap<Ticket, DBTicket>()
           .ForMember(dest => dest.DBTicketId, opt => opt.Ignore());

            CreateMap<DBTicket, Ticket>()
                .ForMember(dest => dest.NameShow, opt => opt.MapFrom(src => src.NameShow))
                .ForMember(dest => dest.NameOfOwner, opt => opt.MapFrom(src => src.NameOfOwner))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
