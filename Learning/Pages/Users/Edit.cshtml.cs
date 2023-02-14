using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Learning.Pages.Users
{
    public class EditModel : PageModel
    {
        public UsersInfo usersInfo = new UsersInfo();
       
        public String errorMessage = "";
        public String successMessage = "";
        
        public void OnGet()
        {
            String id =Request.Query["id"];
           
            try
            {
                String connctionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connctionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                        
                                usersInfo.id = "" + reader.GetInt32(0);
                                usersInfo.username = reader.GetString(1);
                                usersInfo.passwordd = reader.GetString(2);
                                usersInfo.fullname = reader.GetString(3);
                                usersInfo.email = reader.GetString(4);
                                usersInfo.phone = reader.GetString(5);


                            }
                        }

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch(Exception ex) 
            {
                errorMessage= ex.Message;
            }
        }
        public void OnPost()
        {

            usersInfo.username = Request.Form["username"];
            usersInfo.passwordd = Request.Form["passwordd"];
            usersInfo.fullname = Request.Form["fullname"];
            usersInfo.email = Request.Form["email"];
            usersInfo.phone = Request.Form["phone"];
            usersInfo.id = Request.Form["id"];
            if (usersInfo.username.Length == 0 || usersInfo.passwordd.Length == 0 || usersInfo.fullname.Length == 0 ||
            usersInfo.email.Length == 0 || usersInfo.phone.Length == 0)
            {
                errorMessage = "All the fileds are required";
                return;
            }


            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE  users SET username=@username, passwordd=@passwordd, fullname=@fullname, email=@email, phone=@phone WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", usersInfo.username);
                        command.Parameters.AddWithValue("@passwordd", usersInfo.passwordd);
                        command.Parameters.AddWithValue("@fullname", usersInfo.fullname);
                        command.Parameters.AddWithValue("@email", usersInfo.email);
                        command.Parameters.AddWithValue("@phone", usersInfo.phone);
                        command.Parameters.AddWithValue("@id", usersInfo.id);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Users/Index");
        }

    }
}
