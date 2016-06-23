<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AnwendungenPage.aspx.cs" Inherits="IPAdressen.Seiten.AnwendungenPage" %>
<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<fieldset>
    
    <legend>Anwendungen</legend>
   
        <div style="float: right; width: 45%; right: 0px; margin-right: 2px;">

        <fieldset><legend>Bearbeiten</legend>
    <p>                 Name:<br />
                        <asp:TextBox ID="TextBox_Anwendungen_Name" runat="server" Enabled="false"></asp:TextBox><br />
                        Login:<br />
                        <asp:TextBox ID="TextBox_Anwendungen_Login" runat="server" Enabled="false"></asp:TextBox><br />
                        Passwort:<br />
                        <asp:TextBox ID="TextBox_Anwendungen_Passwort" runat="server" Enabled="false"></asp:TextBox><br />
        
         <br />
                        <asp:Button ID="Button_Anwendungen_Neu" runat="server" 
            Text="Neu" onclick="Button_Anwendungen_Neu_Click"  />
                        <asp:Button ID="Button_Anwendungen_Speichern" runat="server" 
            Text="Speichern" Enabled="False" onclick="Button_Anwendungen_Speichern_Click"
                           />
                        <asp:Button ID="Button_Anwendungen_Bearbeiten" runat="server" 
            Text="Bearbeiten" Enabled="False" onclick="Button_Anwendungen_Bearbeiten_Click"
                            />
                        <asp:Button ID="Button_Anwendungen_Löschen" runat="server" 
            Text="Löschen" Enabled="False" onclick="Button_Anwendungen_Löschen_Click" />
                        <br />
                        <asp:Label ID="Label_Anwendungen" runat="server" Text="Label"></asp:Label>
                        <br />
                        </p>
</fieldset>

       
       </div>


       <div style="width: 45%;">

        <fieldset><legend>Suchen</legend>
         Name:<br />
        <asp:TextBox ID="TextBox_AnwendungenSuchen_Name" runat="server"></asp:TextBox>
            <br />
         Login:<br />
        <asp:TextBox ID="TextBox_AnwendungenSuchen_Login" runat="server"></asp:TextBox>
            <br />
            <br />
         <ext:extendedgridview ID="Anwendungen_Suchen_GridView" runat="server" 
                                BorderStyle="None" CellPadding="10" GridLines="Horizontal" 
                AutoGenerateSelectButton="True" 
                onselectedindexchanged="Anwendungen_Suchen_GridView_SelectedIndexChanged">
       </ext:extendedgridview>
       <asp:Button ID="Button_Anwendungen_Suchen" runat="server" Text="Suchen" 
                onclick="Button_Anwendungen_Suchen_Click" /></fieldset>
        
       </div> 
        
        </fieldset>

</asp:Content>
