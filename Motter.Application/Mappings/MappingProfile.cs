using AutoMapper;
using Motter.Application.Commands.Entregadores;
using Motter.Application.Commands.Locacoes;
using Motter.Application.Commands.Motos;
using Motter.Application.DTOs;
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
            CreateMap<CreateMotoCommand, Moto>().ReverseMap();
            CreateMap<Moto, MotoDto>().ReverseMap();

            CreateMap<Entregador, EntregadorDto>().ReverseMap();
            CreateMap<CreateEntregadorCommand, Entregador>().ReverseMap();



            CreateMap<Locacao, LocacaoDto>().ReverseMap();
            CreateMap<CreateLocacaoCommand, Locacao>().ReverseMap();
        }
    }
}
