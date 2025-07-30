namespace Bulky.Core.Application.Models.Common
{
	public class DataTableViewModel<T>
	{
		public required int Draw { get; set; }
		public required int RecordsTotal { get; set; }
		public required int RecordsFiltered { get; set; }
		public required IEnumerable<T> Data { get; set; }
	}
}
