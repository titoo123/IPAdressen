<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="IPAdressen.Account.User" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<ext:ExtendedGridView ID="GridView_User" runat="server" AutoGenerateSelectButton="True"
                                BorderStyle="None" CellPadding="10" GridLines="Horizontal" 
                                onselectedindexchanged="GridView_User_SelectedIndexChanged">
                            </ext:ExtendedGridView>
    <br />
    <asp:Label ID="Label_Name" runat="server" Text="Name: " 
        style="font-weight: 700"></asp:Label><br /><br />
    <asp:DropDownList ID="DropDownList_Rechte" runat="server" Enabled="False" 
        onselectedindexchanged="DropDownList_Rechte_SelectedIndexChanged" 
        Height="24px" Width="250px">
        <asp:ListItem>Admin</asp:ListItem>
        <asp:ListItem Value="Schreiben"></asp:ListItem>
        <asp:ListItem>Lesen</asp:ListItem>
        <asp:ListItem Selected="True"></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:DropDownList ID="DropDownList_Distrikt" runat="server" Enabled="False" 
        Height="24px" 
        onselectedindexchanged="DropDownList_Distrikt_SelectedIndexChanged" 
        Width="250px">
        <asp:ListItem></asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
                            <asp:Button ID="Button_Speichern" runat="server" Text="Speichern" Enabled="False"
                                OnClick="Button_Speichern_Click" /><br />
                            <asp:Button ID="Button_Bearbeiten" runat="server" Text="Bearbeiten" Enabled="False"
                                OnClick="Button_Bearbeiten_Click" /><br />
                            <asp:Button ID="Button_Löschen" runat="server" Text="Löschen" Enabled="False"
                                OnClick="Button_Löschen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" /><br />

</asp:Content>
