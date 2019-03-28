namespace ChatApp.Core.Models
{
	public interface IDataModel
	{
		User User { get; set; }
	}

	public class DataModel : IDataModel
	{
		public User User { get; set; }
	}
}