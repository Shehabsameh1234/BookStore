

namespace BookStore.Core.Helpers
{
    public class QuerySpecParameters
    {
       
        private int pageSize=5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? Sort { get; set; }
        public int? CategoryId { get; set; }
        
    }
}
