using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class TarefaProfile : Profile
{
    public TarefaProfile()
    {
        CreateMap<Tarefa, TarefaDto>().ReverseMap();
    }
}
