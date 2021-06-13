using VideoStore.Enums;

namespace VideoStore.Models
{
	public class Movie
	{
        public string Title { get; set; }
        public PriceCode PriceCode { get; set; }
    }
}
