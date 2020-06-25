using System.Linq;
using AutoMapper;
using Domain;

namespace Application.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(c => c.UserName, opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(c => c.DisplayName, opt => opt.MapFrom(s => s.Author.DisplayName))
                .ForMember(c => c.Image, opt => opt.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }   
    }
}