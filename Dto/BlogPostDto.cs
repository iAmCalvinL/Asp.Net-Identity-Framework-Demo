namespace identity_demo.Dto
{
    public class DatePayload
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
    }

    public class BlogPostDto
    {
        public required DatePayload Date { get; set; }
        public required string Text { get; set; }
    }

    public class UpdateBlogPostDto
    {
        public required int Id { get; set; }
        public required DatePayload Date { get; set; }
        public required string Text { get; set; }
    }
}