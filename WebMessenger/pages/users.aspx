<%@ Page Language="C#" AutoEventWireup="true" CodeFile="users.aspx.cs" Inherits="pages_friends"
    MasterPageFile="~/webchat.master" %>

<%@ MasterType VirtualPath="~/webchat.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <h3>Find New Friends</h3>
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
                    <td colspan="2">
                        <asp:HiddenField ID="lbl_confirm" runat="server" />
                        <asp:HiddenField ID="lbl_rowindex" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="group_users" runat="server" class="contents_width">
            <telerik:RadGrid ID="grid_users" runat="server" AutoGenerateColumns="false" Skin="Forest"
                OnSelectedIndexChanged="grid_users_SelectedIndexChanged">
                <MasterTableView>
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="first_name" DataField="first_name" HeaderText="First Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="last_name" DataField="last_name" HeaderText="Last Name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="gender" DataField="gender" HeaderText="Gender">
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
