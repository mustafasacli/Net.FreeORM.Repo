using System;
using System.Collections.Generic;

namespace Net.FreeORM.Math.Differentiation
{
    public class Interpolation : IDictionary<double, double>
    {
        List<double> _keys = new List<double>();
        List<double> _values = new List<double>();

        public void Add(double key, double value)
        {
            if ((_keys.IndexOf(key) == -1) && (_values.IndexOf(value) == -1))
            {
                _keys.Add(key);
                _values.Add(value);
            }
            else
                throw new Exception("Key or Value is already contained.");
        }

        public bool ContainsKey(double key)
        {
            return _keys.IndexOf(key) != -1;
        }

        public ICollection<double> Keys
        {
            get { return _keys; }
        }

        public bool Remove(double key)
        {
            int index = _keys.IndexOf(key);
            if (index != -1)
            {
                _keys.RemoveAt(index);
                _values.RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        public bool TryGetValue(double key, out double value)
        {
            int index = _keys.IndexOf(key);
            if (index != -1)
            {
                value = _values[index];
                return true;
            }
            else
            {
                value = 0.0d;
                return false;
            }
        }

        public ICollection<double> Values
        {
            get { return _values; }
        }

        public double this[double key]
        {
            get
            {
                int index = _keys.IndexOf(key);
                if (index != -1)
                {
                    return _values[index];
                }
                else
                    throw new Exception("Key is not contained.");
            }
            set
            {
                int index = _keys.IndexOf(key);
                if (index != -1)
                {
                    _values[index] = value;
                }
                else
                    throw new Exception("Key is not contained.");
            }
        }

        public void Add(KeyValuePair<double, double> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _keys.Clear();
            _values.Clear();
        }

        public bool Contains(KeyValuePair<double, double> item)
        {
            return ((_keys.Contains(item.Key)) && (_values.Contains(item.Value)));
        }

        public void CopyTo(KeyValuePair<double, double>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < array.Length; i++)
            {
                Add(array[i]);
            }
        }

        public int Count
        {
            get { return _keys.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<double, double> item)
        {
            if ((_keys.IndexOf(item.Key) == -1) && (_values.IndexOf(item.Value) == -1))
            {
                _keys.Remove(item.Key);
                _values.Remove(item.Value);
                return true;
            }
            else
                return false;
        }

        public bool RemoveKey(double key)
        {
            int index = _keys.IndexOf(key);
            if (index != -1)
            {
                _keys.RemoveAt(index);
                _values.RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        public IEnumerator<KeyValuePair<double, double>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public double GetSubstractWithSelectedValue(double value)
        {
            double v = 1.0d;
            foreach (double d in _values)
            {
                if (d == value)
                    continue;
                else
                {
                    v *= (d - value);
                }
            }
            return v;
        }

        public double[] GetKeyArrayWithoutSelectedValue(double key)
        {
            if (_keys.Count == 0)
                return new double[] { 0.0d };
            else
            {
                double[] d_array = new double[] { 1.0d };
                foreach (double numb in _keys)
                {
                    if (numb == key)
                        continue;
                    else
                    {
                        d_array = DiffUtil.MultiplyTwoOneDimensionArray(d_array, new double[] { -1.0d * numb, 1.0d });
                    }
                }
                return d_array;
            }
        }
    }
}
