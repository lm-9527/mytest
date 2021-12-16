using System;
using System.Collections.Generic;
using System.ComponentModel;
using LiveCharts.Defaults;
using LiveCharts.Geared;

namespace WpfLiveChartsTestApp.ViewModel
{
    public class GearedViewModel : INotifyPropertyChanged
    {
        private Func<double, string> _formatter;
        private double _from;
        private double _to;

        public GearedViewModel()
        {
            var now = DateTime.Now;
            var trend = 0d;
            var l = new List<DateTimePoint>();
            var r = new Random();

            for (var i = 0; i < 50000; i++)
            {
                now = now.AddHours(1);
                l.Add(new DateTimePoint(now.AddMinutes(i), trend));
                trend = r.NextDouble() * 10;
                
                //if (r.NextDouble() > 0.4)
                //{   
                //    trend += r.NextDouble() * 10;
                //}
                //else
                //{
                //    trend -= r.NextDouble() * 10;
                //}
            }

            Formatter = x => new DateTime((long)x).ToString("yyyy/M/dd");

            Values = l.AsGearedValues().WithQuality(Quality.High);

            From = DateTime.Now.AddDays(1).Ticks;
            To = DateTime.Now.AddDays(15).Ticks;
        }

        public object Mapper { get; set; }
        public GearedValues<DateTimePoint> Values { get; set; }
        public double From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged("From");
            }
        }
        public double To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged("To");
            }
        }

        public Func<double, string> Formatter
        {
            get { return _formatter; }
            set
            {
                _formatter = value;
                OnPropertyChanged("Formatter");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

