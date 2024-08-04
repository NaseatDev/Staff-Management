using StaffManagement.Portal.Commons;
using StaffManagement.Portal.Models.Staff;

namespace StaffManagement.Portal.Services.Staff
{
    public interface IStaffService
    {
        Task<PaginatedResponseModel<StaffModel>> GetAllAsync(int? pageIndex, StaffFilterModel? filter);
        Task<PageResponseModel<bool>> CreateAsync(StaffModel staffModel);
        Task<PageResponseModel<bool>> UpdateAsync(StaffModel staffModel);
        Task<PageResponseModel<StaffModel>> GetByIdAsync(string staffId);
        Task<PageResponseModel<bool>> DeleteAsync(string staffId);
    }
}
