namespace DownloadToBrain.Models
{
	public class Vocabulary
	{
		public string Title { get; set; }
		public int TotalCountOfWords { get; set; }

		public Vocabulary()
		{

		}

		public Vocabulary(
			string title,
			int totalCountOfWords)
		{
			Title = title;
			TotalCountOfWords = totalCountOfWords;
		}
	}
}
