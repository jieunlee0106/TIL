using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialsManagementSystem.Model;
using System.Windows;



namespace MaterialsManagementSystem.Service
{
    public sealed class DatabaseHelper : IDisposable
    {
        private static readonly Lazy<DatabaseHelper> instance = new Lazy<DatabaseHelper>(() => new DatabaseHelper());

        private readonly string connectionString;
        private MySqlConnection connection;

        public static DatabaseHelper Instance => instance.Value;

        private DatabaseHelper()
        {
            // MySQL 연결 문자열 설정
            connectionString = "Server=15.165.89.250;Port=3306;Database=MaterialManagement;Uid=root;Pwd=jieun;charset=utf8";
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection OpenConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection(connectionString);
            }

            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("데이터베이스 연결 실패: " + ex.Message);
                }
            }

            return connection;
        }


        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }

        #region "MaterialDB"
        public List<Material> GetMaterials()
        {
            List<Material> materials = new List<Material>();

            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT M.*, C.CODE_NAME AS MATERIAL_GROUP_NAME FROM TB_COM_MATERIAL M INNER JOIN TB_COM_CODE C ON M.MATERIAL_GROUP = C.CODE_ID", OpenConnection()))

                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Material material = new Material
                            {
                                MaterialCode = reader["MATERIAL_CODE"] == DBNull.Value ? null : reader["MATERIAL_CODE"].ToString(),
                                MaterialName = reader["MATERIAL_NAME"] == DBNull.Value ? null : reader["MATERIAL_NAME"].ToString(),
                                MaterialGroup = reader["MATERIAL_GROUP_NAME"] == DBNull.Value ? null : reader["MATERIAL_GROUP_NAME"].ToString(),
                                UseFlag = reader["USE_FLAG"] == DBNull.Value ? null : reader["USE_FLAG"].ToString(),
                                CrtDt = reader["CRT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CRT_DT"]),
                                UdtDt = reader["UDT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["UDT_DT"])

                            };
                            materials.Add(material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터를 가져오는 중 오류가 발생했습니다: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }

            return materials;
        }
        //public void DeleteMaterialFromDatabase(string materialCode)
        //{
        //    try
        //    {
        //        MySqlCommand command = new MySqlCommand("DELETE FROM TB_COM_MATERIAL WHERE MATERIAL_CODE = @materialCode", OpenConnection());
        //        command.Parameters.AddWithValue("@materialCode", materialCode);
        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("데이터 삭제 중 오류가 발생했습니다: " + ex.Message);
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}

        //public void InsertMaterialIntoDatabase(string materialCode, string materialName, string materialGroupName, string useFlag)
        //{
        //    try
        //    {
        //        // MATERIAL_GROUP에 해당하는 CODE_ID를 가져오는 쿼리
        //        string getCodeIdQuery = "SELECT CODE_ID FROM TB_COM_CODE WHERE CODE_NAME = @MaterialGroupName";

        //        using (MySqlCommand getCodeIdCommand = new MySqlCommand(getCodeIdQuery, OpenConnection()))
        //        {
        //            getCodeIdCommand.Parameters.AddWithValue("@MaterialGroupName", materialGroupName);

        //            // CODE_ID를 가져옴
        //            string materialGroupCodeId = getCodeIdCommand.ExecuteScalar() as string;

        //            if (!string.IsNullOrEmpty(materialGroupCodeId))
        //            {
        //                // CODE_ID가 유효한 경우에만 자재를 추가
        //                using (MySqlCommand command = new MySqlCommand(
        //                    "INSERT INTO TB_COM_MATERIAL (MATERIAL_CODE, MATERIAL_NAME, MATERIAL_GROUP, USE_FLAG, CRT_DT, UDT_DT) " +
        //                    "VALUES (@MaterialCode, @MaterialName, @MaterialGroupCodeId, @UseFlag, NOW(), NOW())", OpenConnection()))
        //                {
        //                    command.Parameters.AddWithValue("@MaterialCode", materialCode);
        //                    command.Parameters.AddWithValue("@MaterialName", materialName);
        //                    command.Parameters.AddWithValue("@MaterialGroupCodeId", materialGroupCodeId);
        //                    command.Parameters.AddWithValue("@UseFlag", useFlag);

        //                    command.ExecuteNonQuery();
        //                }

        //            }
        //            else
        //            {
        //                MessageBox.Show("자재 그룹을 찾을 수 없습니다.");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("자재를 추가하는 중 오류가 발생했습니다: " + ex.Message);
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}

        //public List<Material> GetFilteredMaterials(string materialCodeFilter, string materialNameFilter, string materialGroupFilter, string useFlagFilter)
        //{
        //    List<Material> filteredMaterials = new List<Material>();

        //    try
        //    {
        //        MySqlCommand command = new MySqlCommand("searchMaterials", OpenConnection());
        //        command.CommandType = CommandType.StoredProcedure;
        //        {
        //            // 파라미터 추가
        //            // 매개변수 추가
        //            command.Parameters.AddWithValue("@materialCodeFilter", string.IsNullOrEmpty(materialCodeFilter) ? (object)DBNull.Value : materialCodeFilter);
        //            command.Parameters.AddWithValue("@materialNameFilter", string.IsNullOrEmpty(materialNameFilter) ? (object)DBNull.Value : materialNameFilter);
        //            command.Parameters.AddWithValue("@selectedMaterialGroup", string.IsNullOrEmpty(materialGroupFilter) ? (object)DBNull.Value : materialGroupFilter);
        //            command.Parameters.AddWithValue("@selectedUseFlag", string.IsNullOrEmpty(useFlagFilter) ? (object)DBNull.Value : useFlagFilter);

        //            // 기존 필터링된 데이터 초기화
        //            filteredMaterials.Clear();

        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Material material = new Material
        //                    {
        //                        MaterialCode = reader["MATERIAL_CODE"] == DBNull.Value ? null : reader["MATERIAL_CODE"].ToString(),
        //                        MaterialName = reader["MATERIAL_NAME"] == DBNull.Value ? null : reader["MATERIAL_NAME"].ToString(),
        //                        MaterialGroup = reader["MATERIAL_GROUP_NAME"] == DBNull.Value ? null : reader["MATERIAL_GROUP_NAME"].ToString(),
        //                        UseFlag = reader["USE_FLAG"] == DBNull.Value ? null : reader["USE_FLAG"].ToString(),
        //                        CrtDt = reader["CRT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CRT_DT"]),
        //                        UdtDt = reader["UDT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["UDT_DT"])
        //                    };
        //                    filteredMaterials.Add(material);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("데이터를 가져오는 중 오류가 발생했습니다: " + ex.Message);
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }

        //    return filteredMaterials;
        //}
        #endregion


        #region "MateriaGrouplDB"


        //public List<MaterialGroup> GetMaterialGroups(bool mainPage)
        //{
        //    List<MaterialGroup> materialGroups = new List<MaterialGroup>();
        //    MaterialGroup Allgroup = new MaterialGroup
        //    {
        //        CodeId = '0'.ToString(),
        //        CodeName = "All",
        //        UseFlag = "All",
        //        CrtDt = Convert.ToDateTime(DateTime.Now),
        //        UdtDt = Convert.ToDateTime(DateTime.Now)
        //    };

        //    if (mainPage)
        //    {
        //        materialGroups.Add(Allgroup);
        //    }


        //    try
        //    {
        //        using (MySqlCommand command = new MySqlCommand("SELECT C.* " +
        //                                                       "FROM TB_COM_CODE C " 
        //                                                       , OpenConnection()))
        //        {
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
  

        //                    MaterialGroup materialgroup = new MaterialGroup
        //                    {
        //                        CodeId = reader["CODE_ID"] == DBNull.Value ? null : reader["CODE_ID"].ToString(),
        //                        CodeName = reader["CODE_NAME"] == DBNull.Value ? null : reader["CODE_NAME"].ToString(),
        //                        UseFlag = reader["USE_FLAG"] == DBNull.Value ? null : reader["USE_FLAG"].ToString(),
        //                        CrtDt = reader["CRT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CRT_DT"]),
        //                        UdtDt = reader["UDT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["UDT_DT"])
        //                    };
                           
        //                    materialGroups.Add(materialgroup);
                            
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("자재그룹 데이터를 가져오는 중 오류가 발생했습니다: " + ex.Message);
        //    }
        //    finally
        //    {
        //        // 데이터베이스 연결 닫기
        //        CloseConnection();
        //    }

        //    return materialGroups;
        //}


        //public void AddMaterialGroupToDatabase(string codeId, string codeName)
        //{
        //    using (MySqlConnection connection = OpenConnection())
        //    {
        //        try
        //        {
        //            using (MySqlCommand command = new MySqlCommand("INSERT INTO MaterialManagement.TB_COM_CODE (CODE_ID, CODE_NAME, USE_FLAG, CRT_DT, UDT_DT) VALUES (@CodeId, @CodeName, 'Y', NOW(), NOW())", connection))
        //            {
        //                command.Parameters.AddWithValue("@CodeId", codeId);
        //                command.Parameters.AddWithValue("@CodeName", codeName);

        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("자재 그룹을 데이터베이스에 추가하는 중 오류가 발생했습니다: " + ex.Message);
        //        }
        //        finally
        //        {
        //            CloseConnection();
        //        }
        //    }
        //}

        //public void DeleteMaterialGroupFromDatabase(string codeIdToDelete)
        //{
        //    try
        //    {
        //        using (MySqlCommand command = new MySqlCommand("DELETE FROM TB_COM_CODE WHERE CODE_ID = @CodeId", OpenConnection()))
        //        {
        //            command.Parameters.AddWithValue("@CodeId", codeIdToDelete);

        //            // DELETE 쿼리 실행
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("자재 그룹 삭제 중 오류가 발생했습니다: " + ex.Message);
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}

        #endregion


     


    }
}
