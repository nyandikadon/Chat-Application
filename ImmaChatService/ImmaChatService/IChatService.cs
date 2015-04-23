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
    [ServiceContract]
    public interface IChatService
    {
        [OperationContract]
        DataSet user_exists(string username);

        [OperationContract]
        DataSet user_login(string username, string password);

        [OperationContract]
        string insert_userInfo(Users userObj);

        [OperationContract]
        DataSet get_friends(string username);

        [OperationContract]
        DataSet load_userInfo(string username);

        [OperationContract]
        DataSet load_allUsers(string username);

        [OperationContract]
        DataSet load_messages(string username);

        [OperationContract]
        DataSet load_profileInfo(string username);

        [OperationContract]
        string send_message(string user_from, string user_to, string message);

        [OperationContract]
        string send_friendRequest(string username_1, string username_2);

        [OperationContract]
        string accept_friendRequest(string username_1, string username_2);

        [OperationContract]
        string reject_friendRequest(string username_1, string username_2);

        [OperationContract]
        DataSet friend_status(string username_1, string username_2);

        [OperationContract]
        string logout(string username);

        [OperationContract]
        string logged_in(string username);

        [OperationContract]
        DataSet notify_request(string username);
    }
}
