using AutoMapper;
using BugTrackerAPI.Features.AddProject;
using BugTrackerAPI.Features.GetProjectById;
using BugTrackerAPI.Features.UpdateProject;
using BugTrackerAPI.Models.Projects;

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