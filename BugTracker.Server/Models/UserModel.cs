using System;

namespace BugTracker.Shared.UserModels;
public class UserModel : RegisterUserModel
{
    public Guid Id { get; set; }
}
