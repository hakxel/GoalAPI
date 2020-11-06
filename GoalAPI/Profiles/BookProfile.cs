using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace GoalAPI.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Entities.Book, Models.BookDto>();
            CreateMap<Models.BookForCreationDto, Entities.Book>();
            CreateMap<Entities.Book, Models.BookForUpdateDto>().ReverseMap();
        }
    }
}
