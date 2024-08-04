namespace StaffManagement.Service.Dtos
{
    public class PaginatedFilterDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginatedFilterDto()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public PaginatedFilterDto(int pageNo, int pageSize)
        {
            PageNumber = pageNo < 1 ? 1 : pageNo;
            PageSize = pageSize == 0 ? 10 : pageSize;
        }
    }
}
