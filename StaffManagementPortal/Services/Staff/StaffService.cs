using StaffManagement.Portal.Commons;
using StaffManagement.Portal.Extensions;
using StaffManagement.Portal.Models.Staff;
using System.Net;
using System.Text;

namespace StaffManagement.Portal.Services.Staff
{
    public class StaffService : IStaffService
    {
        private readonly HttpClient _httpClient;

        public StaffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedResponseModel<StaffModel>> GetAllAsync(int? pageIndex, StaffFilterModel? filter)
        {
            try
            {
                var url = $"Staff?PageNumber={pageIndex}&PageSize=5";
                var queryString = new StringBuilder();
                queryString.Append(url);
                if (filter != null)
                {
                    if (!string.IsNullOrEmpty(filter.SearchText))
                    {
                        queryString.Append($"&SearchText={filter.SearchText}");
                    }
                    if (!string.IsNullOrEmpty(filter.StaffId))
                    {
                        queryString.Append($"&StaffId={filter.StaffId}");
                    }
                    if (filter.Birthday != null)
                    {
                        queryString.Append($"&Birthday={filter.Birthday}");
                    }
                    if (filter.Gender != null)
                    {
                        queryString.Append($"&Gender={filter.Gender}");
                    }
                }
                var finalUrl = queryString.ToString();
                Console.WriteLine($"Fetching data from URL: {finalUrl}");

                var response = await _httpClient.GetAsync(finalUrl);
                if (response.StatusCode == HttpStatusCode.OK)
                    return (await response.ReadContentAs<PaginatedResponseModel<StaffModel>>())!;
                var error = await response.ReadErrorContentAs<ErrorResponseModel>();
                throw new Exception(error?.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageResponseModel<bool>> CreateAsync(StaffModel staffModel)
        {
            try
            {
                var response = await _httpClient.PostAsJson("Staff", staffModel);
                if (response.StatusCode == HttpStatusCode.OK) return (await response.ReadContentAs<PageResponseModel<bool>>())!;
                var error = await response.ReadErrorContentAs<PageResponseModel<bool>>();
                return error!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageResponseModel<bool>> UpdateAsync(StaffModel staffModel)
        {

            try
            {
                var response = await _httpClient.PutAsJson("Staff", staffModel);
                if (response.StatusCode == HttpStatusCode.OK) return (await response.ReadContentAs<PageResponseModel<bool>>())!;
                var error = await response.ReadErrorContentAs<PageResponseModel<bool>>();
                return error!;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageResponseModel<StaffModel>> GetByIdAsync(string staffId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Staff/{staffId}");
                if (response.StatusCode == HttpStatusCode.OK) return (await response.ReadContentAs<PageResponseModel<StaffModel>>())!;
                var error = await response.ReadErrorContentAs<ErrorResponseModel>();
                throw new Exception(error?.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PageResponseModel<bool>> DeleteAsync(string staffId)
        {

            try
            {
                var response = await _httpClient.DeleteAsync($"Staff/{staffId}");
                if (response.StatusCode == HttpStatusCode.OK) return (await response.ReadContentAs<PageResponseModel<bool>>())!; ;
                var error = await response.ReadErrorContentAs<ErrorResponseModel>();
                throw new Exception(error?.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}