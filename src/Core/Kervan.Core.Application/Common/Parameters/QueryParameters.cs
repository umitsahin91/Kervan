
namespace Kervan.Core.Application.Common.Parameters;

public class QueryParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    // ?sort=price_desc veya ?sort=name gibi değerleri tutacak.
    public string? Sort { get; set; }

    // ?search=Tshirt gibi değerleri tutacak.
    public string? Search { get; set; }
}