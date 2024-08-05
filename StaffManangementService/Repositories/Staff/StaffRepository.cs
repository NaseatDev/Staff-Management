using AutoMapper;
using StaffManagement.Service.Commons.ResponseResult;
using StaffManagement.Service.Dtos;
using StaffManagement.Service.Dtos.Staff;
using StaffManagement.Service.Extensions;
using StaffManagement.Service.Models;

namespace StaffManagement.Service.Repositories.Staff
{
    public class StaffRepository : IStaffRepository
    {
        private readonly StaffDbContext _context;
        private readonly IMapper _mapper;
        public StaffRepository(StaffDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<StaffDto>> GetAllAsync(PaginatedFilterDto paginationFilter, StaffFilterDto filter)
        {
            try
            {
                var queryable = from c in _context.Staff
                                select c;

                if (!string.IsNullOrEmpty(filter.StaffId))
                {
                    queryable = queryable.Where(s => s.StaffId.Equals(filter.StaffId));
                }

                if (filter.Gender > 0)
                {
                    queryable = queryable.Where(s => s.Gender.Equals(filter.Gender));
                }

                if (!string.IsNullOrEmpty(filter.SearchText))
                {
                    queryable = queryable.Where(s => s.StaffId.Contains(filter.SearchText)
                     || s.FullName.Contains(filter.SearchText)
                     );
                }

                var mappedQueryable = _mapper.ProjectTo<StaffDto>(queryable);
                var paginatedResult = await mappedQueryable.OrderByDescending(o => o.CreatedDate).ToPaginatedListAsync(
                    paginationFilter.PageNumber,
                    paginationFilter.PageSize,
                    CancellationToken.None
                );

                return paginatedResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        public async Task<ResponseResult<bool>> SaveAsync(StaffDto data)
        {
            try
            {
                var existingStaff = await _context.Staff.FindAsync(data.StaffId);
                if (existingStaff != null)
                {
                    return await ResponseResult<bool>.FailureAsync("Duplicate StaffId is not allowed.", 409);
                }

                var saveDate = _mapper.Map<Models.Staff>(data);
                saveDate.CreatedDate = DateTime.Now;
                await _context.Staff.AddAsync(saveDate);
                await _context.SaveChangesAsync();
                return await ResponseResult<bool>.SuccessAsync(true, "Data has bee save.");
            }
            catch (Exception e)
            {
                return await ResponseResult<bool>.FailureAsync(e.Message, 500);
            }
        }

        public async Task<ResponseResult<bool>> UpdateAsync(StaffDto data)
        {
            try
            {
                var existingStaff = await _context.Staff.FindAsync(data.StaffId);
                if (existingStaff == null)
                {
                    return await ResponseResult<bool>.FailureAsync("Staff not found.");
                }

                _mapper.Map(data, existingStaff);
                existingStaff.CreatedDate = existingStaff.CreatedDate;
                existingStaff.UpdatedDate = DateTime.Now;
                _context.Staff.Update(existingStaff);
                await _context.SaveChangesAsync();

                return await ResponseResult<bool>.SuccessAsync(true, "Data has been updated.");
            }
            catch (Exception e)
            {
                return await ResponseResult<bool>.FailureAsync(e.Message, 500);
            }
        }

        public async Task<ResponseResult<StaffDto>> GetByIdAsync(string staffId)
        {
            try
            {
                var staffEntity = await _context.Staff.FindAsync(staffId);
                if (staffEntity == null)
                {
                    return await ResponseResult<StaffDto>.FailureAsync("Staff not found.", 404);
                }

                var staffDto = _mapper.Map<StaffDto>(staffEntity);
                return await ResponseResult<StaffDto>.SuccessAsync(staffDto, "Data has been retrieved.");
            }
            catch (Exception e)
            {
                return await ResponseResult<StaffDto>.FailureAsync(e.Message, 500);
            }
        }

        public async Task<ResponseResult<bool>> DeleteAsync(string staffId)
        {
            try
            {
                var staffEntity = await _context.Staff.FindAsync(staffId);
                if (staffEntity == null)
                {
                    return await ResponseResult<bool>.FailureAsync("Staff not found.", 404);
                }
                _context.Staff.Remove(staffEntity);
                await _context.SaveChangesAsync();

                return await ResponseResult<bool>.SuccessAsync(true, "Data has been deleted.");
            }
            catch (Exception e)
            {
                return await ResponseResult<bool>.FailureAsync(e.Message, 500);
            }
        }
    }
}
