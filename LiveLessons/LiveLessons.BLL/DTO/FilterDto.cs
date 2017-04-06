namespace LiveLessons.BLL.DTO
{
    public class FilterDto
    {
        public double CoordX { get; set; }
        public double CoordY { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int MinRate { get; set; }
        public int MaxRate { get; set; }
        public bool SortByDistance { get; set; }
        public bool SortByPrice { get; set; }
        public string SearchString { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
    }
}
