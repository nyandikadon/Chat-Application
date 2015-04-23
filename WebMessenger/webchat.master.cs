using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class webchat : System.Web.UI.MasterPage
{
    public event EventHandler btnNewEventHandler;
    public event EventHandler btnLogOutEventHandler;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Request.UserAgent.Contains("AppleWebKit"))
            Request.Browser.Adapters.Clear();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lbl_first_name.Text = Session["first_name"].ToString().ToUpper();
                panel_controls.Visible = Global.isLoaded;
                create_menu(); 
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void create_menu()
    {
        try
        {
            RadMenuItem BaseMenu = new RadMenuItem();
            RadMenuItem SecMenu = new RadMenuItem();
            RadMenuItem ThirdMenu = new RadMenuItem();

            BaseMenu = new RadMenuItem();
            BaseMenu.Text = "Navigation";
            BaseMenu.PostBack = false;
            webchat_menu.Items.Add(BaseMenu);

            SecMenu = new RadMenuItem();
            SecMenu.Text = "Home";
            SecMenu.PostBack = true;
            SecMenu.NavigateUrl = "pages\\home.aspx";
            BaseMenu.Items.Add(SecMenu);

            SecMenu = new RadMenuItem();
            SecMenu.Text = "Profile";
            SecMenu.PostBack = true;
            SecMenu.NavigateUrl = "pages\\profile.aspx";
            BaseMenu.Items.Add(SecMenu);

            SecMenu = new RadMenuItem();
            SecMenu.Text = "Messages";
            SecMenu.PostBack = true;
            SecMenu.NavigateUrl = "pages\\messages.aspx";
            BaseMenu.Items.Add(SecMenu);

            SecMenu = new RadMenuItem();
            SecMenu.Text = "Find Friends";
            SecMenu.PostBack = true;
            SecMenu.NavigateUrl = "pages\\users.aspx";
            BaseMenu.Items.Add(SecMenu);

        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnNewEventHandler != null)
            {
                btnNewEventHandler(sender, e);                
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btn_logout_Click(object sender, EventArgs e)
    {
        try
        {                       
            if (btnLogOutEventHandler != null)
            {
                btnLogOutEventHandler(sender, e);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
