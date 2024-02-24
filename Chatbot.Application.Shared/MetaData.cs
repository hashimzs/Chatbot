using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chatbot.Application.Shared
{
	public class PagedList<T>
	{
		public MetaData MetaData { get; set; }
		public List<T> List { get; set; }

		[JsonConstructor]
		public PagedList() { }
		public PagedList(List<T> items, int count, int pageNumber, int pageSize)
		{
			MetaData = new MetaData
			{
				TotalCount = count,
				PageSize = pageSize,
				CurrentPage = pageNumber,
				TotalPages = (int)Math.Ceiling(count / (double)pageSize)
			};
			List = items;

		}
	}
	public class MetaData
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public bool HasPrevious => CurrentPage > 1;
		public bool HasNext => CurrentPage < TotalPages;
	}
}
