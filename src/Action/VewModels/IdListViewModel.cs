using Newtonsoft.Json;

namespace Action.VewModels
{
    public class IdListViewModel<T>
    {
        [JsonProperty("id")]
        public T Id { get; set; }
    }
}