using MaterialsManagementSystem.Model;
using MaterialsManagementSystem.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialsManagementSystem.ViewModel
{
    public class CodeGroupManagmentVM
    {


        private ObservableCollection<MaterialGroup> materialGroups;
        public ObservableCollection<MaterialGroup> MaterialGroups
        {
            get { return materialGroups; }
            set
            {
                materialGroups = value;
                OnPropertyChanged("MaterialGroups");
            }
        }

        //MaterialGroupDB 객체 생성
        MaterialGroupDB materialGroupDB = new MaterialGroupDB();

        public CodeGroupManagmentVM()
        {
            LoadMaterialGroup(); 
        }


        // MaterialGroups를 데이터베이스에서 불러와 저장
        public void LoadMaterialGroup()
        {
            using (var dbHelper = DatabaseHelper.Instance)
            {
                dbHelper.OpenConnection();

                var materialGroupsFromDb = materialGroupDB.GetMaterialGroups(true);

                MaterialGroups = new ObservableCollection<MaterialsManagementSystem.Model.MaterialGroup>(materialGroupsFromDb);
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
