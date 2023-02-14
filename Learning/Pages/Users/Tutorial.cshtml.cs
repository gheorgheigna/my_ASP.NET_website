using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Globalization;

namespace Learning.Pages.Users
{
    public class TutorialModel : PageModel
    {
        public UsersInfo usersInfo = new UsersInfo();

        public String errorMessage = "";
        public String successMessage = "";
        public String id;
        public String Menu_items;
        public String selectedItems;
        public String Input_string;
        public String reverseString;
        public String title_case;
        public int no_Word;
        public int items=10;
        public void OnGet()
        {
            id = Request.Query["id"];
            Console.WriteLine(id);


            try
            {
                String connctionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=mystore;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connctionString))
                {
                    connection.Open();
                    String sql = "SELECT fullname FROM users WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                               
                               
                                usersInfo.fullname = reader.GetString(0);
                               


                            }
                        }

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() 
        {
            TextInfo titlecase = new CultureInfo("en-US", false).TextInfo;
            Menu_items = Request.Form["Menu_items"];
            Input_string = Request.Form["string"];
            switch (Menu_items)
            {
                case "uppercase":
                    {
                        selectedItems = Input_string.ToUpper();
                        items = 0;
                        break;
                    }
                case "reverse":
                    {
                        string originalString = Input_string;
                        reverseString = string.Empty;
                        for (int i = originalString.Length - 1; i >= 0; i--)
                        {
                            reverseString += originalString[i];
                        }
                        items = 1;
                        break;
                    }
                case "no_of_words":
                    {
                        int a = 0;
                        while (a <= Input_string.Length - 1)
                        {
                            if (Input_string[a] == ' ')
                            {
                                no_Word++;
                            }
                            a++;
                        }
                        items = 2;
                        break;
                    }
                case "titlecase": 
                    {
                        title_case= titlecase.ToTitleCase(Input_string);
                        items = 3;
                        break;
                    }
                    
                    

                default: break;





            }
           
        }
    }
    }

