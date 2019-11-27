using CHBackOffice.ApiServices.ChsProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SkiaSharpTest
{
    public class MainPageViewModel: INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            this.MachineStatus = GetStatus();
            this.MachineInfo = GetInfo();
            this.MachineChart = GetChart();
            //this.CashDispenser = GetChartData()[0];
            //this.CoinHopper = GetChartData()[1];
            //this.BillAcceptor = GetChartData()[2];
        }

        #region Properties

        #region CashDispenser
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
        #endregion
        #region CoinHopper
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
        #endregion
        #region BillAcceptor
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

        private Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> _machineStatus;
        public Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> MachineStatus
        {
            get { return _machineStatus; }
            set 
            {
                _machineStatus = value;
                OnPropertyChanged(nameof(MachineStatus));
            }
        }

        private Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> _machineInfo;
        public Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> MachineInfo
        {
            get { return _machineInfo; }
            set
            {
                _machineInfo = value;
                OnPropertyChanged(nameof(MachineInfo));
            }
        }

        private Tuple<float, float, Func<float, float, Color>> _machineChart;
        public Tuple<float, float, Func<float, float, Color>> MachineChart
        {
            get { return _machineChart; }
            set 
            {
                _machineChart = value;
                OnPropertyChanged(nameof(MachineChart));
            }
        }

        #endregion

        #region Methods
        private Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> GetStatus()
        {
            return new Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>(
                new List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>
            {
                new Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>
                (
                    new List<Tuple<float, float, Func<float, float, Color>>>
                    {
                        new Tuple<float, float, Func<float, float,Color>>(0, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1940, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1980, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1905, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1999, 2250, CashCoinColorFunc),
                    },
                    "CASH DIS."
                ),
                new Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>
                (
                    new List<Tuple<float, float, Func<float, float, Color>>>
                    {
                        new Tuple<float, float, Func<float, float,Color>>(2, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1800, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1979, 2250, CashCoinColorFunc),
                    },
                    "COINS DIS."
                ),
                new Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>
                (
                    new List<Tuple<float, float, Func<float, float, Color>>>
                    {
                        new Tuple<float, float, Func<float, float,Color>>(2, 990, BillColorFunc),
                    },
                    "BILL VAL."
                )
            },
                KioskState.Offline,
                KioskStatus.Normal,
                "APPLE");
        }

        private Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string> GetInfo()
        {
            return new Tuple<List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>, KioskState, KioskStatus, string>(
                new List<Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>>
            {
                new Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>
                (
                    new List<Tuple<float, float, Func<float, float, Color>>>
                    {
                        new Tuple<float, float, Func<float, float,Color>>(0, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1940, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1980, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1905, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1999, 2250, CashCoinColorFunc),
                    },
                    "CASH DIS."
                ),
                new Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>
                (
                    new List<Tuple<float, float, Func<float, float, Color>>>
                    {
                        new Tuple<float, float, Func<float, float,Color>>(2, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1800, 2250, CashCoinColorFunc),
                        new Tuple<float, float, Func<float, float,Color>>(1979, 2250, CashCoinColorFunc),
                    },
                    "COINS DIS."
                ),
                new Tuple<List<Tuple<float, float, Func<float, float, Color>>>, string>
                (
                    new List<Tuple<float, float, Func<float, float, Color>>>
                    {
                        new Tuple<float, float, Func<float, float,Color>>(2, 990, BillColorFunc),
                    },
                    "BILL VAL."
                )
            },
                KioskState.Offline,
                KioskStatus.Normal,
                string.Empty);
        }

        private Tuple<float, float, Func<float, float, Color>> GetChart()
        {
            return new Tuple<float, float, Func<float, float, Color>>(1200, 2250, ChartColorFunc);
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


        private Func<float, float, Color> CashCoinColorFunc = (count, capacity) =>
        {
            if (count >= 100)
            {
                return Color.FromHex("#65a77b");
            }
            else if (count > 0 && count < 100)
            {
                return Color.FromHex("#F7E076");
            }
            else if (count == 0)
            {
                return Color.FromHex("#EA6964");
            }
            return Color.LightGray;
        };

        private Func<float, float, Color> BillColorFunc = (count, capacity) =>
        {
            if (count < (capacity - 100))
            {
                return Color.FromHex("#65a77b");
            }
            else if (count >= (capacity - 100))
            {
                return Color.FromHex("#F7E076");
            }
            else if (count == capacity)
            {
                return Color.FromHex("#EA6964");
            }
            else if (count == 0)
            {
                return Color.FromHex("#F1F0F3");
            }
            return Color.LightGray;
        };

        private Func<float, float, Color> ChartColorFunc = (count, capacity) => { return Color.FromHex("#65a77b"); };

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
