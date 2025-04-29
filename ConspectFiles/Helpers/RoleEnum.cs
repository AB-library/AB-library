using System.Text.Json.Serialization;

namespace ConspectFiles.Helpers
{
    public enum RoleEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Admin,
        User,
        Editor,
        Viewer
    }
}