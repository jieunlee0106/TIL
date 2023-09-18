using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using MaterialsManagementSystem.Model;
using MaterialsManagementSystem.Service;
using MaterialsManagementSystem.View;
using MaterialsManagementSystem.ViewModel;

namespace MaterialsManagementSystem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MaterialVM(); // ViewModel 인스턴스를 DataContext로 설정

        }
        //materialDB 객체 생성
        MaterialDB materialDB = new MaterialDB();

        //MaterialGroupDB 객체 생성
        MaterialGroupDB materialGroupDB = new MaterialGroupDB();

        //MaterialVM 객체 생성
        MaterialVM materialVM = new MaterialVM();

        //codeGroupManagmentVM 객체 생성
        CodeGroupManagmentVM codeGroupManagmentVM = new CodeGroupManagmentVM();

        #region "Button Click"
        // 자재 관리 페이지 [검색] 버튼 클릭
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // 검색 조건 - SP 로 요청보낼 파라미터 값 
            string materialCodeFilter = MaterialCodeFilter.Text;
            string materialNameFilter = MaterialNameFilter.Text;
            string materialGroupFilter = ((MaterialGroup)MaterialGroupFilter.SelectedItem)?.CodeName;
            string useFlagFilter = UseFlagFilter.Text;



            // GetFilteredMaterials 메서드 호출
            List<Material> filteredMaterials = materialDB.GetFilteredMaterials(materialCodeFilter, materialNameFilter, materialGroupFilter, useFlagFilter);

            // No. 열 번호를 다시 할당
            for (int i = 0; i < filteredMaterials.Count; i++)
            {
                filteredMaterials[i].Number = i + 1;
                filteredMaterials[i].Status = "Nomal";
            }

            MaterialGrid.ItemsSource = filteredMaterials;

            // 자재 그룹 정보 불러오기
            codeGroupManagmentVM.LoadMaterialGroup();

        }

        // 자재 관리 페이지 [삭제] 버튼 클릭
        // desription: DB 변동 없음
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // MaterialGrid에서 선택된 항목 가져오기
            Material selectedMaterial = (Material)MaterialGrid.SelectedItem;

            if (selectedMaterial != null)
            {
                // 삭제 확인 대화 상자 표시
                MessageBoxResult result = MessageBox.Show("선택한 항목을 삭제하시겠습니까?", "삭제 확인", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // '상태' 열 값을 'DELETE'로 변경
                    selectedMaterial.Status = "Delete";

                    // ViewModel의 Materials 컬렉션을 갱신
                    MaterialVM viewModel = (MaterialVM)this.DataContext;
                    viewModel.Materials = new ObservableCollection<Material>(viewModel.Materials);

                }
            }
            else
            {
                MessageBox.Show("삭제할 항목을 선택하세요.", "선택 필요", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // 자재 관리 페이지 [추가] 버튼 클릭 => 팝업 창 생성
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            AddMaterialWindow addMaterialWindow = new AddMaterialWindow();
            addMaterialWindow.Owner = this; // 부모 창 설정

            // 팝업 창을 모달 다이얼로그로 표시합니다.
            bool? result = addMaterialWindow.ShowDialog();

       
            if (result == true)
            {
 
            }
        }

        // 자재 관리 페이지 [초기화] 버튼 클릭
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // 검색 필터 컨트롤들을 초기화
            MaterialCodeFilter.Text = "";
            MaterialNameFilter.Text = "";
            MaterialGroupFilter.SelectedIndex = 0; // 첫 번째 아이템을 선택
            UseFlagFilter.SelectedIndex = 0; // 첫 번째 아이템을 선택

            // 검색 버튼과 같은 동작 수행 => 그리드 초기화
            SearchButton_Click(sender, e);


            // 자재 그룹 정보 불러오기
            codeGroupManagmentVM.LoadMaterialGroup();
        }

        // 자재 관리 페이지 [저장] 버튼 클릭
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            MaterialVM viewModel = (MaterialVM)this.DataContext;
            viewModel.Materials = new ObservableCollection<Material>(viewModel.Materials);


            //materialDB 객체 생성
            MaterialDB materialDB = new MaterialDB();

            foreach (var item in viewModel.Materials)
            {
                {
                    if (item.Status == "Delete")
                    {
                        // '상태' 열이 'Delete'인 경우 해당 행을 DB에서 삭제하는 쿼리를 실행
                        MessageBox.Show(item.MaterialCode);

                        materialDB.DeleteMaterialFromDatabase(item.MaterialCode);
                    }
                    else if (item.Status == "New")
                    {
                        // '상태' 열이 'New'인 경우 해당 행을 DB에 추가하는 쿼리를 실행
                        materialDB.InsertMaterialIntoDatabase(item.MaterialCode, item.MaterialName, item.MaterialGroup, item.UseFlag);
                    }
                }


            }
            MessageBox.Show("수정사항이 반영되었습니다");

            SearchButton_Click(sender, e);

            // 자재 그룹 정보 불러오기
            codeGroupManagmentVM.LoadMaterialGroup();
        }

        // 자재 관리 페이지 [자재 그룹 관리] 버튼 클릭
        private void GroupManagementButton_Click(object sender, RoutedEventArgs e)
        {
            // 팝업 창을 생성하고 초기화합니다.
            GroupManagementWindow groupManagementWindow = new GroupManagementWindow();
            groupManagementWindow.Owner = this; 

            bool? result = groupManagementWindow.ShowDialog();


            if (result == true)
            {
                // 자재 그룹 정보 불러오기
                codeGroupManagmentVM.LoadMaterialGroup();
            }
        }
        #endregion

        #region "Grid Edit"
        // 그리드 수정 - ...코드 수정 중
        private void MaterialGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                if (e.Column.Header.ToString() == "자재그룹")
                {

                    ComboBox comboBox = e.EditingElement as ComboBox;

                    if (comboBox != null)
                    {

                        string selectedMaterialGroup = comboBox.SelectedValue as string;


                        Material editedMaterial = e.Row.DataContext as Material;

                        if (editedMaterial != null)
                        {
                            materialVM.UpdateMaterialStatusToUpdate(editedMaterial);

                        }
                    }
                }
                else if (e.Column.Header.ToString() == "사용여부")
                {

                }
            }
        }
        #endregion
    }

}

