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
            System.Collections.Specialized.ListDictionary listDictionary
                = new System.Collections.Specialized.ListDictionary();
            listDictionary.Add("Name", Name.Text);
            listDictionary.Add("Password", Password.Text);
            LinqDataSource1.Insert(listDictionary);
        }
    }
}