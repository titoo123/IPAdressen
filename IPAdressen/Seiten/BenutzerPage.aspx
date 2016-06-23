<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="BenutzerPage.aspx.cs" Inherits="IPAdressen.Seiten.BenutzerPage" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Benutzer</legend>
        <div style="float: right; width: 45%; right: 0px; margin-right: 2px;">
            <fieldset>
                <legend>Anwendungen</legend>
                <fieldset>
                    <legend>Vorhandene Anwendungen zuordnen</legend>Name:<br />
                    <asp:TextBox ID="TextBox_BenutzerAnwendungenSuchen_Name" runat="server"></asp:TextBox><br />
                  
                    Login:<br />
                    <asp:TextBox ID="TextBox_BenutzerAnwendungenSuchen_Login" runat="server"></asp:TextBox><br />
                    <br />
                    <asp:Button ID="Button_BenutzerAnwendungen_Suchen" runat="server" Text="Suchen" 
                        onclick="Button_BenutzerAnwendungen_Suchen_Click" /><br />
                    <ext:ExtendedGridView ID="GridView_BenutzerAnwendungen_Suchen" runat="server"
                        BorderStyle="None" CellPadding="10" GridLines="Horizontal" 
                        
                        onselectedindexchanged="GridView_BenutzerAnwendungen_Suchen_SelectedIndexChanged" 
                        AutoGenerateSelectButton="True">
                    </ext:ExtendedGridView>
                    <asp:Button ID="Button_BenutzerAnwendungen_Hinzufügen" runat="server" Enabled="false"
                        Text="Hinzufügen" onclick="Button_BenutzerAnwendungen_Hinzufügen_Click" />
                </fieldset>
                <fieldset>
                    <legend>Anwendungen verwalten</legend>
                    <br />
                    <p>
                    <ext:ExtendedGridView ID="GridView_BenutzerAnwendungen" runat="server" 
                        BorderStyle="None" CellPadding="10" GridLines="Horizontal" 
                            onselectedindexchanged="GridView_BenutzerAnwendungen_SelectedIndexChanged" 
                            AutoGenerateSelectButton="True">
                    </ext:ExtendedGridView>
                        Name:<br />
                        <asp:TextBox ID="TextBox_BenutzerAnwendungen_Name" runat="server" Enabled="false"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="Anwendungsnamen_RequiredFieldValidator" ControlToValidate="TextBox_BenutzerAnwendungen_Name" runat="server" ValidationGroup="AnwendungenName_ValidationGroup" ErrorMessage="Bitte geben sie einen Namen für die Anwendung ein!"></asp:RequiredFieldValidator>
                        <br />Login:<br />
                        <asp:TextBox ID="TextBox_BenutzerAnwendungen_Login" runat="server" Enabled="false"></asp:TextBox><br />
                        Passwort:<br />
                        <asp:TextBox ID="TextBox_BenutzerAnwendungen_Passwort" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        
                          
                      
                      <br />
                        <br />




                        <asp:Button ID="Button_BenutzerAnwendungen_Neu" runat="server" Text="Neu" 
                            Enabled="False" onclick="Button_BenutzerAnwendungen_Neu_Click"  />
                        <asp:Button ID="Button_BenutzerAnwendungen_Speichern" runat="server" 
                            Text="Speichern" Enabled="False" ValidationGroup="AnwendungenName_ValidationGroup"
                            onclick="Button_BenutzerAnwendungen_Speichern_Click"  />
                        <asp:Button ID="Button_BenutzerAnwendungen_Bearbeiten" runat="server" 
                            Text="Bearbeiten" Enabled="False" 
                            onclick="Button_BenutzerAnwendungen_Bearbeiten_Click"  />
                        <asp:Button ID="Button_BenutzerAnwendungen_Löschen" runat="server" 
                            Text="Löschen" Enabled="False" 
                            onclick="Button_BenutzerAnwendungen_Löschen_Click" 
                            OnClientClick="if(!confirm('Wirklich löschen?')) return false;"
                            />

                        <br />
                        <br />
                        <asp:Button ID="Button_BenutzerAnwendungen_Details" runat="server" 
                            Text="Details" Enabled="False" 
                            onclick="Button_BenutzerAnwendungen_Details_Click" />
                        <asp:Button ID="Button_BenutzerAnwendungen_Verbindungentfernen" runat="server" Text="Verbindung entfernen"
                            Enabled="False" 
                            onclick="Button_BenutzerAnwendungen_Verbindungentfernen_Click" 
                            OnClientClick="if(!confirm('Wirklich löschen?')) return false;"
                            /></p>
                </fieldset>
            </fieldset>
        </div>
        <div style="width: 45%;">

        <fieldset>
                <legend>Bearbeiten</legend>
                
                 Anrede:<br />
                            <asp:DropDownList ID="DropDownList_Anrede" runat="server" 
                                onselectedindexchanged="DropDownList_Anrede_SelectedIndexChanged" 
                                Enabled="False">
                                <asp:ListItem Selected="True" Value="Herr">Herr</asp:ListItem>
                                <asp:ListItem Value="Frau">Frau</asp:ListItem>
                                <asp:ListItem Value="Firma">Firma</asp:ListItem>
                                <asp:ListItem Value="Abteilung">Abteilung</asp:ListItem>
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList><br /><br />
                
                Name:<br />
                <asp:TextBox ID="TextBox_BenutzerBearbeiten_Nachname" runat="server" Enabled="false"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Nachname" ControlToValidate="TextBox_BenutzerBearbeiten_Nachname" runat="server" ErrorMessage="Bitte geben sie einen Namen ein!" ValidationGroup="BenutzerName_ValidationGroup"></asp:RequiredFieldValidator><br />
                Vorname:<br />
                <asp:TextBox ID="TextBox_BenutzerBearbeiten_Vorname" runat="server" Enabled="false"></asp:TextBox><br /><br />
                

                <asp:Label ID="Label2" runat="server" Text="Internet:  "></asp:Label>  <br />

                            <asp:DropDownList ID="DropDownList_Internet" runat="server" 
                            
                    onselectedindexchanged="DropDownList_Internet_SelectedIndexChanged" 
                    Enabled="False">
                                <asp:ListItem Selected="True" Value="0">Nein</asp:ListItem>
                                <asp:ListItem  Value="L1">L1</asp:ListItem>
                                <asp:ListItem  Value="L2">L2</asp:ListItem>
                                <asp:ListItem  Value="FREI">FREI</asp:ListItem>
                            </asp:DropDownList>

                <br />
                <asp:Button ID="Button_BenutzerBearbeiten_Neu" runat="server" Text="Neu" 
                    onclick="Button_BenutzerBearbeiten_Neu_Click" />
                <asp:Button ID="Button_BenutzerBearbeiten_Speichern" runat="server" 
                    Text="Speichern" Enabled="False" onclick="Button_BenutzerBearbeiten_Speichern_Click" ValidationGroup="BenutzerName_ValidationGroup"
                     />
                <asp:Button ID="Button_BenutzerBearbeiten_Bearbeiten" runat="server" 
                    Text="Bearbeiten" Enabled="False" onclick="Button_BenutzerBearbeiten_Bearbeiten_Click"
                   />
                <asp:Button ID="Button_BenutzerBearbeiten_Löschen" runat="server" 
                    Text="Löschen" Enabled="False" onclick="Button_BenutzerBearbeiten_Löschen_Click"
                    OnClientClick="if(!confirm('Wirklich löschen?')) return false;"
                    />
                
            </fieldset>
        

            <fieldset>
                <legend>Suchen</legend>Nachname:<br />
                <asp:TextBox ID="TextBox_BenutzerSuchen_Nachname" runat="server"></asp:TextBox><br />
                Vorname:<br />
                <asp:TextBox ID="TextBox_BenutzerSuchen_Vorname" runat="server"></asp:TextBox><br />
                <asp:Button ID="Button_Benutzer_Suchen" runat="server" Text="Suchen" 
                    onclick="Button_Benutzer_Suchen_Click" />
                <br />
                <ext:ExtendedGridView ID="GridView_Suchen_Benutzer" runat="server" 
                    BorderStyle="None" CellPadding="10" GridLines="Horizontal" 
                    onselectedindexchanged="GridView_Suchen_Benutzer_SelectedIndexChanged" 
                    AutoGenerateSelectButton="True">
                </ext:ExtendedGridView>
                
            </fieldset>
            </div>
    </fieldset>
    <asp:Label ID="Status" runat="server" Text=""></asp:Label>
</asp:Content>
