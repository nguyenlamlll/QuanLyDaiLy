using DAODLL;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for BaoCaoDoanhThu.xaml
    /// </summary>
    public partial class BaoCaoDoanhThu : Page
    {
        /// <summary>
        /// A collection used to bind into this page's datagrid.
        /// </summary>
        private ObservableCollection<BaoCaoDoanhThuItem> myDataItems = new ObservableCollection<BaoCaoDoanhThuItem>();

        private XuatHangManager xuatHangManager = new XuatHangManager();


        public SeriesCollection ColumnChartSeriesCollection { get; set; }
        public string[] Labels { get; set; }

        public Func<double, string> Formatter { get; set; }

        public BaoCaoDoanhThu()
        {
            InitializeComponent();
            Loaded += BaoCaoDoanhThu_Loaded;
            SalesDataGrid.ItemsSource = myDataItems;

            ColumnChartSeriesCollection = new SeriesCollection();
            //ColumnChart.Visibility = Visibility.Collapsed;
            Formatter = value => value.ToString("N");
            DataContext = this;
        }
        /// <summary>
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;
        private void BaoCaoDoanhThu_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Báo Cáo Doanh Số";
            pageLoaded?.Invoke(this, e);

            ObservableCollection<LOAIDL> LoaiDaiLyList = new ObservableCollection<LOAIDL>();
            LoaiDaiLyList = ViewManager.Instance.GetAllLoaiDL();
            LoaiDaiLyList.Add(new DAODLL.LOAIDL() { MALOAI = 0, TENLOAI = "Tất Cả" });
            LoaiDaiLyList.Move(LoaiDaiLyList.Count - 1, 0);
            TypeComboBox.ItemsSource = LoaiDaiLyList;

            ObservableCollection<QUAN> QuanList = new ObservableCollection<QUAN>();
            QuanList = ViewManager.Instance.GetAllQuan();
            QuanList.Add(new QUAN() { MAQUAN = 0, TENQUAN = "Tất Cả" });
            QuanList.Move(QuanList.Count - 1, 0);
            DistrictComboBox.ItemsSource = QuanList;
        }


        /// <summary>
        /// Validate query options before excuting it.
        /// </summary>
        /// <returns>Return true if the query can be excuted properly.</returns>
        private bool IsQuerySuitable()
        {
            if (TypeComboBox.SelectedIndex == -1) return false;
            if (DistrictComboBox.SelectedIndex == -1) return false;
            if (MonthComboBox.SelectedIndex == -1) return false;
            return true;
        }

        private void GetBaoCaoButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsQuerySuitable())
            {
                try
                {
                    int selectedMaLoaiDaiLy = (int)TypeComboBox.SelectedValue;
                    int selectedMaQuan = (int)DistrictComboBox.SelectedValue;
                    DateTime selectedDate = Helper.Utilities.GetDateTimeFromMonthComboBox(out selectedDate, MonthComboBox);
                    
                    //Get the scope of DaiLy needed to get reports.
                    ObservableCollection<DAILY> listDaiLy = new ObservableCollection<DAILY>();
                    if (selectedMaLoaiDaiLy == 0 && selectedMaQuan == 0)
                        listDaiLy = ViewManager.Instance.GetAllDaiLy();
                    else if (selectedMaLoaiDaiLy != 0 && selectedMaQuan == 0)
                        listDaiLy = ViewManager.Instance.GetAllDaiLyByMaLoai(selectedMaLoaiDaiLy);
                    else if (selectedMaLoaiDaiLy == 0 && selectedMaQuan != 0)
                        listDaiLy = ViewManager.Instance.GetAllDaiLy(selectedMaQuan);
                    else if (selectedMaLoaiDaiLy != 0 && selectedMaQuan != 0)
                        listDaiLy = ViewManager.Instance.GetAllDaiLy(selectedMaQuan, selectedMaLoaiDaiLy);

                    // Update the DataGrid
                    if (listDaiLy.Any())
                    {
                        myDataItems.Clear();
                        decimal SumMoneyOfAllDaiLies = 0;
                        foreach (DAILY daiLy in listDaiLy)
                        {
                            int tongPhieuXuat = xuatHangManager.GetAllPhieuXuatHang(daiLy.MADL).Where(p => p.NGAYLAP.Value.Month == selectedDate.Month && p.NGAYLAP.Value.Year == selectedDate.Year).Count();
                            decimal sum = xuatHangManager.GetAllPhieuXuatHang(daiLy.MADL).Where(p => p.NGAYLAP.Value.Month == selectedDate.Month && p.NGAYLAP.Value.Year == selectedDate.Year).Sum(p => p.SOTIENTRA.Value);
                            SumMoneyOfAllDaiLies += sum;
                            BaoCaoDoanhThuItem item = new BaoCaoDoanhThuItem()
                            {
                                DaiLy = daiLy.TENDL,
                                SoPhieuXuat = tongPhieuXuat,
                                TongTriGia = sum
                            };
                            myDataItems.Add(item);
                        }
                        // Update TyLe for each item in the collection
                        foreach (BaoCaoDoanhThuItem item in myDataItems)
                        {
                            item.TyLe = ((double)item.TongTriGia / (double)SumMoneyOfAllDaiLies) * 100.0;
                        }

                        // Try to draw the charts
                        DrawColumnChart(listDaiLy);
                    }
                    else
                    {
                        MessageBox.Show("Không có đại lý nào trong danh sách", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        myDataItems.Clear();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Could not excute queries");
                }
            }
        }

        /// <summary>
        /// Draw the chart of current Dailies in myDataItems View Models.
        /// </summary>
        /// <param name="listDaiLy">Dailies to be taken in account.</param>
        private void DrawColumnChart(ObservableCollection<DAILY> listDaiLy)
        {
            ColumnChartSeriesCollection.Clear();
            ColumnChart.AxisX.Clear();
            Axis ax = new Axis()
            {
                Title = "Đại Lý",
                FontSize = 14,
                //Separator = new LiveCharts.Wpf.Separator { Step = 1, IsEnabled = false },
                ShowLabels = true,
                Labels = listDaiLy.Select(p => p.TENDL).ToArray(),
            };
            ColumnChart.AxisX.Add(ax);
            ChartValues<ObservableValue> listTongTriGia = new ChartValues<ObservableValue>();
            foreach (BaoCaoDoanhThuItem item in myDataItems)
            {
                listTongTriGia.Add(new ObservableValue((double)item.TongTriGia));

            }
            if (listTongTriGia.Any())
            {
                ColumnChartSeriesCollection.Add(new ColumnSeries
                {
                    Title = DateTime.Now.Year.ToString(),
                    Values = listTongTriGia
                });
            }
        }
    }


    public class BaoCaoDoanhThuItem
    {
        public string DaiLy { get; set; }
        public int SoPhieuXuat { get; set; }
        public decimal TongTriGia { get; set; }
        public double TyLe { get; set; }
    }
}
