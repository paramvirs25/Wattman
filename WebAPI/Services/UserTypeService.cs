using System.Collections.Generic;
using AutoMapper;
using WebApi.Models;
using DAL.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public interface IUserTypeService
    {
        Task<IEnumerable<UserTypesModel>> GetAll();
    }

    public class UserTypeService : IUserTypeService
    {
        private DataContext _context;
        private IMapper _mapper;

        public UserTypeService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserTypesModel>> GetAll()
        {
            var userTypesTbl = await _context.UserTypesTbl.ToListAsync();
            return _mapper.Map<List<UserTypesTbl>, IEnumerable<UserTypesModel>>(userTypesTbl);
        }
    }
}