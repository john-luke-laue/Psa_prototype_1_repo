using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using PSA_Prototype_1.Models;
using System.Data.SqlClient;
using System.Data;

namespace PSA_Prototype_1
{
    public partial class JobViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // The Page is accessed for the first time. 
            if (!IsPostBack)
            {
                // Enable the GridView paging option and  
                // specify the page size. 
                gvPerson.AllowPaging = true;
                gvPerson.PageSize = 15;


                // Enable the GridView sorting option. 
                gvPerson.AllowSorting = true;


                // Initialize the sorting expression. 
                ViewState["SortExpression"] = "SchoolCode ASC";


                // Populate the GridView. 
                BindGridView();
            }
        }

        private void BindGridView()
        {
            // Get the connection string from Web.config.  
            // When we use Using statement,  
            // we don't need to explicitly dispose the object in the code,  
            // the using statement takes care of it. 
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                // Create a DataSet object. 
                DataSet dsPerson = new DataSet();


                // Create a SELECT query. 
                string strSelectCmd = "SELECT SchoolCode, JobName, Hours FROM Jobs";


                // Create a SqlDataAdapter object 
                // SqlDataAdapter represents a set of data commands and a  
                // database connection that are used to fill the DataSet and  
                // update a SQL Server database.  
                SqlDataAdapter da = new SqlDataAdapter(strSelectCmd, conn);


                // Open the connection 
                conn.Open();


                // Fill the DataTable named "Person" in DataSet with the rows 
                // returned by the query.new n 
                da.Fill(dsPerson, "Jobs");


                // Get the DataView from Person DataTable. 
                DataView dvPerson = dsPerson.Tables["Jobs"].DefaultView;


                // Set the sort column and sort order. 
                dvPerson.Sort = ViewState["SortExpression"].ToString();


                // Bind the GridView control. 
                gvPerson.DataSource = dvPerson;
                gvPerson.DataBind();
            }
        }

        // GridView.RowEditing Event 
        protected void gvPerson_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Make the GridView control into edit mode  
            // for the selected row.  
            gvPerson.EditIndex = e.NewEditIndex;


            // Rebind the GridView control to show data in edit mode. 
            BindGridView();


            // Hide the Add button. 
            lbtnAdd.Visible = false;
        } 

        // GridView.RowUpdating Event 
        protected void gvPerson_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                // Create a command object. 
                SqlCommand cmd = new SqlCommand();


                // Assign the connection to the command. 
                cmd.Connection = conn;


                // Set the command text 
                // SQL statement or the name of the stored procedure  
                cmd.CommandText = "UPDATE Jobs SET JobName = @JobName WHERE SchoolCode = @SchoolCode";


                // Set the command type 
                // CommandType.Text for ordinary SQL statements;  
                // CommandType.StoredProcedure for stored procedures. 
                cmd.CommandType = CommandType.Text;


                // Get the PersonID of the selected row. 
                string strSchoolcode = gvPerson.Rows[e.RowIndex].Cells[2].Text;
                string strLastName = ((TextBox)gvPerson.Rows[e.RowIndex].FindControl("TextBox1")).Text;


                // Append the parameters. 
                cmd.Parameters.Add("@SchoolCode", SqlDbType.Int).Value = strSchoolcode;
                cmd.Parameters.Add("@JobName", SqlDbType.NVarChar, 50).Value = strLastName;


                // Open the connection. 
                conn.Open();


                // Execute the command. 
                cmd.ExecuteNonQuery();
            }


            // Exit edit mode. 
            gvPerson.EditIndex = -1;


            // Rebind the GridView control to show data after updating. 
            BindGridView();


            // Show the Add button. 
            lbtnAdd.Visible = true;
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            // Hide the Add button and showing Add panel. 
            lbtnAdd.Visible = false;
            ////pnlAdd.Visible = true;
        } 
    }
}