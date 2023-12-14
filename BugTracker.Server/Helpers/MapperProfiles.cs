using AutoMapper;
using BugTracker.Server.Features.Projects.AddProjectEndpoint;
using BugTracker.Server.Features.Projects.GetProjectByIdEndpoint;
using BugTracker.Server.Features.Projects.UpdateProjectEndpoint;
using BugTracker.Server.Models;

namespace BugTracker.Server.Helpers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<ProjectModel, GetProjectByIdResponse>().ReverseMap();
        CreateMap<ProjectModel, AddProjectResponse>().ReverseMap();
        CreateMap<ProjectModel, UpdateProjectResponse>().ReverseMap();
    }
}