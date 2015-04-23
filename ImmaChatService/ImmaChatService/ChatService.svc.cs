using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ImmaChatService
{
    public class ChatService : IChatService
    {
        ChatBL bl = new ChatBL();

        public string insert_userInfo(Users userObj)
        {
            string result = "";
            try
            {
                result = bl.insert_userInfo(userObj);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public DataSet user_exists(string username)
        {
            DataSet dset_user_exists = new DataSet();
            try
            {
                dset_user_exists = bl.user_exists(username);
            }
            catch (Exception ex)
            {
                throw;
            }
            return dset_user_exists;
        }
        public DataSet user_login(string username, string password)
        {
            DataSet dset_login = new DataSet();
            try
            {
                dset_login = bl.user_login(username, password);
            }
            catch (Exception ex)
            {
                dset_login = null;
            }
            return dset_login;
        }
        public DataSet get_friends(string username)
        {
            DataSet dset_friends= new DataSet();
            try
            {
                dset_friends = bl.get_friends(username);
            }
            catch (Exception ex)
            {
                dset_friends = null;
            }
            return dset_friends;
        }
        public DataSet load_userInfo(string username)
        {
            DataSet dset_userInfo= new DataSet();
            try
            {
                dset_userInfo = bl.load_userInfo(username);
            }
            catch (Exception ex)
            {
                dset_userInfo = null;
            }
            return dset_userInfo;
        }
        public DataSet load_allUsers(string username)
        {
            DataSet dset_allUsers= new DataSet();
            try
            {
                dset_allUsers = bl.load_allUsers(username);
            }
            catch (Exception ex)
            {
                dset_allUsers = null;
            }
            return dset_allUsers;
        }
        public DataSet load_messages(string username)
        {
            DataSet dset_messages = new DataSet();
            try
            {
                dset_messages = bl.load_messages(username);
            }
            catch (Exception ex)
            {
                dset_messages = null;
            }
            return dset_messages;
        }
        public DataSet load_profileInfo(string username)
        {
            DataSet dset_profile= new DataSet();
            try
            {
                dset_profile = bl.load_profileInfo(username);
            }
            catch (Exception ex)
            {
                dset_profile = null;
            }
            return dset_profile;
        }
        public string send_message(string user_from, string user_to, string message)
        {
            string result = "";
            try
            {
                result = bl.send_message(user_from, user_to, message);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string send_friendRequest(string username_1, string username_2)
        {
            string result = "";
            try
            {
                result = bl.send_friendRequest(username_1, username_2);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string accept_friendRequest(string username_1, string username_2)
        {
            string result = "";
            try
            {
                result = bl.accept_friendRequest(username_1, username_2);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string reject_friendRequest(string username_1, string username_2)
        {
            string result = "";
            try
            {
                result = bl.reject_friendRequest(username_1, username_2);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public DataSet friend_status(string username_1, string username_2)
        {
            DataSet dset_status = new DataSet();
            try
            {
                dset_status = bl.friend_status(username_1, username_2);
            }
            catch (Exception ex)
            {
                dset_status = null;
            }
            return dset_status;
        }
        public string logout(string username)
        {
            string result = "";
            try
            {
                result = bl.logout(username);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string logged_in(string username)
        {
            string result = "";
            try
            {
                result = bl.logged_in(username);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public DataSet notify_request(string username)
        {
            DataSet dset_notify = new DataSet();
            try
            {
                dset_notify = bl.notify_request(username);
            }
            catch (Exception ex)
            {
                dset_notify = null;
            }
            return dset_notify;
        }
    }
}
