<%@ Page Title="Chat Home" Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="pages_index"
    MasterPageFile="~/webchat.master" %>

<%@ MasterType VirtualPath="~/webchat.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <h3>Welcome to Web Messenger</h3>
    <hr />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="main_content" runat="Server">
</asp:Content>
