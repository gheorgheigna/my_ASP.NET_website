using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace Learning.Pages.Users
{
    public class IndexModel : PageModel
    {
        public List<UsersInfo> listUsers = new List<UsersInfo>();
        public void OnGet()
        {
            try
            {
                String connctionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection=new SqlConnection(connctionString)) 
                {   
                    connection.Open();
                    String sql = "SELECT * FROM users";
                    using (SqlCommand command= new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        while (reader.Read())
                        {
                            UsersInfo usersInfo= new UsersInfo();
                                usersInfo.id = "" + reader.GetInt32(0);
                                usersInfo.username= reader.GetString(1);
                                usersInfo.passwordd= reader.GetString(2);
                                usersInfo.fullname= reader.GetString(3); 
                                usersInfo.email= reader.GetString(4);
                                usersInfo.phone= reader.GetString(5);
                                usersInfo.data_time= reader.GetDateTime(6).ToString();
                                listUsers.Add(usersInfo);
                        }

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Exception "+ex.ToString());
            }
            
            
        }
    }
    public class UsersInfo
    {
        public String id;
        public String username;
        public String passwordd;
        public String fullname;
        public String email;
        public String phone;
        public String data_time;
        
    }
   
}
