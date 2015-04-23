<%@ Page Language="C#" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="pages_profile"
    MasterPageFile="~/webchat.master" %>

<%@ MasterType VirtualPath="~/webchat.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <h3>Profile</h3>
    <hr />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="Server">
    <br />
    <br />
    <asp:Panel ID="ajax_panel1" runat="server" Style="width: 100%">
        <asp:Panel ID="group_add" runat="server" class="contents_width">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:HiddenField ID="lbl_confirm" runat="server" />
                        <asp:HiddenField ID="lbl_rowindex" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>First Name
                    </td>
                    <td>
                        <asp:Label ID="lbl_first_name" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Last Name
                    </td>
                    <td>
                        <asp:Label ID="lbl_last_name" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Gender
                    </td>
                    <td>
                        <asp:Label ID="lbl_gender" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Date of Birth
                    </td>
                    <td>
                        <asp:Label ID="lbl_dob" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>    
</asp:Content>
