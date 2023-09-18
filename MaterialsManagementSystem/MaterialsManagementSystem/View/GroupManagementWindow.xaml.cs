using MaterialsManagementSystem.Model;
using MaterialsManagementSystem.Service;
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

    public partial class GroupManagementWindow : Window
    {
        public GroupManagementWindow()
        {
            InitializeComponent();
            DataContext = new CodeGroupManagmentVM();

        }
        // DB 연결 인스턴스 
        DatabaseHelper dbHelper = DatabaseHelper.Instance;


        //MaterialGroupDB 객체 생성
        MaterialGroupDB materialGroupDB = new MaterialGroupDB();

        // 자재 그룹 관리 페이지 [자재 그룹 조회] 버튼 클릭
        private void LoadGroups_Click(object sender, RoutedEventArgs e)
        {
            
            var groups = materialGroupDB.GetMaterialGroups(false);

            // 데이터를 그리드에 바인딩
            GroupGrid.ItemsSource = groups;
        }

        private void GroupGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

         }

        // 자재 그룹 관리 페이지 [자재 그룹 추가] 버튼 클릭
        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            // 자재 그룹 코드와 자재 그룹 이름을 텍스트 박스에서 가져오기
            string codeId = MaterialCodeTextBox.Text;
            string codeName = MaterialNameTextBox.Text;

            // 입력값이 비어있는지 확인
            if (string.IsNullOrWhiteSpace(codeId) || string.IsNullOrWhiteSpace(codeName))
            {
                MessageBox.Show("자재 그룹 코드와 자재 그룹 이름을 입력하세요.");
                return;
            }

            // DatabaseHelper 객체 생성
            var dbHelper = DatabaseHelper.Instance;

            try
            {

                materialGroupDB.AddMaterialGroupToDatabase(codeId, codeName);
                MessageBox.Show("자재 그룹이 추가되었습니다.");

                // 그리드 다시 로드
                var groups = materialGroupDB.GetMaterialGroups(false);

                // 입력 텍스트 박스 초기화
                MaterialCodeTextBox.Clear();
                MaterialNameTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("자재 그룹 추가 중 오류가 발생했습니다: " + ex.Message);
            }

            finally
            {
                // 자재 그룹 정보 불러오기
                MaterialVM materialVM = new MaterialVM();
                materialVM.LoadMaterialGroup();
            }
        }

        // 자재 그룹 관리 페이지 [자재 그룹 삭제] 버튼 클릭
        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            // 데이터 그리드에서 선택된 행 가져오기
            MaterialGroup selectedGroup = GroupGrid.SelectedItem as MaterialGroup;

            if (selectedGroup == null)
            {
                MessageBox.Show("삭제할 자재 그룹을 선택하세요.");
                return;
            }

            // 선택된 자재 그룹의 CODE_ID 가져오기
            string codeIdToDelete = selectedGroup.CodeId;

            // dbHelper를 사용하여 자재 그룹 삭제
            materialGroupDB.DeleteMaterialGroupFromDatabase(codeIdToDelete);

            // 데이터를 그리드에 재바인딩
            var groups = materialGroupDB.GetMaterialGroups(false);
            GroupGrid.ItemsSource = groups;

        }



        private void CodeIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
