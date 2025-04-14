using AutoMapper;
using Lab_05_Roman_Qquelcca.Models;
using Lab_05_Roman_Qquelcca.DTOs;

namespace Lab_05_Roman_Qquelcca.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Estudiante, EstudianteGetDto>();
    }
}