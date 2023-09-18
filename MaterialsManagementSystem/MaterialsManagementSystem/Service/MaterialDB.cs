using MaterialsManagementSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialsManagementSystem.Service
{
    public class MaterialDB
    {
        // DatabaseHelper 객체 생성
        DatabaseHelper dbHelper = DatabaseHelper.Instance;

        #region "MaterialDB"
        public List<Material> GetMaterials()
        {
            List<Material> materials = new List<Material>();

            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT M.*, C.CODE_NAME AS MATERIAL_GROUP_NAME FROM TB_COM_MATERIAL M INNER JOIN TB_COM_CODE C ON M.MATERIAL_GROUP = C.CODE_ID", dbHelper.OpenConnection()))

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
                dbHelper.CloseConnection();
            }

            return materials;
        }
        public void DeleteMaterialFromDatabase(string materialCode)
        {
            try
            {
                MySqlCommand command = new MySqlCommand("DELETE FROM TB_COM_MATERIAL WHERE MATERIAL_CODE = @materialCode", dbHelper.OpenConnection());
                command.Parameters.AddWithValue("@materialCode", materialCode);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("데이터 삭제 중 오류가 발생했습니다: " + ex.Message);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }

        public void InsertMaterialIntoDatabase(string materialCode, string materialName, string materialGroupName, string useFlag)
        {
            try
            {
                // MATERIAL_GROUP에 해당하는 CODE_ID를 가져오는 쿼리
                string getCodeIdQuery = "SELECT CODE_ID FROM TB_COM_CODE WHERE CODE_NAME = @MaterialGroupName";

                using (MySqlCommand getCodeIdCommand = new MySqlCommand(getCodeIdQuery, dbHelper.OpenConnection()))
                {
                    getCodeIdCommand.Parameters.AddWithValue("@MaterialGroupName", materialGroupName);

                    // CODE_ID를 가져옴
                    string materialGroupCodeId = getCodeIdCommand.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(materialGroupCodeId))
                    {
                        // CODE_ID가 유효한 경우에만 자재를 추가
                        using (MySqlCommand command = new MySqlCommand(
                            "INSERT INTO TB_COM_MATERIAL (MATERIAL_CODE, MATERIAL_NAME, MATERIAL_GROUP, USE_FLAG, CRT_DT, UDT_DT) " +
                            "VALUES (@MaterialCode, @MaterialName, @MaterialGroupCodeId, @UseFlag, NOW(), NOW())", dbHelper.OpenConnection()))
                        {
                            command.Parameters.AddWithValue("@MaterialCode", materialCode);
                            command.Parameters.AddWithValue("@MaterialName", materialName);
                            command.Parameters.AddWithValue("@MaterialGroupCodeId", materialGroupCodeId);
                            command.Parameters.AddWithValue("@UseFlag", useFlag);

                            command.ExecuteNonQuery();
                        }

                    }
                    else
                    {
                        MessageBox.Show("자재 그룹을 찾을 수 없습니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("자재를 추가하는 중 오류가 발생했습니다: " + ex.Message);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }

        public List<Material> GetFilteredMaterials(string materialCodeFilter, string materialNameFilter, string materialGroupFilter, string useFlagFilter)
        {
            List<Material> filteredMaterials = new List<Material>();

            try
            {
                MySqlCommand command = new MySqlCommand("searchMaterials", dbHelper.OpenConnection());
                command.CommandType = CommandType.StoredProcedure;
                {
                    // 파라미터 추가
                    // 매개변수 추가
                    command.Parameters.AddWithValue("@materialCodeFilter", string.IsNullOrEmpty(materialCodeFilter) ? (object)DBNull.Value : materialCodeFilter);
                    command.Parameters.AddWithValue("@materialNameFilter", string.IsNullOrEmpty(materialNameFilter) ? (object)DBNull.Value : materialNameFilter);
                    command.Parameters.AddWithValue("@selectedMaterialGroup", string.IsNullOrEmpty(materialGroupFilter) ? (object)DBNull.Value : materialGroupFilter);
                    command.Parameters.AddWithValue("@selectedUseFlag", string.IsNullOrEmpty(useFlagFilter) ? (object)DBNull.Value : useFlagFilter);

                    // 기존 필터링된 데이터 초기화
                    filteredMaterials.Clear();

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
                            filteredMaterials.Add(material);
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
                dbHelper.CloseConnection();
            }

            return filteredMaterials;
        }
        #endregion

    }
}
