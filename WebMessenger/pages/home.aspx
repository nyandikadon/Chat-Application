<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="pages_home"
    MasterPageFile="~/webchat.master" %>

<%@ MasterType VirtualPath="~/webchat.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <h3>Home Page</h3>
    <hr />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="Server">
    <br />
    <br />
    <div class="div_header">
        <div id="Information_Bar" runat="server" class="info_hidden" style="z-index: 200001;">
        </div>
    </div>
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
    <asp:Panel ID="ajax_panel1" runat="server" Style="width: 100%">
        <asp:Panel ID="group_add" runat="server" class="contents_width">
            <table style="width: 100%">
                <tr>
                    <td colspan="4">
                        <asp:HiddenField ID="lbl_confirm" runat="server" />
                        <asp:HiddenField ID="lbl_rowindex" runat="server" />
                    </td>
                </tr>
                <tr id="tr_request" runat="server">
                    <td>
                        You have a friend request from: 
                    </td>
                    <td>
                        <asp:Label ID="lbl_username" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btn_accept" Text="Accept" runat="server" OnClick="btn_accept_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btn_reject" Text="Reject" runat="server" OnClick="btn_reject_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="description" runat="server" class="contents_width">
            <table style="width: 100%">
                <tr>
                    <td colspan="2">
                        <asp:Literal ID="friends" runat="server" Text="See who of your friends is online below:" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="group_friends" runat="server" class="contents_width">
            <telerik:RadGrid ID="grid_friends" runat="server" AutoGenerateColumns="false" Skin="Forest"
                OnSelectedIndexChanged="grid_friends_SelectedIndexChanged">
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="username" DataField="username" HeaderText="Username">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="first_name" DataField="first_name" HeaderText="First Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="last_name" DataField="last_name" HeaderText="Last Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="status" DataField="status" HeaderText="Status">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <SelectedItemStyle />
                <ClientSettings EnablePostBackOnRowClick="True">
                    <Selecting AllowRowSelect="true" />
                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                </ClientSettings>
            </telerik:RadGrid>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
