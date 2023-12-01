using AutoMapper;
using BugTrackerApi.Features.Projects.AddProjectEndpoint;
using BugTrackerApi.Features.Projects.GetProjectByIdEndpoint;
using BugTrackerApi.Features.Projects.UpdateProjectEndpoint;
using BugTrackerApi.Models.Projects;

namespace BugTrackerApi.Helpers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<ProjectModel, GetProjectByIdResponse>().ReverseMap();
        CreateMap<ProjectModel, AddProjectRequest>().ReverseMap();
        CreateMap<ProjectModel, UpdateProjectRequest>().ReverseMap();
    }
}