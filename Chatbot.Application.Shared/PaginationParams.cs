using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Application.Shared
{
	public class PaginationParams
	{
		private int _pageSize = 20;
		private int MaxPageSize = 100;
		public string? SearchString { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = value < MaxPageSize ? value : MaxPageSize;
			}
		}

	}
}
