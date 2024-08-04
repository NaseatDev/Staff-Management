using Microsoft.AspNetCore.Mvc;
using StaffManagement.Service.Dtos;
using StaffManagement.Service.Dtos.Staff;
using StaffManagement.Service.Repositories.Staff;

namespace StaffManagement.Api.Controllers
{
    public class StaffController : ApiControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedFilterDto filter, [FromQuery] StaffFilterDto modelFilter)
        {
            try
            {
                var validFilter = new PaginatedFilterDto(filter.PageNumber, filter.PageSize);
                var result = await _staffRepository.GetAllAsync(validFilter, modelFilter);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the staff: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StaffDto staffDto)
        {
            try
            {
                var result = await _staffRepository.SaveAsync(staffDto);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while saving the staff: " + ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] StaffDto staffDto)
        {
            try
            {
                var result = await _staffRepository.UpdateAsync(staffDto);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while update the staff: " + ex.Message);
            }
        }


        [HttpGet("{staffId}")]
        public async Task<IActionResult> GetById(string staffId)
        {
            try
            {
                var result = await _staffRepository.GetByIdAsync(staffId);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the staff: " + ex.Message);
            }
        }

        [HttpDelete("{staffId}")]
        public async Task<IActionResult> DeleteAsync(string staffId)
        {
            try
            {
                var result = await _staffRepository.DeleteAsync(staffId);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while delete the staff: " + ex.Message);
            }
        }
    }
}
