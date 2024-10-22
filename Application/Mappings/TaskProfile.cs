using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<UserTask, UserTaskDto>().ReverseMap();
    }
}
