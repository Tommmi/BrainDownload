using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Settings;

namespace DownloadToBrain.common
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
			CrossSettings.Current.AddOrUpdateValue(key: key, value: text);
			//Application.Current.Properties[key] = text;
			//_ = Application.Current.SavePropertiesAsync();
		}

		public async Task<T> TryGet<T>(string key) where T: class
		{
			string value = CrossSettings.Current.GetValueOrDefault(key: key, defaultValue: (string)null);

			if (value==null)
			{
				return null;
			}

			var textReader = new StringReader(value);
			var jsonReader = new JsonTextReader(textReader);
			var result = _serializer.Deserialize<T>(jsonReader);
			return result;
		}
	}
}
