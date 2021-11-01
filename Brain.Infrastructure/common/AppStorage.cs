using System.IO;
using System.Threading.Tasks;

namespace Brain.Infrastructure.common
{
	public class AppStorage
	{
		private JsonSerializer _serializer = new JsonSerializer();

		public async Task Save<T>(string key, T data)
		{
			var textWriter = new StringWriter();
			_serializer.Serialize(textWriter,data);
			textWriter.Flush();
			string text = textWriter.ToString();
			Application.Current.Properties[key] = text;
			await Application.Current.SavePropertiesAsync();
		}

		public async Task<T> TryGet<T>(string key) where T: class
		{
			if (!Application.Current.Properties.TryGetValue(key, out var text))
			{
				return null;
			}

			var textReader = new StringReader((string)text);
			var jsonReader = new JsonTextReader(textReader);
			var result = _serializer.Deserialize<T>(jsonReader);
			return result;
		}
	}
}
