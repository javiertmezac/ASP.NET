using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

    /// <summary>
    /// Summary description for DBConfiguration
    /// </summary>
    public  class DBConfiguration
    {
        public  DBConfiguration()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string DBConnection
        {
            get
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
                }
                catch (Exception error)
                {
                    //return string.Empty;
                    return error.Data.ToString();
                }
            }
        }
    }
