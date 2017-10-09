namespace Action.Services.Scrap
{
    /// <summary>
    ///     Page class
    /// </summary>
    public class Page
    {
        #region Constructor

        #endregion

        #region Private Instance Fields

        private string _text;

        //private int _viewstateSize;

        #endregion

        #region Public Properties

        public int Size { get; private set; }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                Size = value.Length;
            }
        }

        public string Url { get; set; }

        #endregion
    }
}