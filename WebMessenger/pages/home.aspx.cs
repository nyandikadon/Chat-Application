using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class pages_home : System.Web.UI.Page
{
    chat_svc.ChatServiceClient client = new chat_svc.ChatServiceClient();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Master.btnLogOutEventHandler += new EventHandler(logout_btn);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                tr_request.Visible = false;
                initialize_page();
                Global.isLoaded = false;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void initialize_page()
    {
        try
        {
            isSession();
            Session["tbl_friends"] = null;
            tr_request.Visible = false;
            checkNotifications();
            load_friends();            
            group_add.Enabled = true;
            group_friends.Enabled = true;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Name required ','info_hidden');", true);
            display_friends((DataTable)Session["tbl_friends"]);

            grid_friends.ClientSettings.Selecting.AllowRowSelect = true;
            grid_friends.ClientSettings.EnablePostBackOnRowClick = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void checkNotifications()
    {
        DataTable tbl_request= new DataTable();
        try
        {
            string username = Session["username"].ToString();
            tbl_request = client.notify_request(username).Tables["dbTable"];
            if(tbl_request.Rows.Count >= 1)
            {
                string username_2 = tbl_request.Rows[0]["username_2"].ToString();
                if(username_2 == username)
                {
                    username_2 = tbl_request.Rows[0]["username_1"].ToString();
                    lbl_username.Text = username_2;
                }
                else
                {
                    lbl_username.Text = username_2;
                }
                tr_request.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void load_friends()
    {
        DataTable tbl_friends = new DataTable();
        tbl_friends.Columns.Add("username");
        tbl_friends.Columns.Add("first_name");
        tbl_friends.Columns.Add("last_name");
        tbl_friends.Columns.Add("status");

        try
        {
            try
            {
                string username = Session["username"].ToString();
                string friend_username = "";
                DataTable friends = client.get_friends(username).Tables["dbTable"];                
                if (friends != null)
                {

                    int counter = friends.Rows.Count;
                    for (int n = 0; n < counter; n++)
                    {
                        if (username == friends.Rows[n]["username_1"].ToString().Trim())
                        {
                            friend_username = friends.Rows[n]["username_2"].ToString();
                            DataRow drow = client.load_userInfo(friend_username).Tables["dbTable"].Rows[0];
                            DataRow newrow = tbl_friends.NewRow();
                            newrow["username"] = drow["username"];
                            newrow["first_name"] = drow["first_name"];
                            newrow["last_name"] = drow["last_name"];
                            newrow["status"] = drow["status"];
                            tbl_friends.Rows.Add(newrow);
                        }
                        else if (username == friends.Rows[n]["username_2"].ToString().Trim())
                        {
                            friend_username = friends.Rows[n]["username_1"].ToString();
                            DataRow drow = client.load_userInfo(friend_username).Tables["dbTable"].Rows[0];
                            DataRow newrow = tbl_friends.NewRow();
                            newrow["username"] = drow["username"];
                            newrow["first_name"] = drow["first_name"];
                            newrow["last_name"] = drow["last_name"];
                            newrow["status"] = drow["status"];
                            tbl_friends.Rows.Add(newrow);
                        }
                    }
                    Session["tbl_friends"] = tbl_friends;
                }
                else
                {
                    DataRow drow = tbl_friends.NewRow();
                    drow["username"] = "You have no friends yet. Navigate to 'Find Friends' and add some!";
                    tbl_friends.Rows.Add(drow);
                    Session["tbl_friends"] = tbl_friends;
                }
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void display_friends(DataTable tbl_friends)
    {
        try
        {
            grid_friends.MasterTableView.DataSource = tbl_friends;
            grid_friends.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void grid_friends_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Row_Index = 0;
        try
        {
            if (grid_friends.SelectedItems.Count > 1)
            {
                GridItem item = grid_friends.SelectedItems[0];
                item.Selected = false;

            }
            foreach (GridDataItem dgi in grid_friends.SelectedItems)
            {
                if (dgi.Selected)
                {
                    Row_Index = dgi.ItemIndex;
                    grid_friends.ClientSettings.Selecting.AllowRowSelect = true;
                    grid_friends.ClientSettings.EnablePostBackOnRowClick = true;
                    group_add.Enabled = true;
                    break;
                }
            }
            GridDataItem dgitem = grid_friends.Items[Row_Index];
            lbl_rowindex.Value = dgitem.ItemIndex.ToString();
            DataRow drow = ((DataTable)Session["tbl_counties"]).Rows[dgitem.DataSetIndex];
            //populate_controls(drow);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btn_accept_Click(object sender, EventArgs e)
    {
        string username = Session["username"].ToString();
        string result = "";
        try
        {
            result = client.accept_friendRequest(username, lbl_username.Text);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar( 'Friend request accepted: " + result + "','info_success');", true);
            initialize_page();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btn_reject_Click(object sender, EventArgs e)
    {
        string username = Session["username"].ToString();
        string result = "";
        try
        {
            result = client.reject_friendRequest(username, lbl_username.Text);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar( 'Friend request rejected: " + result + "','info_success');", true);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public void logout_btn(object sender, EventArgs e)
    {
        try
        {
            string username = Session["username"].ToString();
            client.logout(username);
            Response.Redirect("~/login.aspx");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void isSession()
    {
        try
        {
            string username = Session["username"].ToString();
        }
        catch (Exception ex)
        {
            Response.Redirect("~/login.aspx");
        }
    }
}