namespace BasicAPI.Services
{
    public class DataContainer
    {
        private IList<int> _numbers;

        public DataContainer()
        {
            _numbers = new List<int>();
        }
        
        public bool TryAdd(int x)
        {
            if (_numbers.Any(y => y == x)) return false;

            _numbers.Add(x);
            return true;
        }

        public void Remove(int x)
        {
            _numbers.Remove(x);
        }

        public bool Update(int oldValue, int newValue, out bool duplicated)
        {
            duplicated = _numbers.Any(x => x == oldValue);
            if (duplicated) return false;

            for (int i = 0; i < _numbers.Count; i++)
            {
                if (_numbers[i] == oldValue)
                {
                    _numbers[i] = newValue;
                    return true;
                }                
            }
            return false;
        }

        public IList<int> GetAllNumbers => _numbers;
    }
}
