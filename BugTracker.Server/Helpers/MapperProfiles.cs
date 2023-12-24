using AutoMapper;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Helpers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<CreateProjectModel, ProjectModel>().ReverseMap();
        CreateMap<GetProjectModel, ProjectModel>().ReverseMap();
        CreateMap<UpdateProjectModel, ProjectModel>().ReverseMap();

        CreateMap<CreateBugModel, BugModel>().ReverseMap();
        CreateMap<GetBugModel, BugModel>().ReverseMap();
        CreateMap<UpdateBugModel, BugModel>().ReverseMap();

        CreateMap<CreateCommentModel, CommentModel>().ReverseMap();
        CreateMap<GetCommentModel, CommentModel>().ReverseMap();
        CreateMap<UpdateCommentModel, CommentModel>().ReverseMap();
    }
}