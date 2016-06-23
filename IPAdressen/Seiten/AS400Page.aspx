<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  MaintainScrollPositionOnPostback="true"
    CodeBehind="AS400Page.aspx.cs" Inherits="IPAdressen.AS400Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>
    <div style="width: 100%;">
        <fieldset>
            <legend>AS400</legend>
            <asp:Button ID="Button_zum_Geraet" runat="server" Text="Zum Gerät" 
                Enabled="False" onclick="Button_zum_Geraet_Click" />
           <fieldset><legend>IP</legend>

IP-Adresse:<br />
    
<asp:TextBox ID="TextBox_ad1" runat="server" TextMode="Search" Width="82px" MaxLength="3" Enabled="False" ></asp:TextBox>.
<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="999" TargetControlID="TextBox_ad1" MaskType="Number" />

<asp:TextBox ID="TextBox_ad2" runat="server" TextMode="Search" Width="82px" MaxLength="3"  Enabled="False" ></asp:TextBox>.
<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="999" TargetControlID="TextBox_ad2" MaskType="Number" />

<asp:TextBox ID="TextBox_ad3" runat="server" TextMode="Search" Width="82px" MaxLength="3"  Enabled="False" ></asp:TextBox>.
<cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="999" TargetControlID="TextBox_ad3" MaskType="Number" />

<asp:TextBox ID="TextBox_ad4" runat="server" TextMode="Search" Width="82px" MaxLength="3"  Enabled="False" ></asp:TextBox><br />
<cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="999" TargetControlID="TextBox_ad4" MaskType="Number" />
MAC:<br />
<asp:TextBox ID="TextMAC" runat="server" TextMode="Search"  Enabled="False" ></asp:TextBox>
<cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="CC\:CC\:CC\:CC\:CC\:CC" TargetControlID="TextMAC" Filtered="ABCDEFabcdef0123456789"/>
                 <br />
</fieldset>
            <div>
                <fieldset>
                    <legend>DEVR</legend>
                    <div>
                       <%-- <fieldset>
                            <legend>Geräte zuordnen</legend>Kennung:<br />
                            <asp:TextBox ID="TextBox_AS400Suchen_Kennung" runat="server" Enabled="false"></asp:TextBox><br />
                            <br />
                            <asp:Button ID="Button_AS400_Suchen" runat="server" Text="Suchen"
                                OnClick="Button_AS400_Suchen_Click" Enabled="False" />
                            <br />
                            <ext:ExtendedGridView ID="GridView_AS400_Suchen" runat="server" AutoGenerateSelectButton="True"
                                BorderStyle="None" CellPadding="10" GridLines="Horizontal" 
                                onselectedindexchanged="GridView_AS400_Suchen_SelectedIndexChanged">
                            </ext:ExtendedGridView>
                            <asp:Button ID="Button_AS400Suchen_Hinzufügen" runat="server" Text="Hinzufügen" Enabled="false"
                                OnClick="Button_AS400Suchen_Hinzufügen_Click" />
                        </fieldset>--%>
                        <fieldset>
                            <legend>DEVR verwalten</legend>
                            
                            <ext:ExtendedGridView ID="GridView_AS400_Verwalten" runat="server"
                                BorderStyle="None" CellPadding="10" GridLines="Horizontal" OnRowCreated="GridView_AS400_Verwalten_RowCreated"
                                onselectedindexchanged="GridView_AS400_Verwalten_SelectedIndexChanged" OnDataBound="GridView_AS400_Verwalten_DataBound" >
                            </ext:ExtendedGridView>

                            Kennung:<br />
                            <asp:TextBox ID="TextBox_AS400_Kennung" runat="server" Enabled="false"></asp:TextBox>
                            <br />
                            <asp:Button ID="Button1" runat="server" Text="B Sitzung hinzufügen" Enabled="False" />
                            <br />
                            <br />
                            <asp:Button ID="Button_AS400_Neu" runat="server" Text="Neu" Enabled="False" OnClick="Button_AS400_Neu_Click" /><br />
                            <asp:Button ID="Button_AS400_Speichern" runat="server" Text="Speichern" Enabled="False"
                                OnClick="Button_AS400_Speichern_Click" /><br />
                            <asp:Button ID="Button_AS400_Bearbeiten" runat="server" Text="Bearbeiten" Enabled="False"
                                OnClick="Button_AS400_Bearbeiten_Click" /><br />
                            <asp:Button ID="Button_AS400_Löschen" runat="server" Text="Löschen" Enabled="False"
                                OnClick="Button_AS400_Löschen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" /><br />
                        </fieldset>
                    </div>
                </fieldset>
                <fieldset>
                    <legend>Profilgend>
                    <fieldset>
                        <legend>Profil zuordnen</legend>
                        Profil:<br />
                        <asp:TextBox ID="TextBox_AS400BenutzerSuchen_Kennung" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        <asp:Button ID="Button_AS400Benutzer_Suchen" runat="server" Text="Suchen" 
                            OnClick="Button_AS400Benutzer_Suchen_Click" Enabled="False" />
                        <br />
                        <ext:ExtendedGridView ID="GridView_AS400_Benutzer_Suchen" runat="server"
                            BorderStyle="None" CellPadding="10" CssClass="Grid" GridLines="Horizontal" OnPageIndexChanging="GridView_AS400_Benutzer_Suchen_OnPageIndexChanging"
       OnRowCreated="GridView_AS400_Benutzer_Suchen_RowCreated" OnDataBound="GridView_AS400_Benutzer_Suchen_DataBound"
                            OnSelectedIndexChanged="GridView_AS400_Benutzer_Suchen_SelectedIndexChanged" 
                            AllowPaging="True" PageSize="3">
                        </ext:ExtendedGridView>
                        <asp:Button ID="Button_AS400BenutzerSuchen_Hinzufügen" runat="server" Text="Hinzufügen" Enabled="false" OnClick="Button_AS400BenutzerSuchen_Hinzufügen_Click" />
                    </fieldset>
                    <fieldset>
                        <legend>Profil verwalten</legend>
                        <div class="input">
                            <ext:ExtendedGridView ID="GridView_AS400_Benutzer" runat="server" BorderStyle="None" CellPadding="10" 
                                GridLines="Horizontal"  OnRowCreated="GridView_AS400_Benutzer_RowCreated" OnDataBound="GridView_AS400_Benutzer_DataBound"
                                onselectedindexchanged="GridView_AS400_Benutzer_SelectedIndexChanged">
                            </ext:ExtendedGridView>
                            <br />
                            Profil:<br />
                            <asp:TextBox ID="TextBox_AS400Benutzer_Kennung" runat="server" Enabled="false"></asp:TextBox><br />
                            Kennwort:<br />
                            <asp:TextBox ID="TextBox_AS400Benutzer_Kennwort" runat="server" Enabled="false"></asp:TextBox><br />
                            Bemerkung:<br />
                            <asp:TextBox ID="TextBox_AS400Benutzer_Bemerkung" runat="server" Enabled="false"></asp:TextBox><br />
                            <br />
                            <asp:Button ID="Button_AS400Benutzer_Neu" runat="server" Text="Neu" Enabled="False" OnClick="Button_AS400Benutzer_Neu_Click" /><br />
                            <asp:Button ID="Button_AS400Benutzer_Speichern" runat="server" Text="Speichern" Enabled="False"
                                OnClick="Button_AS400Benutzer_Speichern_Click" /><br />
                            <asp:Button ID="Button_AS400Benutzer_Bearbeiten" runat="server" Text="Bearbeiten" Enabled="False"
                                OnClick="Button_AS400Benutzer_Bearbeiten_Click" /><br />
                            <asp:Button ID="Button_AS400Benutzer_Löschen" runat="server" Text="Löschen" Enabled="False"
                                OnClick="Button_AS400Benutzer_Löschen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" /><br />
                            <br />
                            <asp:Button ID="Button_AS400Benutzer_VerbindungEntfernen" runat="server" Text="Verbindung entfernen"
                                Enabled="False" OnClick="Button_AS400Benutzer_VerbindungEntfernen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />
                        </div>
                    </fieldset>
                </fieldset>
            </div>
        </fieldset>
    </div>
</asp:Content>
