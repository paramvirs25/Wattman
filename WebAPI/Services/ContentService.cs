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
    public interface IContentService
    {
        Task<ContentModel> GetById(int id);
        Task<List<ContentListModel>> GetList();
        Task<List<ContentTbl>> GetAll();
        ContentCreateGetModel GetForCreate();
        Task<ContentEditGetModel> GetForEdit(int id);

        Task<bool> Create(ContentBaseModel createModel, int operatingUserId);
        Task<bool> Update(ContentBaseModel updateModel, int operatingUserId);
        Task Delete(int id, int operatingUserId);
    }

    public class ContentService : IContentService
    {
        private DataContext _context;
        private IMapper _mapper;

        public ContentService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get content details by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ContentModel> GetById(int id)
        {
            var tblRow = await _context.ContentTbl
                .Where(c => 
                    c.ContentId == id 
                    && !c.IsDeleted)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            //if not found then show error
            if (tblRow == null) { throw new NotFoundException(ContentValidationMessage.CONTENT_NOT_FOUND); }

            return _mapper.Map<ContentModel>(tblRow);
        }

        /// <summary>
        /// Gets list of all contents excluding non-active ones
        /// </summary>
        /// <returns>Returns list of content</returns>
        public async Task<List<ContentListModel>> GetList()
        {
            var tblRows = await _context.ContentTbl
                .Where(c =>
                    !c.IsDeleted)
                .Include(s => s.CreatedByNavigation)
                .Include(s => s.ModifiedByNavigation)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ContentTbl>, List<ContentListModel>>(tblRows);
        }

        /// <summary>
        /// Gets list of all contents excluding non-active ones.
        /// This method is intended to be used across services, where as GetList() method is to be used by Web API controllers
        /// </summary>
        /// <returns>Returns list of content</returns>
        public async Task<List<ContentTbl>> GetAll()
        {
            return await _context.ContentTbl
                .Where(c =>
                    !c.IsDeleted)
                .ToListAsync();
        }

        public ContentCreateGetModel GetForCreate()
        {
            ContentCreateGetModel createGetModel = new ContentCreateGetModel
            {
                ContentType = ContentType.GetAll()
            };

            return createGetModel;
        }

        public async Task<ContentEditGetModel> GetForEdit(int id)
        {
            ContentEditGetModel editGetModel = new ContentEditGetModel
            {
                ContentType = ContentType.GetAll()
            };

            var toEditTblRow = await _context.ContentTbl
                .Where(c =>
                    c.ContentId == id
                    && !c.IsDeleted)
                .AsNoTracking()
                .SingleOrDefaultAsync();

            //if not found then show error
            if (toEditTblRow == null) { throw new NotFoundException(ContentValidationMessage.CONTENT_NOT_FOUND); }

            editGetModel.Content = _mapper.Map<ContentBaseModel>(toEditTblRow);

            return editGetModel;
        }

        public async Task<bool> Create(ContentBaseModel createModel, int operatingUserId)
        {
            //No need to check for duplicate as in some cases same content can be displayed twice.
            //So for now lets allow duplicate content

            ContentTbl tblRow = new ContentTbl();

            //populate table object
            _mapper.Map(createModel, tblRow);

            tblRow.CreatedBy = operatingUserId;
            tblRow.ModifiedBy = operatingUserId;

            await _context.AddAsync(tblRow);
            
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(ContentBaseModel updateModel, int operatingUserId)
        {
            //No need to check for duplicate as in some cases same content can be displayed twice.
            //So for now lets allow duplicate content

            ContentTbl tblRow = await _context.ContentTbl
                .Where(row =>
                    row.ContentId == updateModel.ContentId
                    && !row.IsDeleted)
                .SingleOrDefaultAsync();

            //if no record found then show error
            if (tblRow == null) { throw new NotFoundException(ContentValidationMessage.CONTENT_NOT_FOUND); }

            //populate table objects
            _mapper.Map(updateModel, tblRow);

            tblRow.ModifiedBy = operatingUserId;
            tblRow.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task Delete(int id, int operatingUserId)
        {
            var tblRow = await _context.ContentTbl.SingleOrDefaultAsync(x => x.ContentId == id);

            //if no user is found then show error
            if (tblRow == null) { throw new NotFoundException(ContentValidationMessage.CONTENT_NOT_FOUND); }

            tblRow.IsDeleted = true;
            tblRow.ModifiedBy = operatingUserId;
            tblRow.ModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}