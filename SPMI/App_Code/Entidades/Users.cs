using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

    /// <summary>
    /// Summary description for Users
    /// </summary>
    public class Users
    {
        public Users()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variables privadas
        string K_PREFIX = "Users";
        int _id = 0;
        string _name = string.Empty;
        string _username = string.Empty;
        string _password = string.Empty;
        DateTime _regDate = DateTime.Now;
        DateTime _expDate = DateTime.Now;
        bool _status = true;       
        #endregion

        #region Propiedades públicas
        /// <summary>
        /// Identificador del Usuario
        /// </summary>
        public int ID
        {
            get { return this._id; }
            set { this._id = value; }
        }
        /// <summary>
        /// Nombre del Usuario
        /// </summary>
        public String Name
        {
            get { return this._name; }
            set { this._name = value.Trim().ToUpper(); }
        }
        /// <summary>
        /// Username
        /// </summary>
        public String UserName
        {
            get { return this._username; }
            set { this._username = value.Trim(); }
        }
        /// <summary>
        /// Password del Usuario
        /// </summary>
        public String Password
        {
            get { return this._password; }
            set { this._password = value.Trim(); }
        }
        /// <summary>
        /// Fecha de Registro Password
        /// </summary>
        public DateTime RegistrationDate
        {
            get { return this._regDate; }
            set { this._regDate = value; }
        }
        /// <summary>
        /// Fecha de Vencimiento del Usuario
        /// </summary>
        public DateTime ExpirationDate
        {
            get { return this._expDate; }
            set { this._expDate = value; }
        }
        /// <summary>
        /// Indica si el usuario esta activo o inactivo
        /// </summary>
        public bool Status
        {
            get { return this._status; }
            set { this._status = value; }
        }       
        #endregion

        #region Métodos públicos
        /// <summary>
        /// Si el nombre y password son correctos, regresara 'true'
        /// </summary>
        public bool Login()
        {
            
            SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIX + "_login");
            DBaccess.ParameterAdd(cmd, "@username", SqlDbType.VarChar, this.UserName);
            DBaccess.ParameterAdd(cmd, "@password", SqlDbType.VarChar, this.Password);

            DataTable result = DBaccess.ExecuteSQLSelect(cmd);
            if (result.Rows.Count > 0)
            {
                this._id = Convert.ToInt32(result.Rows[0]["id"]);
                   return this.Load();
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Carga un usuario
        /// </summary>
        /// <returns>'true' si fue correcto, 'false' si fue incorrecto</returns>
        public bool Load()
        {
            SqlCommand cmd = DBaccess.CreateSQLCommand(K_PREFIX + "_SELECT");
            DBaccess.ParameterAdd(cmd, "@id", SqlDbType.Int, this._id);

            DataTable result = DBaccess.ExecuteSQLSelect(cmd);
            if (result.Rows.Count > 0)
            {
                this.Name = result.Rows[0]["name"].ToString();
                this.UserName = result.Rows[0]["username"].ToString();
                this.Password = result.Rows[0]["password"].ToString();
                this.RegistrationDate = Convert.ToDateTime(result.Rows[0]["regDate"]);
                this.ExpirationDate = Convert.ToDateTime(result.Rows[0]["expDate"]);
                this.Status = Convert.ToBoolean(result.Rows[0]["status"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
