namespace PROG6212.Models
{
    public class ReviewClaimsViewModel
    {
        public IEnumerable<Claim> Claims { get; set; }
        public string StatusFilter { get; set; }
        public DateTime? DateFilter { get; set; }
    }
}