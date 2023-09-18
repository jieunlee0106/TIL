using MaterialsManagementSystem.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialsManagementSystem.Model;
using MaterialsManagementSystem.ViewModel;


public class MaterialVM : INotifyPropertyChanged
{
    private ObservableCollection<Material> materials;
    public ObservableCollection<Material> Materials
    {
        get { return materials; }
        set
        {
            materials = value;
            OnPropertyChanged("Materials");
        }
    }

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
    public void UpdateMaterialStatusToUpdate(Material material)
    {
        if (material != null)
        {

            material.Status = "Update";

            // Notify the UI that the Status property has changed for this material
            OnPropertyChanged("Materials");
        }
    }
    //MaterialGroupDB 객체 생성
    MaterialGroupDB materialGroupDB = new MaterialGroupDB();

    public MaterialVM()
    {
        LoadMaterials(); // 데이터베이스에서 Materials 불러와 저장
        LoadMaterialGroup(); //데이터베이스에서 MaterialGroups를  불러와 저장
    }

    // 초기 Materials 로드
    private void LoadMaterials()
    {
        using (var dbHelper = DatabaseHelper.Instance)
        {
            dbHelper.OpenConnection();

            var materialsFromDb = dbHelper.GetMaterials();

            for (int i = 0; i < materialsFromDb.Count; i++)
            {
                materialsFromDb[i].Number = i + 1;
                materialsFromDb[i].Status = "Nomal";
            }

            Materials = new ObservableCollection<MaterialsManagementSystem.Model.Material>(materialsFromDb);
        }
    }

    // 초기 Materials 로드
    public void LoadMaterialGroup()
    {
        using (var dbHelper = DatabaseHelper.Instance)
        {
            dbHelper.OpenConnection();

            var materialGroupsFromDb = materialGroupDB.GetMaterialGroups(true);

            MaterialGroups = new ObservableCollection<MaterialsManagementSystem.Model.MaterialGroup>(materialGroupsFromDb);
        }
    }

    // 자료를 추가하는 메서드
    public void AddMaterial(Material material)
    {
        // Material을 컬렉션에 추가
        Materials.Add(material);

        // 데이터 바인딩을 업데이트
        OnPropertyChanged("Materials");
        LoadMaterialGroup();
    }

    // INotifyPropertyChanged 구현 코드 (속성 변경 알림을 위해 필요)
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
