using AutoMapper;
using Motter.Application.Commands.Motos;
using Motter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motter.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateMotoCommand, Moto>();
            CreateMap<Moto, MotoDto>(); 
        }
    }
}
