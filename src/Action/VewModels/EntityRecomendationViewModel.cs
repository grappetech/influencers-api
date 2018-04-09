using Newtonsoft.Json;

namespace Action.VewModels
{
	public class EntityRecomendationViewModel : EntityViewModel
	{
		[JsonProperty("score")]
		public decimal Score { get; set; }
	}
}