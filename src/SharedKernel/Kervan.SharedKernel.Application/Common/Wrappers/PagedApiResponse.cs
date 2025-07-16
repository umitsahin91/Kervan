namespace Kervan.SharedKernel.Application.Common.Wrappers;

public class PagedApiResponse<T> : ApiResponse<List<T>> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }

    public PagedApiResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
        : base(data, 200)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)System.Math.Ceiling(totalRecords / (double)pageSize);
    }
}