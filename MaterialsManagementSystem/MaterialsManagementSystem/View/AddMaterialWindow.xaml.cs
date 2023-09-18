using MaterialsManagementSystem.Model;
using MaterialsManagementSystem.ViewModel;
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
using System.Windows.Shapes;

namespace MaterialsManagementSystem.View
{
    /// <summary>
    /// 자재 그룹 관리 모달 창
    /// </summary>
    public partial class AddMaterialWindow : Window
    {
        public AddMaterialWindow()
        {
            InitializeComponent();
            DataContext = new MaterialAddVM();
        }
        #region "Button Click"
        // 자재 그룹 관리 페이지 [추가] 버튼 클릭
        // desription: DB 변동 없음
        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {

            // 입력값 가져오기
            string materialCode = MaterialCodeTextBox.Text;
            string materialName = MaterialNameTextBox.Text;
            string materialGroup = ((MaterialGroup)MaterialGroupFilter.SelectedItem)?.CodeName;
            string useFlag = UseFlagComboBox.Text;

            // 입력값 검증
            if (string.IsNullOrEmpty(materialCode) || string.IsNullOrEmpty(materialName) || string.IsNullOrEmpty(materialGroup) || string.IsNullOrEmpty(useFlag))
            {
                MessageBox.Show("모든 필드를 입력해주세요.", "오류", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // MainWindow로 데이터 전달
            Material material = new Material
            {
                Status = "New",
                MaterialCode = materialCode,
                MaterialName = materialName,
                MaterialGroup = materialGroup, // 수정: 선택된 드롭다운 값으로 설정
                UseFlag = useFlag, // 수정: 선택된 드롭다운 값으로 설정
                CrtDt = DateTime.Now, // 현재 날짜 및 시간 사용
                UdtDt = DateTime.Now  // 현재 날짜 및 시간 사용
            };

            // MaterialVM 클래스의 메서드를 호출하여 데이터를 추가
            MaterialVM viewModel = (MaterialVM)this.Owner.DataContext;
            viewModel.AddMaterial(material);

            // 창 닫기
            this.DialogResult = true;
        }
        #endregion
    }
}
