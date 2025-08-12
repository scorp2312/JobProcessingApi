namespace JobCreator.Models;

using System.Collections.Generic;

public class PaginatedList<T>(List<T> items, int totalItems, int pageIndex, int pageSize)
{
    public List<T> Items { get; } = items;

    public int PageIndex { get; } = pageIndex;

    public int TotalPages { get; } = (int)Math.Ceiling(totalItems / (double)pageSize);

    public int TotalItems { get; } = totalItems;

    public bool HasPrev => this.PageIndex > 1;

    public bool HasNext => this.PageIndex < this.TotalPages;
}