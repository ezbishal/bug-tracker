using AutoMapper;
using BugTracker.Server.Models;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Helpers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<ProjectModel, GetProjectModel>().ReverseMap();
        CreateMap<ProjectModel, AddProjectModel>().ReverseMap();
        CreateMap<ProjectModel, UpdateProjectModel>().ReverseMap();
    }
}