using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD.Models;
using CRUD.DTO;

namespace CRUD
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<FactoryDto, Factory>();

            CreateMap<UnitDto, Unit>();

            CreateMap<Tank, TankDto>();
        }
    }
}
