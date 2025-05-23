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
                Tag = conspectModel.Tag,
                IsDraft = conspectModel.IsDraft,
                CreatedOn = conspectModel.CreatedOn,
                PublishedOn = conspectModel.PublishedOn,
                Comments = conspectModel.Comments.Select(c=>c.ToCommentDto()).ToList()
            };
        }

        public static Conspect ToConspectFromCreateDto(this CreateConspectDto conspectDto)
        {
            return new Conspect
            {
                Title = conspectDto.Title,
                Content = conspectDto.Content,
                Tag = conspectDto.Tag,
                IsDraft = conspectDto.IsDraft
            };
        }
    }
}