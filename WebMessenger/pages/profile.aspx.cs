using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_profile : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            Global.isLoaded = false;
            initialize_page();
        }
    }
    private void initialize_page()
    {
        try
        {
            isSession();
            lbl_first_name.Text = Session["first_name"].ToString();
            lbl_last_name.Text = Session["last_name"].ToString();
            lbl_gender.Text = Session["gender"].ToString();
            lbl_dob.Text = DateTime.Parse(Session["dob"].ToString()).ToString("dd/MM/yy");
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