// Kervan.Core.Application/Common/Wrappers/PagedApiResponse.cs
using System.Collections.Generic;

namespace Kervan.Core.Application.Common.Wrappers;

// Bu sınıf, ApiResponse<T>'den miras alır. Böylece IsSuccess, Errors gibi temel özellikleri de barındırır.
// Ama T'nin bir liste olmasını zorunlu kılar (where T : class).
public class PagedApiResponse<T> : ApiResponse<List<T>> where T : class
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }

    // Bu constructor, başarılı bir sayfalanmış cevap oluşturur.
    public PagedApiResponse(List<T> data, int pageNumber, int pageSize, int totalRecords)
        : base(data, 200) // Base class'ın Success constructor'ını çağırır.
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
        TotalPages = (int)System.Math.Ceiling(totalRecords / (double)pageSize);
    }
}