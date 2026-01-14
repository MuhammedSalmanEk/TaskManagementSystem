using System;
namespace API.Auth
{
    public static class FakeUsers
    {
        public static readonly Dictionary<string, string> Users = new()
    {
        { "admin", "Admin" },
        { "user", "User" },
        {"salman","User" }
    };
    }
}

