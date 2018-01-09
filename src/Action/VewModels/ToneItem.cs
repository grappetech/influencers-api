namespace Action.VewModels
{
	public class ToneItem
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public double Score { get; set; }

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