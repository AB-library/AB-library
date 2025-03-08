using ConspectFiles.Model;
using ConspectFiles.Dto;

namespace ConspectFiles.Mapper
{
    public static class ConspectMapper
    {
        public static ConspectDto ToConspectDto(this Conspect conspectModel)
        {
            return new ConspectDto
            {
                Id = conspectModel.Id,
                Title = conspectModel.Title,
                Content = conspectModel.Content,
                CreatedOn = conspectModel.CreatedOn
            };
        }
    }
}