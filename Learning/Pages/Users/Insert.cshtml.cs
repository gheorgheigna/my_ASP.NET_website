using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Learning.Pages.Users
{
    public class Index1Model : PageModel
    {
        public UsersInfo usersInfo = new UsersInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
            usersInfo.username = Request.Form["username"];
            usersInfo.passwordd = Request.Form["passwordd"];
            usersInfo.fullname = Request.Form["fullname"];
            usersInfo.email = Request.Form["email"];
            usersInfo.phone = Request.Form["phone"];
            //clientInfo.name=Request.Form["name"];
            if (usersInfo.username.Length == 0 || usersInfo.passwordd.Length == 0 || usersInfo.fullname.Length == 0 ||
            usersInfo.email.Length == 0 || usersInfo.phone.Length == 0)
            {
                errorMessage = "All the fileds are required";
                return;
            }
            //save the new users into the database

            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO users" + "(username, passwordd, fullname, email, phone) VALUES" +
                    "(@username, @passwordd, @fullname, @email, @phone);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@username", usersInfo.username);
                        command.Parameters.AddWithValue("@passwordd", usersInfo.passwordd);
                        command.Parameters.AddWithValue("@fullname", usersInfo.fullname);
                        command.Parameters.AddWithValue("@email", usersInfo.email);
                        command.Parameters.AddWithValue("@phone", usersInfo.phone);

                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;


            }
            usersInfo.username = "";
            usersInfo.passwordd = "";
            usersInfo.fullname = "";
            usersInfo.email = "";
            usersInfo.phone = "";
            successMessage = "Succesfull! You have an account!";
            //Response.Redirect("/Users/Index");


        }
    }
}
