using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class login : System.Web.UI.Page
{
    chat_svc.ChatServiceClient client = new chat_svc.ChatServiceClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //initialize_page();
            if (!IsPostBack)
            {
                initialize_page();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void initialize_page()
    {
        try
        {
            txt_username.Text = "";
            txt_password.Text = "";
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected bool IsLoginValid()
    {
        bool Isvalid = true;

        try
        {
            if (txt_username.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Username is required ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }
            if (txt_password.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Password is required ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }

            if (!IsCredentialLoaded())
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Invalid User Login Credentials ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }

        }
        catch (Exception ex)
        {
            Isvalid = false;
        }
        return Isvalid;
    }
    private bool IsCredentialLoaded()
    {
        bool IsCredentialsOk = false;
        DataSet dset_user_exists = new DataSet();
        DataSet dset_user = new DataSet();
        try
        {
            dset_user_exists = client.user_exists(txt_username.Text);
            if (dset_user_exists.Tables["dbTable"].Rows.Count > 0)
            {
                dset_user = client.user_login(txt_username.Text, txt_password.Text);
                if (dset_user.Tables["dbTable"].Rows.Count > 0)
                {
                    Session["login_status"] = true;
                    Session["first_name"] = dset_user.Tables["dbTable"].Rows[0]["first_name"].ToString();
                    Session["username"] = dset_user.Tables["dbTable"].Rows[0]["username"].ToString();
                    Session["last_name"] = dset_user.Tables["dbTable"].Rows[0]["last_name"].ToString();
                    Session["gender"] = dset_user.Tables["dbTable"].Rows[0]["gender"].ToString();
                    Session["dob"] = dset_user.Tables["dbTable"].Rows[0]["date_of_birth"].ToString();
                    client.logged_in(txt_username.Text);
                    IsCredentialsOk = true;
                }
                else
                {
                    Session["login_status"] = false;
                }                
            }
            else
            {
                initialize_page();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('The username you provided does not exist yet ','info_warning');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('The server is down! Contact Admin ','info_warning');", true);
        }
        return IsCredentialsOk;
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsLoginValid())
            {
                initialize_page();
                return;
            }
            Response.Redirect("pages/index.aspx");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void btn_signup_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("pages/sign_up.aspx");
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}