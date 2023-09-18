using System;
using System.ComponentModel;
using MaterialsManagementSystem.Model;
using MaterialsManagementSystem.Service;
using System.Windows.Input;

namespace MaterialsManagementSystem.ViewModel
{
    public class MaterialEditVM : INotifyPropertyChanged
    {
        private Material selectedMaterial;
        private ICommand saveCommand;
        private bool isEditing;

        public Material SelectedMaterial
        {
            get { return selectedMaterial; }
            set
            {
                selectedMaterial = value;
                OnPropertyChanged("SelectedMaterial");
            }
        }

        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                isEditing = value;
                OnPropertyChanged("IsEditing");
            }
        }

        public MaterialEditVM(Material material)
        {
            SelectedMaterial = material;
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(SaveMaterial, CanSaveMaterial);
                }
                return saveCommand;
            }
        }

        private bool CanSaveMaterial(object parameter)
        {
            // 저장 가능한지 여부를 반환
            return IsEditing; // 편집 중인 경우에만 저장 가능
        }

        private void SaveMaterial(object parameter)
        {
            // 변경된 데이터를 데이터베이스에 저장하는 로직 추가
            using (var dbHelper = DatabaseHelper.Instance)
            {
                dbHelper.OpenConnection();

                // SelectedMaterial을 사용하여 데이터베이스 업데이트 수행

                IsEditing = false; // 편집 모드 종료
            }
        }

        // INotifyPropertyChanged 구현 코드 (속성 변경 알림을 위해 필요)
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
