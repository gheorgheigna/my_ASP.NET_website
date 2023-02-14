using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection;

namespace Learning.Pages.Users
{
    public class LoginModel : PageModel
    {
        public UsersInfo usersInfo = new UsersInfo();
       
        public String errorMessage = "";
        public String successMessage = "";
        public List<UsersInfo> listUsers = new List<UsersInfo>();
        public string username1="";
        public string passwordd1 = "";
        
        public void OnGet() { 
        }
        public void OnPost()
        {
            usersInfo.username = Request.Form["username"];
            usersInfo.passwordd = Request.Form["passwordd"];
            
            if (usersInfo.username.Length == 0 || usersInfo.passwordd.Length == 0 )
            {
                errorMessage = "All the fileds are required";
                return;
            }
           
            //verify identity
            try
            {
                if (usersInfo.username == "gh" && usersInfo.passwordd == "gh")
                {
                    Response.Redirect("/Users/Index");
                }
               String connctionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connctionString))
                {
                    connection.Open();
                    String sql = "SELECT username,passwordd, id FROM users";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                       
                        using (SqlDataReader reader = command.ExecuteReader())
                            while(reader.Read())
                            {
                                UsersInfo usersInfo = new UsersInfo();
                                usersInfo.username = reader.GetString(0);
                                usersInfo.passwordd = reader.GetString(1);
                                usersInfo.id = "" + reader.GetInt32(2);
                                listUsers.Add(usersInfo);
                             
                            }

                        command.ExecuteNonQuery();
                        foreach (var i in listUsers)
                        {
                            if(i.username== usersInfo.username &&i.passwordd == usersInfo.passwordd)
                            {
                                
                                Response.Redirect("/Users/Tutorial?id="+i.id);
                                                              
                            }
                        }
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
            errorMessage = "Username or passwor is not correct, please try again";
            //successMessage = "Succesfull! You have an account!";
            //Response.Redirect("/Users/Index");


        }

       
    }
    
}
