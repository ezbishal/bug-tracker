using AutoMapper;
using BugTracker.Shared.Models;

namespace BugTracker.Server.Helpers;

public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<CreateProjectModel, ProjectModel>();
        CreateMap<GetProjectModel, ProjectModel>();
        CreateMap<UpdateProjectModel, ProjectModel>();

        CreateMap<CreateBugModel, BugModel>();
        CreateMap<GetBugModel, BugModel>();
        CreateMap<UpdateBugModel, BugModel>();

        CreateMap<CreateCommentModel, CommentModel>();
        CreateMap<GetCommentModel, CommentModel>();
        CreateMap<UpdateCommentModel, CommentModel>();
    }
}