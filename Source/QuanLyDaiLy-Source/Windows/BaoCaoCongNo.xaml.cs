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
using DAODLL;
using QuanLyDaiLy_Source.Commons.BusinessLogic;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace QuanLyDaiLy_Source.Windows
{
    /// <summary>
    /// Interaction logic for BaoCaoCongNo.xaml
    /// </summary>
    public partial class BaoCaoCongNo : Page
    {

        /// <summary>
        /// Series Container of the chart.
        /// </summary>
        public SeriesCollection SeriesCollection { get; set; }

        /// <summary>
        /// Label of the chart.
        /// </summary>
        public string[] Labels { get; set; }

        /// <summary>
        /// Y Axis Formatter. Convert a double variable to a string variable in order to display it on the chart.
        /// </summary>
        public Func<double, string> YFormatter { get; set; }

        BaoCaoCongNoManager baoCaoCongNoManager = new BaoCaoCongNoManager();

        /// <summary>
        /// A collection used to bind into this page's datagrid.
        /// </summary>
        private ObservableCollection<BaoCaoCongNoItem> myDataItems = new ObservableCollection<BaoCaoCongNoItem>();
        public BaoCaoCongNo()
        {
            InitializeComponent();
            Loaded += BaoCaoCongNo_Loaded;

            //this.PreviewKeyDown += BaoCaoCongNo_PreviewKeyDown;

            SalesDataGrid.ItemsSource = myDataItems;

            GetBaoCaoButton.Click += SearchQueryChanged;

            // Line Chart settings
            SeriesCollection = new SeriesCollection();
            Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7",
                "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
            YFormatter = value => value.ToString();
            DataContext = this;
        }

        /*
        private void BaoCaoCongNo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Focus();
            }

            if (e.Key == Key.Up)
            {
                if (MyScrollViewer.ScrollableHeight > 0 && MyScrollViewer.VerticalOffset > 0)
                {
                    MyScrollViewer.ScrollToHorizontalOffset(MyScrollViewer.HorizontalOffset - 5);
                }

            }
        }
        */

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

        /// <summary>
        /// Initialize a DateTime variable from MonthComboBox.
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime GetDateTime(out DateTime time)
        {
            ComboBoxItem selectedMonth = (ComboBoxItem)MonthComboBox.SelectedItem;
            switch (selectedMonth.Content.ToString())
            {
                case "Tháng Một":
                    {
                        time = new DateTime(DateTime.Now.Year, 1, 1);
                        break;
                    }
                case "Tháng Hai":
                    {
                        time = new DateTime(DateTime.Now.Year, 2, 1);
                        break;
                    }
                case "Tháng Ba":
                    {
                        time = new DateTime(DateTime.Now.Year, 3, 1);
                        break;
                    }
                case "Tháng Bốn":
                    {
                        time = new DateTime(DateTime.Now.Year, 4, 1);
                        break;
                    }
                case "Tháng Năm":
                    {
                        time = new DateTime(DateTime.Now.Year, 5, 1);
                        break;
                    }
                case "Tháng Sáu":
                    {
                        time = new DateTime(DateTime.Now.Year, 6, 1);
                        break;
                    }
                case "Tháng Bảy":
                    {
                        time = new DateTime(DateTime.Now.Year, 7, 1);
                        break;
                    }
                case "Tháng Tám":
                    {
                        time = new DateTime(DateTime.Now.Year, 8, 1);
                        break;
                    }
                case "Tháng Chín":
                    {
                        time = new DateTime(DateTime.Now.Year, 9, 1);
                        break;
                    }
                case "Tháng Mười":
                    {
                        time = new DateTime(DateTime.Now.Year, 10, 1);
                        break;
                    }
                case "Tháng Mười Một":
                    {
                        time = new DateTime(DateTime.Now.Year, 11, 1);
                        break;
                    }
                case "Tháng Mười Hai":
                    {
                        time = new DateTime(DateTime.Now.Year, 12, 1);
                        break;
                    }
                default:
                    {
                        time = new DateTime();
                        break;
                    }
            }
            return time;
        }

        /// <summary>
        /// This method is invoked when user clicks LapBaoCao button.
        /// Check fir required input fields and get BaoCao.
        /// </summary>
        private void SearchQueryChanged(object sender, RoutedEventArgs e)
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

                    if (listDaiLy.Any())
                    {
                        myDataItems.Clear();
                        foreach (DAILY daiLy in listDaiLy)
                        {
                            decimal _NoDau = baoCaoCongNoManager.GetSoNoDauKy(selectedDate, daiLy.MADL);
                            decimal _NoCuoi = baoCaoCongNoManager.GetSoNoPhatSinh(selectedDate, daiLy.MADL);
                            BaoCaoCongNoItem item = new BaoCaoCongNoItem()
                            {
                                TenDaiLy = daiLy.TENDL,
                                NoDau = _NoDau,
                                NoPhatSinh = _NoCuoi,
                                NoCuoi = Math.Abs(_NoDau + _NoCuoi),
                            };
                            myDataItems.Add(item);

                        }
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
        /// Invoke changes within the page loaded event.
        /// </summary>
        public static event EventHandler pageLoaded;
        private void BaoCaoCongNo_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.Properties[Models.DefaultSettings.ContentFrameTitle] = "Báo Cáo Công Nợ";
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

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Mouse.OverrideCursor = Cursors.Wait;
            });

            SeriesCollection.Clear();
            
            ObservableCollection<DAILY> listDaiLy = new ObservableCollection<DAILY>();
            int selectedMaLoaiDaiLy = (int)TypeComboBox.SelectedValue;
            int selectedMaQuan = (int)DistrictComboBox.SelectedValue;

            if (selectedMaLoaiDaiLy == 0 && selectedMaQuan == 0)
                listDaiLy = ViewManager.Instance.GetAllDaiLy();
            else if (selectedMaLoaiDaiLy != 0 && selectedMaQuan == 0)
                listDaiLy = ViewManager.Instance.GetAllDaiLyByMaLoai(selectedMaLoaiDaiLy);
            else if (selectedMaLoaiDaiLy == 0 && selectedMaQuan != 0)
                listDaiLy = ViewManager.Instance.GetAllDaiLy(selectedMaQuan);
            else if (selectedMaLoaiDaiLy != 0 && selectedMaQuan != 0)
                listDaiLy = ViewManager.Instance.GetAllDaiLy(selectedMaQuan, selectedMaLoaiDaiLy);


            DateTime time = new DateTime(DateTime.Now.Year, 1, 1);

            foreach (DAILY daiLy in listDaiLy)
            {
                ChartValues<ObservableValue> listSoNoDau = new ChartValues<ObservableValue>();
                for (int i = 0; i <= 11; i++)
                {
                    double _NoDau = (double)baoCaoCongNoManager.GetSoNoDauKy(time, daiLy.MADL);
                    listSoNoDau.Add(new ObservableValue(_NoDau));
                    time = time.AddMonths(1);
                }

                SeriesCollection.Add(new LineSeries()
                {
                    Title = daiLy.TENDL,
                    Values = listSoNoDau,
                    LineSmoothness = 0,
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 5
                });
                
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                Mouse.OverrideCursor = null;
            });
        }
    }

    /// <summary>
    /// Item template for Datagrid of BaoCaoCongNo
    /// </summary>
    public class BaoCaoCongNoItem
    {
        public string TenDaiLy { get; set; }
        public decimal NoDau { get; set; }
        public decimal NoPhatSinh { get; set; }
        public decimal NoCuoi { get; set; }
    }



}
