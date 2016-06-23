<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BereichPage.aspx.cs" Inherits="IPAdressen.BereichPage"  MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <fieldset>
    <legend>Bereiche</legend>

    <div  style="float: right; width: 45%; "><fieldset><legend>Freie IP's</legend>


        <asp:Button ID="Button_Bereich_SuchenIP" runat="server" Text="Suchen" 
            onclick="Button_Bereich_SuchenIP_Click" Enabled="False" />
        <asp:Button ID="Button_neues_Gerät" runat="server" Text="Neues Gerät" 
            Enabled="False" onclick="Button_neues_Gerät_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Anzahl:"></asp:Label><br />
        <asp:TextBox ID="TextBox_Bereich_IP_Anzahl" runat="server" ></asp:TextBox>
        <br />
        <br />

        <ext:ExtendedGridView ID="GridView_Bereich_IPSuche" runat="server" 
            AutoGenerateSelectButton="True" BorderStyle="None" CellPadding="10" 
            GridLines="Horizontal"  OnDataBound="GridView_Bereich_IPSuche_OnDataBound"
            onselectedindexchanged="BereichGridView_ipsuche_SelectedIndexChanged" 
            AllowPaging="True" 
            OnPageIndexChanging="GridView_Bereich_IPSuche_PageIndexChanging" 
            Width="75%" PageSize="30">
            
        </ext:ExtendedGridView>
        <br />
        </fieldset> </div>
    <div  style="width: 45%;"><fieldset><legend>Bearbeiten</legend>
    
    
    <ext:ExtendedGridView ID="BereichGridView" runat="server" 
            AutoGenerateSelectButton="True"  BorderStyle="None" CellPadding="10" GridLines="Horizontal" 
            onselectedindexchanged="BereichGridView_SelectedIndexChanged" OnDataBound="BereichGridView_OnDataBound">
       
     </ext:ExtendedGridView>

        <br />
        
        Name:<br />
        <asp:TextBox ID="TestBox_Bereich_Name" runat="server" Enabled="False"></asp:TextBox><br />
          <asp:RequiredFieldValidator ID="TestBox_Bereich_NameValidator" runat="server" ControlToValidate="TestBox_Bereich_Name"
                    ErrorMessage="Bitte Bereichsnamen eingeben!" 
                    ValidationGroup="ValidationGroup"></asp:RequiredFieldValidator><br />

                    IP Bereich 1<br />
        <asp:TextBox ID="Textbox_Bereich_IP1" runat="server" Enabled="False"></asp:TextBox><br />

        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Textbox_Bereich_IP1"
                    ErrorMessage="Bitte IP für Bereich eingeben!" 
                    ValidationGroup="ValidationGroup"></asp:RequiredFieldValidator><br />

                    IP Bereich 2<br />
        <asp:TextBox ID="Textbox_Bereich_IP2" runat="server" Enabled="False"></asp:TextBox><br />

        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Textbox_Bereich_IP2"
                    ErrorMessage="Bitte IP für Bereich eingeben!" 
                    ValidationGroup="ValidationGroup"></asp:RequiredFieldValidator><br />

        IP Bereich 3<br />
        <asp:TextBox ID="Textbox_Bereich_IP3" runat="server" Enabled="False"></asp:TextBox><br />

        <asp:RequiredFieldValidator ID="Textbox_Bereich_IPValidator" runat="server" ControlToValidate="Textbox_Bereich_IP3"
                    ErrorMessage="Bitte IP für Bereich eingeben!" 
                    ValidationGroup="ValidationGroup"></asp:RequiredFieldValidator>
                   
        <br /><br />
        <asp:Button ID="BereichNeuButton" runat="server" Text="Neu" 
            onclick="BereichNeuButton_Click" /><br />
        <asp:Button ID="BereichSpeichernButton" runat="server" Text="Speichern" 
            onclick="BereichSpeichernButton_Click" ValidationGroup="ValidationGroup" 
            Enabled="False" /><br />
        <asp:Button ID="BereichBearbeitenButton" runat="server" Text="Bearbeiten" 
            onclick="BereichBearbeitenButton_Click" Enabled="False" /><br />
        <asp:Button ID="BereichLöschenButton" runat="server" Text="Löschen" 
            onclick="BereichLöschenButton_Click" Enabled="False" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />
   
        
       
    
    
    </fieldset> 
    </div>
    


    </fieldset>
    
    <asp:Label ID="Status" runat="server" Text=""></asp:Label>
</asp:Content>
