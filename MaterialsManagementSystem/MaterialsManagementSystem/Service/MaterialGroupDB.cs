using MaterialsManagementSystem.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialsManagementSystem.Service
{
    public class MaterialGroupDB
    {

        // DatabaseHelper 객체 생성
        DatabaseHelper dbHelper = DatabaseHelper.Instance;

        public List<MaterialGroup> GetMaterialGroups(bool mainPage)
        {
            List<MaterialGroup> materialGroups = new List<MaterialGroup>();
            MaterialGroup Allgroup = new MaterialGroup
            {
                CodeId = '0'.ToString(),
                CodeName = "All",
                UseFlag = "All",
                CrtDt = Convert.ToDateTime(DateTime.Now),
                UdtDt = Convert.ToDateTime(DateTime.Now)
            };

            if (mainPage)
            {
                materialGroups.Add(Allgroup);
            }


            try
            {
                using (MySqlCommand command = new MySqlCommand("SELECT C.* " +
                                                               "FROM TB_COM_CODE C "
                                                               , dbHelper.OpenConnection()))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            MaterialGroup materialgroup = new MaterialGroup
                            {
                                CodeId = reader["CODE_ID"] == DBNull.Value ? null : reader["CODE_ID"].ToString(),
                                CodeName = reader["CODE_NAME"] == DBNull.Value ? null : reader["CODE_NAME"].ToString(),
                                UseFlag = reader["USE_FLAG"] == DBNull.Value ? null : reader["USE_FLAG"].ToString(),
                                CrtDt = reader["CRT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["CRT_DT"]),
                                UdtDt = reader["UDT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(reader["UDT_DT"])
                            };

                            materialGroups.Add(materialgroup);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("자재그룹 데이터를 가져오는 중 오류가 발생했습니다: " + ex.Message);
            }
            finally
            {
                // 데이터베이스 연결 닫기
                dbHelper.CloseConnection();
            }

            return materialGroups;
        }


        public void AddMaterialGroupToDatabase(string codeId, string codeName)
        {
            using (MySqlConnection connection = dbHelper.OpenConnection())
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO MaterialManagement.TB_COM_CODE (CODE_ID, CODE_NAME, USE_FLAG, CRT_DT, UDT_DT) VALUES (@CodeId, @CodeName, 'Y', NOW(), NOW())", connection))
                    {
                        command.Parameters.AddWithValue("@CodeId", codeId);
                        command.Parameters.AddWithValue("@CodeName", codeName);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("자재 그룹을 데이터베이스에 추가하는 중 오류가 발생했습니다: " + ex.Message);
                }
                finally
                {
                    dbHelper.CloseConnection();
                }
            }


            // 자재 그룹 정보 불러오기
            MaterialVM materialVM = new MaterialVM();
            materialVM.LoadMaterialGroup();
        }

        public void DeleteMaterialGroupFromDatabase(string codeIdToDelete)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("DELETE FROM TB_COM_CODE WHERE CODE_ID = @CodeId", dbHelper.OpenConnection()))
                {
                    command.Parameters.AddWithValue("@CodeId", codeIdToDelete);

                    // DELETE 쿼리 실행
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("자재 그룹 삭제 중 오류가 발생했습니다: " + ex.Message);
            }
            finally
            {
                dbHelper.CloseConnection();
            }
        }
    }
}
