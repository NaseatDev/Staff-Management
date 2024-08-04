using StaffManagement.Service.Commons.ResponseResult;
using StaffManagement.Service.Dtos;
using StaffManagement.Service.Dtos.Staff;

namespace StaffManagement.Service.Repositories.Staff
{
    public interface IStaffRepository
    {
        Task<PaginatedResult<StaffDto>> GetAllAsync(PaginatedFilterDto paginationFilter, StaffFilterDto filter);
        Task<ResponseResult<bool>> SaveAsync(StaffDto data);
        Task<ResponseResult<bool>> UpdateAsync(StaffDto data);
        Task<ResponseResult<StaffDto>> GetByIdAsync(string staffId);
        Task<ResponseResult<bool>> DeleteAsync(string staffId);
    }
}
