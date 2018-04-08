using Newtonsoft.Json;

namespace Action.VewModels
{
	public class ImageRequest
	{
		[JsonProperty("base64Image")]
		public string Base64Image { get; set; }
		[JsonProperty("imageName")]
		public string ImageName { get; set; }
	}
}
