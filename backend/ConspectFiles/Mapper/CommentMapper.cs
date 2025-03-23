using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConspectFiles.Dto.CommentDTO;
using ConspectFiles.Model;

namespace ConspectFiles.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                AuthorName = commentModel.AuthorName,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                ConspectId = commentModel.ConspectId
            };
        }
    }
}