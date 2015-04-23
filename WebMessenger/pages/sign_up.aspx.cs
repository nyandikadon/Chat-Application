using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class pages_sign_up : System.Web.UI.Page
{
    chat_svc.ChatServiceClient client = new chat_svc.ChatServiceClient();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

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
            txt_first_name.Text = "";
            txt_last_name.Text = "";
            txt_password_retype.Text = "";
            cmb_gender.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private bool info_valid()
    {
        bool Isvalid = true;
        try
        {
            if (txt_first_name.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('First name is required ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }
            if (txt_last_name.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Last name is required ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }
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
            if (txt_password_retype.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Confirm password ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }
            if(cmb_gender.SelectedItem.Text == "Select gender")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Select gender ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }
            if(dtp_dob.IsEmpty)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Provide correct date ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }
            if(txt_password_retype.Text != txt_password.Text)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Retype the same password you have written ','info_warning');", true);
                Isvalid = false;
                return Isvalid;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return Isvalid;
    }
    private bool userAdd_successfull()
    {
        bool IsaddSuccessfull = false;
        string result = "";
        DataSet dset_user_exists = new DataSet();
        try
        {
            dset_user_exists = client.user_exists(txt_username.Text);
            if (dset_user_exists.Tables["dbTable"].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('That username is already in use, try another one ','info_warning');", true);
                return IsaddSuccessfull;
            }
            else
            {
                chat_svc.Users userObj = new chat_svc.Users();
                userObj.username = txt_username.Text;
                userObj.password = txt_password.Text;
                userObj.first_name = txt_first_name.Text;
                userObj.last_name = txt_last_name.Text;
                userObj.gender = cmb_gender.SelectedItem.Text;
                userObj.date_of_birth = dtp_dob.SelectedDate.Value.ToString();
                result = client.insert_userInfo(userObj);
                if(result == "Successful")
                {
                    IsaddSuccessfull = true;
                }
                else
                {
                    IsaddSuccessfull = false;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ErrorBar", "javascript:DisplayInforBar('Error trying to save ','info_warning');", true);
        }
        return IsaddSuccessfull;
    }
    protected void btn_signup_Click(object sender, EventArgs e)
    {
        try
        {
            if (!info_valid())
            {
                return;
            }
            if (!userAdd_successfull())
            {
                return;
            }
            Session["username"] = txt_username.Text;
            Session["first_name"] = txt_first_name.Text;
            Response.Redirect("index.aspx");
            initialize_page();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}