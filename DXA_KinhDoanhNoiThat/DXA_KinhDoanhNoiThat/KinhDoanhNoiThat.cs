using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;

namespace DXA_KinhDoanhNoiThat
{
    internal class KinhDoanhNoiThat
    {
        public KinhDoanhNoiThat() { }

        /*--------------------------------------------------------------------*/
        /*|                             FORM_LOGIN                           |*/
        /*--------------------------------------------------------------------*/
        public bool checkUser_Pwd(string userName, string passWord)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=Tger\TGER_22;Initial Catalog=UngDungPhanTich_KDNT;Integrated Security=True"))
                {
                    connect.Open();

                    string checkString = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @username AND MatKhau = @password";
                    SqlCommand cmd = new SqlCommand(checkString, connect);
                    cmd.Parameters.AddWithValue("@username", userName);
                    cmd.Parameters.AddWithValue("@password", passWord);

                    int count = (int)cmd.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Xử lý ngoại lệ, có thể ghi log hoặc thông báo lỗi
                return false;
            }
        }

        public string checkRoles(string userName)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(@"Data Source=Tger\TGER_22;Initial Catalog=UngDungPhanTich_KDNT;Integrated Security=True"))
                {
                    connect.Open();

                    string checkString = "SELECT MaVaiTro FROM NguoiDung_VaiTro WHERE MaNguoiDung = (SELECT MaNguoiDung FROM NguoiDung WHERE TenDangNhap = @username)";
                    SqlCommand cmd = new SqlCommand(checkString, connect);
                    cmd.Parameters.AddWithValue("@username", userName);

                    string roles = cmd.ExecuteScalar()?.ToString(); // Handle NULL value

                    return roles;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Xử lý ngoại lệ, có thể ghi log hoặc thông báo lỗi
                return null;
            }
        }


        /*--------------------------------------------------------------------*/
        /*|                   KET NOI ANALYSIS SERVER                        |*/
        /*--------------------------------------------------------------------*/
        public void ConnectToAnalysisServer()
        {
            try
            {
                // Chuỗi kết nối đến SSAS
                string connectionString = "Provider=MSOLAP;Data Source=Tger\\TGER_22;Catalog=SSAS_DDS;Integrated Security=SSPI;";

                // Tạo kết nối
                using (AdomdConnection conn = new AdomdConnection(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Connected to SSAS successfully.");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Xử lý ngoại lệ, ghi log hoặc thông báo lỗi
            }
        }

        //public Dictionary<string, object> GetUserInfo(string userName)
        //{
        //    try
        //    {
        //        using (SqlConnection connect = new SqlConnection(@"Data Source=Tger\TGER_22;Initial Catalog=UngDungPhanTich_KDNT;Integrated Security=True"))
        //        {
        //            connect.Open();

        //            string query = "SELECT MaNguoiDung, HoTen, Email FROM NguoiDung WHERE TenDangNhap = @username";
        //            SqlCommand cmd = new SqlCommand(query, connect);
        //            cmd.Parameters.AddWithValue("@username", userName);

        //            SqlDataReader reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                Dictionary<string, object> userInfo = new Dictionary<string, object>();
        //                userInfo["MaNguoiDung"] = reader["MaNguoiDung"];
        //                userInfo["HoTen"] = reader["HoTen"];
        //                userInfo["Email"] = reader["Email"];
        //                return userInfo;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);
        //        // Xử lý ngoại lệ, có thể ghi log hoặc thông báo lỗi
        //    }
        //    return null;
        //}
        /*--------------------------------------------------------------------*/
        /*|                             FORM_SAOLUU                          |*/
        /*--------------------------------------------------------------------*/




        /*--------------------------------------------------------------------*/
        /*|                             FORM_PHANTICH                        |*/
        /*--------------------------------------------------------------------*/



        /*--------------------------------------------------------------------*/
        /*|                             FORM_BAOCAO                           |*/
        /*--------------------------------------------------------------------*/



        /*--------------------------------------------------------------------*/
        /*|                             FORM_NAPDULIEU                       |*/
        /*--------------------------------------------------------------------*/
    }
}
