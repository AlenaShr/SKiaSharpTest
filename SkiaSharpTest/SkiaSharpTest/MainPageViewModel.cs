using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SkiaSharpTest
{
    public class MainPageViewModel: INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            this.MachineChart = GetChart();
            //this.CashDispenser = GetChartData()[0];
            //this.CoinHopper = GetChartData()[1];
            //this.BillAcceptor = GetChartData()[2];
        }

        #region Properties
        private List<Tuple<List<Tuple<float, float>>, string>> _machineChart;
        public List<Tuple<List<Tuple<float, float>>, string>> MachineChart
        {
            get { return _machineChart; }
            set 
            {
                _machineChart = value;
                OnPropertyChanged(nameof(MachineChart));
            }
        }


        private List<Tuple<float, float>> _cashDispenser;
        public List<Tuple<float, float>> CashDispenser
        {
            get { return _cashDispenser; }
            set 
            {
                _cashDispenser = value;
                OnPropertyChanged(nameof(CashDispenser));
            }
        }

        private List<Tuple<float, float>> _coinHopper;
        public List<Tuple<float, float>> CoinHopper
        {
            get { return _coinHopper; }
            set
            {
                _coinHopper = value;
                OnPropertyChanged(nameof(CoinHopper));
            }
        }

        private List<Tuple<float, float>> _billAcceptor;
        public List<Tuple<float, float>> BillAcceptor
        {
            get { return _billAcceptor; }
            set
            {
                _billAcceptor = value;
                OnPropertyChanged(nameof(BillAcceptor));
            }
        }

        #endregion

        #region Methods
        private List<Tuple<List<Tuple<float, float>>, string>> GetChart()
        {
            return new List<Tuple<List<Tuple<float, float>>, string>>
            {
                new Tuple<List<Tuple<float, float>>, string>
                (
                    new List<Tuple<float, float>>
                    {
                        new Tuple<float, float>(0,2250),
                        new Tuple<float, float>(1940,2250),
                        new Tuple<float, float>(1980,2250),
                        new Tuple<float, float>(1905,2250),
                        new Tuple<float, float>(1999,2250),
                    },
                    "CASH DIS."
                ),
                new Tuple<List<Tuple<float, float>>, string>
                (
                    new List<Tuple<float, float>>
                    {
                        new Tuple<float, float>(2,2250),
                        new Tuple<float, float>(1800,2250),
                        new Tuple<float, float>(1979,2250),
                    },
                    "COINS DIS."
                ),
                new Tuple<List<Tuple<float, float>>, string>
                (
                    new List<Tuple<float, float>>
                    {
                        new Tuple<float, float>(2,990),
                    },
                    "BILL VAL."
                )
            };
        }




        private List<List<Tuple<float, float>>> GetChartData()
        {
            return new List<List<Tuple<float, float>>> 
            {
                new List<Tuple<float, float>>
                {
                    new Tuple<float, float>(0,2250),
                    new Tuple<float, float>(1940,2250),
                    new Tuple<float, float>(1980,2250),
                    new Tuple<float, float>(1905,2250),
                    new Tuple<float, float>(1999,2250)
                },
                new List<Tuple<float, float>>
                {
                    new Tuple<float, float>(1,2250),
                    new Tuple<float, float>(1800,2250),
                    new Tuple<float, float>(1979,2250),
                },
                new List<Tuple<float, float>>
                {
                    new Tuple<float, float>(2,990)
                },
            };
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
