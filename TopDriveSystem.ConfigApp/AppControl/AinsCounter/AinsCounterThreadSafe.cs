namespace TopDriveSystem.ConfigApp.AppControl.AinsCounter
{
    internal class AinsCounterThreadSafe : IAinsCounterRaisable
    {
        private readonly object _ainsCountSync;
        private int _ainsCount;

        public AinsCounterThreadSafe(int ainsCount)
        {
            _ainsCountSync = new object();
            _ainsCount = ainsCount;
        }

        public event AinsCountInSystemHasBeenChangedDelegate AinsCountInSystemHasBeenChanged;

        public int SelectedAinsCount
        {
            get
            {
                lock (_ainsCountSync)
                {
                    return _ainsCount;
                }
            }
        }

        public void SetAinsCountAndRaiseChange(int ainsCount)
        {
            lock (_ainsCountSync)
            {
                _ainsCount = ainsCount;
                var eve = AinsCountInSystemHasBeenChanged; // TODO: thing if I need lock
                eve?.Invoke(ainsCount);
            }
        }
    }
}