using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SkiaSharpTest.Controls
{
    public class DataEntryCollection : Collection<DataEntry>
    {
        public new void Add(DataEntry dataEntry)
        {
            base.Add(dataEntry);
        }

        public new void Remove(DataEntry dataEntry)
        {
            base.Remove(dataEntry);
        }

        public void Remove(int index)
        {
            if (index > Count - 1 || index < 0)
            {
                throw new IndexOutOfRangeException("index is invalid!");
            }
            else
            {
                base.RemoveAt(index);
            }
        }

        public new DataEntry this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
            }
        }
    }
}
