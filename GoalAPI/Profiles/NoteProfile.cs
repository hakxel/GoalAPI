using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace GoalAPI.Profiles
{
    public class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Entities.Note, Models.NoteDto>();
            CreateMap<Models.NoteForCreationDto, Entities.Note>();
            CreateMap<Models.NoteForUpdateDto, Entities.Note>();
        }
    }
}
