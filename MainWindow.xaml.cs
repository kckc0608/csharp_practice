using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

using Excel = Microsoft.Office.Interop.Excel;

namespace GridPractice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<RowData> rowdataList = new ObservableCollection<RowData>();
            /*{
                new RowData(),
                new RowData()
            };*/

            rowdataList.Add(new RowData());
            rowdataList.Add(new RowData("test2", true, Status.계정));
            rowdataList.Add(new RowData("test3", false, Status.프린터));

            //dataGrid.DataContext = rowdataList; // 이걸로 해도 되더라 단, xaml에서 ItemsSource = "{Binding}" 을 해주어야 함. 무슨 차이인지 모르겠음.
            dataGrid.ItemsSource = rowdataList; // 이걸 할때는 단독으로 이거만 쓰면 됨.
        }
    }

    public enum Status {전문처리, 계정, 프린터};

    public class RowData
    {
        public string text {get; set;}
        public bool isChecked {get; set;}
        //public Uri email;
        public Status status {get; set;} // Status enum 의 정의는 외부 클래스에서 정의함. 여기는 값을 담는 곳이지, 값을 정의하는 곳이 아니기 때문.

        public RowData() 
        {
            this.text = "test";
            this.isChecked = true;
            this.status = Status.전문처리;
        }

        public RowData(string text, bool isChecked, Status status) 
        {
            this.text = text;
            this.isChecked = isChecked;
            this.status = status;
        }
    }
}