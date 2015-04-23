using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class pages_friends : System.Web.UI.Page
{
    chat_svc.ChatServiceClient client = new chat_svc.ChatServiceClient();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            Master.btnNewEventHandler += new EventHandler(newrec_btn);
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
                initialize_page();
                Global.isLoaded = true;
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
            Session["tbl_users"] = null;
            load_users();
            group_add.Enabled = true;
            group_users.Enabled = true;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Name required ','info_hidden');", true);
            display_users((DataTable)Session["tbl_users"]);

            grid_users.ClientSettings.Selecting.AllowRowSelect = true;
            grid_users.ClientSettings.EnablePostBackOnRowClick = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void load_users()
    {
        DataTable tbl_users = new DataTable();
        try
        {
            string username = Session["username"].ToString();
            Session["tbl_users"] = client.load_allUsers(username).Tables["dbTable"];
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void display_users(DataTable tbl_users)
    {
        try
        {
            grid_users.MasterTableView.DataSource = tbl_users;
            grid_users.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void grid_users_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Row_Index = 0;
        try
        {
            if (grid_users.SelectedItems.Count > 1)
            {
                GridItem item = grid_users.SelectedItems[0];
                item.Selected = false;

            }
            foreach (GridDataItem dgi in grid_users.SelectedItems)
            {
                if (dgi.Selected)
                {
                    Row_Index = dgi.ItemIndex;
                    grid_users.ClientSettings.Selecting.AllowRowSelect = true;
                    grid_users.ClientSettings.EnablePostBackOnRowClick = true;
                    group_add.Enabled = true;
                    break;
                }
            }
            GridDataItem dgitem = grid_users.Items[Row_Index];
            lbl_rowindex.Value = dgitem.ItemIndex.ToString();
            DataRow drow = ((DataTable)Session["tbl_users"]).Rows[dgitem.DataSetIndex];
            Session["username_2"] = drow["username"].ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void newrec_btn(object sender, EventArgs e)
    {
        string result = "";
        try
        {
            if (grid_users.SelectedItems.Count < 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Select the user you want to send a friend request to ','info_warning');", true);
                return;
            }

            string username_1 = Session["username"].ToString();
            string username_2 = Session["username_2"].ToString();

            if(client.friend_status(username_1, username_2).Tables["dbTable"].Rows.Count > 0)
            {
                string status = client.friend_status(username_1, username_2).Tables["dbTable"].Rows[0][0].ToString();
                if (status == "FRIENDS")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('You are already friends with " + username_2 + "','info_warning');", true);
                    return;
                }
                if (status == "PENDING")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('There already is a pending request to " + username_2 + "','info_warning');", true);
                    return;
                }
            }            
            else
            {
                result = client.send_friendRequest(username_1, username_2);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar( 'Friend Request: " + result + "','info_success');", true);
            }            
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