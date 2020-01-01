using System.Collections.Generic;

namespace Net.FreeORM.Extensions
{
    /// <summary>
    /// Description of FilterParameter.
    /// </summary>
    public class FilterParameter
    {
        private string _Name;
        private List<object> _Values = new List<object>();

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public List<object> Values
        {
            get { return _Values; }
            set { _Values = value; }
        }


    }
}
