using Newtonsoft.Json;

namespace Action.VewModels
{
	public class ToneItem
	{
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("name")]
		public string Name { get; set; }
		[JsonProperty("score")]
		public double Score { get; set; }
		[JsonProperty("toneType")]
		public EToneType ToneType
		{
			get
			{
				switch (Id.ToLower())
				{
					case "excited":
					case "satisfied":
					case "polite":
						return EToneType.Positivo;
					case "sad":
					case "frustrated":
					case "impolite":
					case "sympathetic":
						return EToneType.Negativo;
					default:
						return EToneType.Neutro;
				}
			}
		}
	}
}