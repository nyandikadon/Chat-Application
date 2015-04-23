using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ImmaChatService
{
    public class ChatBL
    {
        public DataSet user_login(string username, string password)
        {
            DataSet dset_login = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select  username, password, first_name, last_name, gender, date_of_birth   
                                   from users where username='" + username + "' and password='" + password + "'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_login = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                dset_login = null;
            }
            return dset_login;
        }
        public DataSet user_exists(string username)
        {
            DataSet dset_user_exists = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select  1 from users where username='" + username + "'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_user_exists = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_user_exists;
        }
        public string generate_serial(string serial_code)
        {
            string serial_value = " ";
            string sp_name = "generate_serials";
            DBAccessClass dbclass = new DBAccessClass();
            DataTable dtable = new DataTable();
            try
            {
                dbclass.SerialCode = serial_code;
                dtable = dbclass.ExecuteStoredProc(sp_name).Tables["dbTable"];
                serial_value = dtable.Rows[0]["value"].ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
            return serial_value;
        }
        public string insert_userInfo(Users userObj)
        {
            DBAccessClass dbclass = new DBAccessClass();
            string SqlStatement = "";
            try
            {
                DateTime dob = DateTime.Parse(userObj.date_of_birth);
                string user_id = generate_serial("USER");

                SqlStatement += @" insert into users(user_id, username, password, first_name, last_name, gender, date_of_birth) ";
                SqlStatement += @" values('" + user_id + "','" + userObj.username + "','" + userObj.password + "','" + 
                    userObj.first_name + "','" + userObj.last_name + "','" + userObj.gender + "',CONVERT(datetime,'" + dob + "',102))";
                dbclass.AddSqlStatements(SqlStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error inserting", ex);
            }
            return dbclass.ExecuteSqlStatements();
        }
        public DataSet get_friends(string username)
        {
            DataSet dset_friends = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select friend_id, username_1, username_2 from friends where (username_1 = '" + username + "' or username_2 = '" + username + "') and status = 'FRIENDS'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_friends = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_friends;
        }
        public DataSet load_userInfo(string username)
        {
            DataSet dset_userInfo= new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select username, first_name, last_name, gender, date_of_birth, status from users where username ='" + username + "'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_userInfo = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_userInfo;
        }
        public DataSet load_allUsers(string username)
        {
            DataSet dset_allUsers = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select  * from users where username not like '" + username + "'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_allUsers = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_allUsers;
        }
        public DataSet load_messages(string username)
        {
            DataSet dset_messages = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select  * from messages where user_from  ='" + username + "' or user_to ='" + username + "'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_messages = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_messages;
        }
        public DataSet load_profileInfo(string username)
        {
            DataSet dset_profile = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select * from users where username  ='" + username + "'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_profile = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_profile;
        }
        public string send_message(string user_from, string user_to, string message)
        {
            DBAccessClass dbclass = new DBAccessClass();
            string SqlStatement = "";
            try
            {
                SqlStatement += @"insert into [messages] (user_from, user_to, [message]) ";
                SqlStatement += @"values('" + user_from + "','" + user_to + "','" + message + "')";
                dbclass.AddSqlStatements(SqlStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error inserting", ex);
            }
            return dbclass.ExecuteSqlStatements();
        }
        public string send_friendRequest(string username_1, string username_2)
        {
            DBAccessClass dbclass = new DBAccessClass();
            string SqlStatement = "";
            try
            {
                string friend_id = generate_serial("FRND");

                SqlStatement += @"insert into friends(friend_id, username_1, username_2, status) ";
                SqlStatement += @"values('" + friend_id + "','" + username_1 + "','" + username_2 + "','PENDING')";
                dbclass.AddSqlStatements(SqlStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error inserting", ex);
            }
            return dbclass.ExecuteSqlStatements();
        }
        public string accept_friendRequest(string username_1, string username_2)
        {
            DBAccessClass dbclass = new DBAccessClass();
            string SqlStatement = "";
            try
            {
                SqlStatement += @"update friends set status = 'FRIENDS' where (username_1 = '"+ username_1 + "' and username_2 = '" + username_2 + "') or (username_2 = '" + username_1 + "' and username_1 = '" + username_2 + "')";
                dbclass.AddSqlStatements(SqlStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error inserting", ex);
            }
            return dbclass.ExecuteSqlStatements();
        }
        public string reject_friendRequest(string username_1, string username_2)
        {
            DBAccessClass dbclass = new DBAccessClass();
            string SqlStatement = "";
            try
            {
                SqlStatement += @"update friends set status = 'NULL' (where username_1 = '" + username_1 + "' and username_2 = '" + username_2 + "') or (where username_2 = '" + username_1 + "' and username_1 = '" + username_2 + "')";
                dbclass.AddSqlStatements(SqlStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error inserting", ex);
            }
            return dbclass.ExecuteSqlStatements();
        }
        public DataSet friend_status(string username_1, string username_2)
        {
            DataSet dset_status = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select status from friends where (username_1  ='" + username_1 + "' and username_2 ='" + username_2 + "') or (username_1  ='" 
                    + username_2 + "' and username_2 ='" + username_1 + "')";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_status = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_status;
        }
        public string logout(string username)
        {
            DBAccessClass dbclass = new DBAccessClass();
            string SqlStatement = "";
            try
            {
                SqlStatement += @"update users set status = 'OFFLINE' where username = '" + username + "'";
                dbclass.AddSqlStatements(SqlStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error inserting", ex);
            }
            return dbclass.ExecuteSqlStatements();
        }
        public string logged_in(string username)
        {
            DBAccessClass dbclass = new DBAccessClass();
            string SqlStatement = "";
            try
            {
                SqlStatement += @"update users set status = 'ONLINE' where username = '" + username + "'";
                dbclass.AddSqlStatements(SqlStatement);
            }
            catch (Exception ex)
            {
                throw new Exception("There was an error inserting", ex);
            }
            return dbclass.ExecuteSqlStatements();
        }
        public DataSet notify_request(string username)
        {
            DataSet dset_notify = new DataSet();
            string SqlStatement = "";
            try
            {
                SqlStatement = @" select username_1, username_2 from friends where (username_1  ='" + username + "' or username_2  ='" + username + "') and status = 'PENDING'";
                DBAccessClass dbclass = new DBAccessClass();
                dbclass.SearchSqlStatements(SqlStatement);
                dset_notify = dbclass.Dbdset;
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_notify;
        }
    }
}