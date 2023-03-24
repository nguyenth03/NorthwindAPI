using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using NorthWindAPI.Models;
using System.Configuration;

namespace NorthWindAPI.Controllers
{
    public class USERSController : ApiController
    {
        string strconn = ConfigurationManager.ConnectionStrings["NW"].ConnectionString;

        [HttpGet]
        public List<USER> GetAllUser()
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            string sql = "Select * from USERS";
            SqlDataAdapter adapter = new SqlDataAdapter(sql,sqlConn);
            DataSet ds = new DataSet();
            adapter.Fill(ds,"USERS");
            List<USER> lusers = new List<USER>();
            foreach( DataRow use in ds.Tables["USERS"].Rows)
            {
                USER u = new USER();
                u.UserID = (int)use["UserID"];
                u.UserName = use["UserName"].ToString();
                u.Password = use["Password"].ToString();
                u.Description   = use["Description"].ToString();
                lusers.Add(u);
            }
            return lusers;

        }

        [HttpPost]
        public DataSet GetCategoryID(int ID)
        {
            SqlConnection conn= new SqlConnection(strconn);
            String sql = "Select * from USERS where UserID = " + ID;
            SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
            DataSet d = new DataSet();
            adapter.Fill(d, "USERS");
            return d;
        }

        [HttpPut]
        public int InserCategory(string name,string pwd ,string discription)
        {
            SqlConnection strcon = new SqlConnection(strconn);
            string sql = "Insert into USERS(UserName,Password,Description) values(N'" + name + "',N'" + pwd + "',N'" + discription + "')";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = strcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPut]
        public int UpdateCategory(int id ,string name,string pwd,string discription)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string sql = "Update USERS set UserName=N'" + name + "',Password=N'" + pwd + "',Description=N'" + discription + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
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

        [HttpDelete]
        public int DeleteCategory(int id)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string sql = "delete from USERS where UserID =" + id;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
                return 1;
            }catch(Exception ex) { return 0; }
        }
    }
}
