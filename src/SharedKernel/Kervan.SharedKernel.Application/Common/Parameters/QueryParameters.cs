﻿namespace Kervan.SharedKernel.Application.Common.Parameters;

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

    public string? Sort { get; set; }

    public string? Search { get; set; }
}