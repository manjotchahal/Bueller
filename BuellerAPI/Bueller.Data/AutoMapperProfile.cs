using AutoMapper;
using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Assignment, Library.Models.Assignment>().ReverseMap();
            CreateMap<Book, Library.Models.Book>().ReverseMap();
            CreateMap<Class, Library.Models.Class>().ReverseMap();
            CreateMap<File, Library.Models.File>().ReverseMap();
            CreateMap<Grade, Library.Models.Grade>().ReverseMap();
            CreateMap<Student, Library.Models.Student>().ReverseMap();
            CreateMap<Subject, Library.Models.Subject>().ReverseMap();
            CreateMap<Teacher, Library.Models.Teacher>().ReverseMap();
        }
    }
}
