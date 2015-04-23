using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class pages_messages : System.Web.UI.Page
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
            Session["tbl_messages"] = null;
            clear_page();
            load_messages();
            load_friends();
            group_add.Enabled = true;
            group_messages.Enabled = true;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Name required ','info_hidden');", true);
            display_messages((DataTable)Session["tbl_messages"]);

            grid_messages.ClientSettings.Selecting.AllowRowSelect = true;
            grid_messages.ClientSettings.EnablePostBackOnRowClick = true;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void clear_page()
    {
        try
        {
            txt_message.Text = "";
            cmb_user_to.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    private void load_messages()
    {
        DataTable tbl_messages= new DataTable();
        try
        {
            string username = Session["username"].ToString();
            Session["tbl_messages"] = client.load_messages(username).Tables["dbTable"];
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void load_friends()
    {
        DataTable tbl_friends = new DataTable();
        try
        {
            string username = Session["username"].ToString();
            tbl_friends = client.load_allUsers(username).Tables["dbTable"];
            if(tbl_friends != null)
            {
                DataRow drow = tbl_friends.NewRow();
                drow["username"] = "";
                drow["first_name"] = "choose receipient";
                tbl_friends.Rows.InsertAt(drow, 0);
                cmb_user_to.DataValueField = "username";
                cmb_user_to.DataTextField = "first_name";
                cmb_user_to.DataSource = tbl_friends;
                cmb_user_to.DataBind();
            }            
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void display_messages(DataTable tbl_messages)
    {
        try
        {
            grid_messages.MasterTableView.DataSource = tbl_messages;
            grid_messages.DataBind();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    protected void grid_messages_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Row_Index = 0;
        try
        {
            if (grid_messages.SelectedItems.Count > 1)
            {
                GridItem item = grid_messages.SelectedItems[0];
                item.Selected = false;

            }
            foreach (GridDataItem dgi in grid_messages.SelectedItems)
            {
                if (dgi.Selected)
                {
                    Row_Index = dgi.ItemIndex;
                    grid_messages.ClientSettings.Selecting.AllowRowSelect = true;
                    grid_messages.ClientSettings.EnablePostBackOnRowClick = true;
                    group_add.Enabled = true;
                    break;
                }
            }
            GridDataItem dgitem = grid_messages.Items[Row_Index];
            lbl_rowindex.Value = dgitem.ItemIndex.ToString();
            DataRow drow = ((DataTable)Session["tbl_counties"]).Rows[dgitem.DataSetIndex];
            //populate_controls(drow);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btn_Send_Click(object sender, EventArgs e)
    {
        try
        {
            string username_1 = Session["username"].ToString();
            string username_2 = cmb_user_to.SelectedValue;
            string message = txt_message.Text;

            string result = client.send_message(username_1, username_2, message);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar( 'Message sent: " + result + "','info_success');", true);
            initialize_page();
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