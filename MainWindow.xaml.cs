using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts.Events;
using WpfLiveChartsTestApp.ViewModel;

namespace WpfLiveChartsTestApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑 
    /// </summary>
    public partial class MainWindow : IDisposable
    {
        public MainWindow()

        {
            InitializeComponent();
            //DataContext = new GearedViewModel();
        }
        //调节X轴缩放比例
        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            var vm = (GearedViewModel)DataContext;

            var currentRange = eventargs.Range;

            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("t");    
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                
                vm.Formatter = x => new DateTime((long)x).ToString("yyyy/M/dd");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                vm.Formatter = x => new DateTime((long)x).ToString("yyyy/M");
                return;
            }
            else
            {
                vm.Formatter = x => new DateTime((long)x).ToString("yyyy");
                return;
            }

        }


        public void Dispose()
        {
            var vm = (GearedViewModel)DataContext;
            vm.Values.Dispose();
        }
    }
}
