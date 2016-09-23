using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WcfService
{
    /// <summary>
    /// Summary description for GetCustomer
    /// </summary>
    public class GetCustomer : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string callback = context.Request.QueryString["callback"];
            int customerId = 0;
            string requestkey = context.Request.QueryString["key"];
            string key = "c6e025367cf9b699f8d60a9bb8b142a4";
            int.TryParse(context.Request.QueryString["customerId"], out customerId);
            string json = this.GetCustomersJSON(customerId);
            if (key == requestkey)
            {
                if (!string.IsNullOrEmpty(callback))
                {
                    json = string.Format("{0}({1});", callback, json);
                }

                context.Response.ContentType = "text/json";
                context.Response.Write(json);
            }

        }

        private string GetCustomersJSON(int customerId)
        {
            List<object> customers = new List<object>();

            using (SqlConnection conn = new SqlConnection(Degisken.SqlStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM t_test WHERE id = @CustomerId OR @CustomerId = 0";
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new
                            {
                                CustomerId = sdr["id"],
                                Name = sdr["unvan"]
                            });
                        }
                    }
                    conn.Close();
                }
                return (new JavaScriptSerializer().Serialize(customers));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}