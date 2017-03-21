using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Blog
{
    public partial class Default : System.Web.UI.Page
    {
        /*SqlConnection myConnection = new SqlConnection(
                        "server=localhost;uid=sa;pwd=;database=pubs");*/
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Name.Text == "" || Password.Text == "")
            {
                Label2.Text = "Не введен логин или пароль!";
            }
            else
            {
                SqlDataSource1.Insert();
                Label2.Text = "Запись успешно добавлена!";
            }
        }
    }
}