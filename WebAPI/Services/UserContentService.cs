using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

using AutoMapper;
using DAL.Entities;

using WebApi.Models;
using WebApi.Models.ContentModelExtensions;
using WebApi.AppConstants;
using WebApi.AppConstants.ValidationMessages;
using WebApi.Helpers.Exceptions;

namespace WebApi.Services
{
    public interface IUserContentService
    {
        Task<List<UserContentListModel>> GetListByUserId(int userId);
        
        Task<List<UserContentTbl>> GetNewUserContent(int userId);

        Task<bool> MarkContentCompleted(int userId, int contentId);
    }

    public class UserContentService : IUserContentService
    {
        private DataContext _context;
        private IMapper _mapper;
        private IContentService _contentService;

        public UserContentService(
            DataContext context,
            IMapper mapper,
            IContentService contentService)
        {
            _context = context;
            _mapper = mapper;
            _contentService = contentService;
        }

        public async Task<List<UserContentListModel>> GetListByUserId(int userId)
        {
            var tblRows = await _context.UserContentTbl
                .Include(uct => uct.Content)
                .Where(uct =>
                    uct.UserId == userId &&
                    !uct.Content.IsDeleted)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<UserContentTbl>, List<UserContentListModel>>(tblRows);
        }

        public async Task<List<UserContentTbl>> GetNewUserContent(int userId)
        {
            //get list of all content
            var contentTbl = await _contentService.GetAll();

            List<UserContentTbl> userContent = new List<UserContentTbl>();
            foreach (var contentRow in contentTbl)
            {
                userContent.Add(
                    new UserContentTbl()
                    {
                        UserId = userId,
                        ContentId = contentRow.ContentId
                    }
                );
            };

            return userContent;
        }

        public async Task<bool> MarkContentCompleted(int userId, int contentId)
        {
            var tblRow = await _context.UserContentTbl
                .Where(uct =>
                    uct.UserId == userId
                    && uct.ContentId == contentId)
                .SingleOrDefaultAsync();

            //if no record is found then show error
            if (tblRow == null) { throw new NotFoundException(UserContentValidationMessage.USER_CONTENT_NOT_FOUND); }

            tblRow.IsComplete = true;
            tblRow.DateCompleted = DateTime.Now;

            await _context.SaveChangesAsync();

            //Another way to run update command without getting data first
            //var tblRow = new UserContentTbl()
            //{
            //    UserId = userId,
            //    ContentId = contentId,
            //    IsComplete = true,
            //    DateCompleted = DateTime.Now
            //};
            //_context.UserContentTbl.Update(tblRow);
            //await _context.SaveChangesAsync();

            return true;
        }
    }
}