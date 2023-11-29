using AutoMapper;
using BugTrackerApi.Features.AddProject;
using BugTrackerApi.Features.GetProjectById;
using BugTrackerApi.Features.UpdateProject;
using BugTrackerApi.Models.Projects;

namespace BugTrackerAPI.Helpers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<ProjectModel, GetProjectByIdResponse>().ReverseMap();
        CreateMap<ProjectModel, AddProjectRequest>().ReverseMap();
        CreateMap<ProjectModel, UpdateProjectRequest>().ReverseMap();
    }
}