<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="styles/immachat.css" rel="stylesheet" type="text/css" />
    <title>Web Chat login</title>
    <div class="div_header">
        <div id="Information_Bar" runat="server" class="info_hidden" style="z-index: 200001;">
        </div>
    </div>
</head>
<body>
    <form id="frm_login" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="600">
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="rdcblok" runat="server">

            <script type="text/javascript">
                function DisplayInforBar(Msg, MsgType) {

                    var ErrorBar = document.getElementById('<%= Information_Bar.ClientID %>');
                switch (MsgType) {
                    case "info_warning":
                        {
                            ErrorBar.className = "info_warning";
                            break;
                        }
                    case "info_success":
                        {
                            ErrorBar.className = "info_success";
                            break;
                        }
                    case "info_info":
                        {
                            ErrorBar.className = "info_info";
                            break;
                        }
                    case "info_hidden":
                        {
                            ErrorBar.className = "info_hidden";
                            break;
                        }
                }
                ErrorBar.innerHTML = Msg;
                return;
            }
            </script>

        </telerik:RadCodeBlock>
        <div style="width: 100%;">
            <table style="width: 100%; margin-top: 0%">
                <tr>
                    <td align="right">
                        <img src="Images/Immachat_1.jpg" alt="Display Image" />
                    </td>
                    <td class="login">
                        <table style="width: 100%">
                            <tr>
                                <td colspan="3">Provide Login information
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td>Username
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_username" runat="server" Width="60%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>Password
                                </td>
                                <td>
                                    <asp:TextBox ID="txt_password" runat="server" TextMode="Password" Width="60%"></asp:TextBox>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btn_login" runat="server" Text="Login"
                                        Width="60%" OnClick="btn_login_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">Do not have an account?                               
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btn_signup" runat="server" Text="Sign Up"
                                        Width="60%" Height="30px" OnClick="btn_signup_Click" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
