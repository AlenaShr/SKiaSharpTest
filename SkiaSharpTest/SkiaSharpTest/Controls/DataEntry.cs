using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharpTest.Controls
{
    public class DataEntry
    {
        #region Constructors
        public DataEntry()
        { 
        }

        public DataEntry(float value, float capacity)
        {
            Value = value;
            Capacity = capacity;
        }
        #endregion

        #region Properties

        public float Value { get; set; }

        public float Capacity { get; set; }

        #endregion
    }
}
