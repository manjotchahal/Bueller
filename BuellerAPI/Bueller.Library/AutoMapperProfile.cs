using AutoMapper;
using Bueller.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Library
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Assignment, Bueller.Data.Models.Assignment>();
            CreateMap<Book, Bueller.Data.Models.Book>();
            CreateMap<Class, Bueller.Data.Models.Class>();
            CreateMap<File, Bueller.Data.Models.File>();
            CreateMap<Grade, Bueller.Data.Models.Grade>();
            CreateMap<Student, Bueller.Data.Models.Student>();
            CreateMap<Subject, Bueller.Data.Models.Subject>();
            CreateMap<Teacher, Bueller.Data.Models.Teacher>();
        }
    }
}
