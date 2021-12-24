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
        Excel.Application app;
        Excel.Workbook workbook;
        Excel.Worksheet worksheet;
        Excel.Range range;

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

            this.app = new Excel.Application();
            // 새 워크북 생성
            //this.workbook = app.Workbooks.Add();

            // 기존 워크북 열기
            this.workbook = app.Workbooks.Open(@"C:\Users\W24880\Documents\test.xlsx");
            this.worksheet = workbook.Worksheets.Item["sheet1"] as Excel.Worksheet;

            //this.worksheet = (Excel.Worksheet)workbook.Worksheets.Add(); // 명시적으로 형변환을 안하면 오류가 난다고..?
        }

        private void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            this.app.Visible = true;

            // 선택한 셀에 대해 값 쓰기
            range = worksheet.Cells[1,1] as Excel.Range;
            range.Value = 1;

            // 범위로 지정한 열에 대해 같은 값을 한번에 쓰기 (2,2,2 가 써짐)
            range = worksheet.Range["A2:C2"] as Excel.Range;
            range.Value = 2;

            // 그리드에 있는 값을 엑셀에 적기
            int row = 3;
            foreach (RowData item in dataGrid.Items.OfType<RowData>())
            {
                string text = item.text;
                bool isChecked = item.isChecked;
                string status = item.status.ToString();

                Excel.Range range1 = worksheet.Cells[row, 1] as Excel.Range;
                Excel.Range range2 = worksheet.Cells[row, 2] as Excel.Range;
                Excel.Range range3 = worksheet.Cells[row, 3] as Excel.Range;
                range1.Value = text;
                range2.Value = isChecked;
                range3.Value = status;
                row ++;
            }

            // 추가한 데이터를 저장한다. 닫을 때 저장하시겠습니까? 안뜸! (와우..)
            workbook.Save();
            this.Close();
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