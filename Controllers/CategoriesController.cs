using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Data.SqlClient;
using System.Data;
using NorthWindAPI.Models;
using System.Configuration;
//Edit file in github
namespace NorthWindAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        string strconn = ConfigurationManager.ConnectionStrings["NW"].ConnectionString;

        [HttpGet]
        [Route("api/IndexCategory")]
        public List<Category> GetCategories()
        {
            SqlConnection conn = new SqlConnection(strconn);
            string Sql = "Select * from Categories";
            SqlDataAdapter da = new SqlDataAdapter(Sql,conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Categories");
            List<Category> categories = new List<Category>();
            foreach(DataRow i in ds.Tables["Categories"].Rows)
            {
                Category c = new Category();
                c.CategoryID = (int)i["CategoryID"];
                c.CategoryName = i["CategoryName"].ToString();
                categories.Add(c);
            }
            return categories;
        }

        [HttpPost]
        [Route("api/Detail")]
        public DataSet GetCategoryID(int ID)
        {
            SqlConnection s = new SqlConnection(strconn);
            string sql = "Select * from Categories where CategoryID =" + ID;
            SqlDataAdapter da = new SqlDataAdapter(sql, s);
            DataSet ds = new DataSet();
            da.Fill(ds, "Categories");
            return ds;
        }

        [HttpPost]
        [Route("api/SearchCategory")]
        public List<Category> Search(string str)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string sql = "Select * from Categories where CategoryName like '%" + str + "%'";
            SqlDataAdapter da = new SqlDataAdapter(sql,conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Categories");
            List<Category> ListC = new List<Category>();
            foreach(DataRow dL in ds.Tables["Categories"].Rows)
            {
                Category c = new Category();
                c.CategoryID = (int)dL["CategoryID"];
                c.CategoryName = (string)dL["CategoryName"];
                ListC.Add(c);
            }
            return ListC;
        }
        [HttpPost]
        [Route("api/Category/Insert")]
        public int InsertCategory(string CategoryName, string Description)
        {
            SqlConnection conn = new SqlConnection(strconn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            string Sql = "insert into Categories (CategoryName,Description) values (N'" + CategoryName + "',N'" + Description + "')";
            cmd.CommandText = Sql;
            return 0;
        }

        [HttpPost]
        [Route("api/Categories/Update")]
        public int UpdateCategory(int id,string CategoryName, string Description)
        {
            SqlConnection conn = new SqlConnection(strconn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            string Sql = "Update Categories  set CategoryName =N'" + CategoryName + "',Description =N'" + Description + "' where CategoryID=" + id;
            cmd.CommandText = Sql;
            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }catch(Exception ex)
            {
                return 0;
            }
        }

        [HttpDelete]
        [Route("api/Delete")]
        public int DeleteCategory(int id)
        {
            SqlConnection conn = new SqlConnection(strconn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            string sql = "Delete from Categories where CategoryID =" + id;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }catch(Exception ex)
            {
                return 0;
            }
        }

    }
}
