<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    CodeBehind="GerätePage.aspx.cs" Inherits="IPAdressen.Seiten.GerätePage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        input {
            width: 350px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>

    <fieldset>
        <legend>Übersicht</legend>
        <asp:Button ID="Button_LetzteSuche" runat="server" Text="Letzte Suche"
            OnClick="Button_LetzteSuche_Click" />
        <div id="gerät">
            <fieldset>
                <legend>Gerät</legend>
                <div style="width: 33%; float: right;">
                    VNC Passwort:<br />
                    <asp:TextBox ID="TextBox_Gerät_VNCPasswort" runat="server" Enabled="False"></asp:TextBox><br />
                    <br />
                    Kommentar:<br />
                    <asp:TextBox ID="TextBox_Gerät_Kommentar" runat="server" TextMode="MultiLine"
                        Enabled="False" Height="22px" Width="350px"></asp:TextBox>
                    <br />
                    <br />
                    Bereich:<br />
                    <asp:DropDownList ID="DropDownList_Gerät_Bereich" runat="server" 
                        DataTextField="Name"
                        DataValueField="Name"
                        Enabled="False" Height="23px" Width="354px">
                    </asp:DropDownList>
                    <br />
                    <asp:RequiredFieldValidator ID="DDLRequiredFieldValidator" runat="server" ControlToValidate="DropDownList_Gerät_Bereich"
                        ErrorMessage="Bitte wählen sie einen Bereich!" ValidationGroup="GValidationGroup"></asp:RequiredFieldValidator>
                    <br />
                </div>
                <div style="width: 33%; float: right;">
                    Standort:<br />
                    <asp:TextBox ID="TextBox_Gerät_Standort" runat="server" Enabled="False"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="StandortRequiredFieldValidator" runat="server" ControlToValidate="TextBox_Gerät_Standort"
                        ErrorMessage="Bitte geben sie den Standort des Gerätes ein!" ValidationGroup="GValidationGroup"></asp:RequiredFieldValidator><br />
                    Alte IP-Adresse:<br />
                    <asp:TextBox ID="TextBox_Gerät_AlteIP" runat="server" Enabled="False"></asp:TextBox><br />
                    <br />
                    VNC Port:<br />
                    <asp:TextBox ID="TextBox_Gerät_VNCPort" runat="server" Enabled="False"></asp:TextBox><br />
                </div>
                <div style="width: 33%">
                    Name:<br />
                    <asp:TextBox ID="TextBox_Gerät_Name" runat="server" Enabled="False"> </asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ControlToValidate="TextBox_Gerät_Name"
                        ErrorMessage="Bitte geben sie einen Namen für das Gerät ein!" ValidationGroup="GValidationGroup"></asp:RequiredFieldValidator>
                    <br />
                    Typ:<br />
                    <asp:TextBox ID="TextBox_Gerät_Typ" runat="server" Enabled="False"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="TypRequiredFieldValidator" runat="server" ControlToValidate="TextBox_Gerät_Typ"
                        ErrorMessage="Bitte geben sie den Typ des Gerätes ein!" ValidationGroup="GValidationGroup"></asp:RequiredFieldValidator>
                    <br />
                    Art:
                    <br />
                    <asp:TextBox ID="TextBox_Gerät_Art" runat="server" Enabled="False"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="ArtRequiredFieldValidator" runat="server" ControlToValidate="TextBox_Gerät_Art"
                        ErrorMessage="Bitte geben sie die Art des Gerätes ein!" ValidationGroup="GValidationGroup"></asp:RequiredFieldValidator><br />
                </div>
                <br />
                <br />
                <br />
                <asp:Button ID="Button_Gerät_Neu" runat="server" Text="Neu"
                    OnClick="Button_Gerät_Neu_Click" Enabled="False" /><br />
                <asp:Button ID="Button_Gerät_Speichern" runat="server" OnClick="Button_Gerät_Speichern_Click"
                    ValidationGroup="GValidationGroup" Enabled="False" Text="Speichern" /><br />
                <asp:Button ID="Button_Gerät_Bearbeiten" runat="server" Enabled="False" OnClick="Button_Gerät_Bearbeiten_Click"
                    Text="Bearbeiten" /><br />
                <asp:Button ID="Button_Gerät_Löschen" runat="server" Text="Löschen" Enabled="False" OnClientClick="if(!confirm('Wirklich löschen?')) return false;"
                    OnClick="Button_Gerät_Löschen_Click" /><br />
                <br />


            </fieldset>
        </div>

        <div>
            <fieldset>
                <legend>Adressen</legend>
                <div style="width: 45%; float: right">
                    <ext:ExtendedGridView ID="GridView_IPAdressen" runat="server"
                        OnSelectedIndexChanged="GridView_IPAdressen_SelectedIndexChanged" BorderStyle="None" OnDataBound="GridView_IPAdressen_OnDataBound"
                        CellPadding="10" GridLines="Horizontal">
                    </ext:ExtendedGridView>
                </div>



                <div class="input">

                    <script type="text/javascript">

                        function isNumberKey(evt) {
                            var charCode = (evt.which) ? evt.which : evt.keyCode;
                            if (charCode != 46 && charCode > 31
                              && (charCode < 48 || charCode > 57))
                                return false;

                            return true;
                        }

                        function moveCursor(fromTextBox, n) {
                            var length = fromTextBox.value.length;
                            var maxLength = fromTextBox.getAttribute("maxLength");

                            if (length == maxLength) {

                                if (n == 2) {
                                    document.getElementById("<%=TextBox_IPAdressen_ad2.ClientID %>").focus();
                }
                if (n == 3) {
                    document.getElementById("<%=TextBox_IPAdressen_ad3.ClientID %>").focus();
                }
                if (n == 4) {
                    document.getElementById("<%=TextBox_IPAdressen_ad4.ClientID %>").focus();
                }
            }
        }

                    </script>
                    IP-Adresse
                    <br />
                    <asp:TextBox ID="TextBox_IPAdressen_ad1" runat="server" MaxLength="3" onkeyup="moveCursor(this,2)" onkeypress="return isNumberKey(event)" 
                        Width="78px" Enabled="False"></asp:TextBox>.


                    <asp:TextBox ID="TextBox_IPAdressen_ad2" runat="server" MaxLength="3" onkeyup="moveCursor(this,3)" onkeypress="return isNumberKey(event)"
                        Width="78px" Enabled="False"></asp:TextBox>.


                    <asp:TextBox ID="TextBox_IPAdressen_ad3" runat="server" MaxLength="3" onkeyup="moveCursor(this,4)" onkeypress="return isNumberKey(event)"
                        Width="78px" Enabled="False"></asp:TextBox>.

                    <asp:TextBox ID="TextBox_IPAdressen_ad4" runat="server" MaxLength="3" onkeypress="return isNumberKey(event)" 
                        Width="78px" Enabled="False"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text=" / " Font-Size="Larger" BorderColor="White" BorderWidth="10px"></asp:Label>
                    <asp:TextBox ID="TextBox_IPAdressen_ad5" runat="server" MaxLength="3" onkeypress="return isNumberKey(event)" 
                        Width="78px" Enabled="False"></asp:TextBox>
                    <br />

                   
                    <script type="text/javascript">

                           function isMAC(a) {
                               var t = document.getElementById("<%=TextBox_IPAdressen_MAC.ClientID %>");
                            
                               if (t.value.length == 2 || t.value.length == 5 || t.value.length == 8 || t.value.length == 11 || t.value.length == 14 ) {
                                   t.value = t.value + ":";
                               }
                          
                        }
                 

                    </script>

                    MAC-Adresse:<br />
                    <asp:TextBox ID="TextBox_IPAdressen_MAC" runat="server" Enabled="false" MaxLength="17" onkeyup="isMAC(event,this.value);"></asp:TextBox><br />
                   <%-- <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="TextBox_IPAdressen_MAC" Filtered="ABCDEFabcdef0123456789:-" />--%>
                    
                    
                    
                    
                    <br />
                    <asp:Button ID="Button_IPAdressen_Neu" runat="server" Text="Neu" Enabled="False"
                        OnClick="Button_IPAdresse_Neu_Click" Height="28px" /><br />
                    <asp:Button ID="Button_IPAdressen_Speichern" runat="server" Text="Speichern" Enabled="False" ValidationGroup="IPValidationGroup"
                        OnClick="Button_IPAdressen_Speichern_Click" /><br />
                    <asp:Button ID="Button_IPAdressen_Bearbeiten" runat="server" Text="Bearbeiten" OnClick="Button_IPAdressen_Bearbeiten_Click"
                        Enabled="False" /><br />
                    <asp:Button ID="Button_IPAdressen_Löschen" runat="server" Text="Löschen" OnClick="Button_IPAdressen_Löschen_Click"
                        Enabled="False" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />
                    <br />
                    <br />
                    <asp:Button ID="Button_Geräte_IPAdressen_AS400" runat="server" Enabled="False"
                        OnClick="Button_Geräte_IPAdressen_AS400_Click" Text="AS400" />
                    <br />
                </div>

            </fieldset>
        </div>


        <div>
            <fieldset>
                <legend>Benutzer</legend>
                <div style="width: 45%; float: right;">
                    <fieldset>
                        <legend>Benutzer verwalten</legend>
                        <ext:ExtendedGridView ID="GridView_Benutzer_BenutzerVerwalten" runat="server"
                            OnSelectedIndexChanged="GridView_Benutzer_BenutzerVerwalten_SelectedIndexChanged" OnDataBound="GridView_Benutzer_BenutzerVerwalten_OnDataBound"
                            BorderStyle="None" CellPadding="10" GridLines="Horizontal">
                        </ext:ExtendedGridView>
                        <div class="input">
                            Anrede:<br />
                            <asp:DropDownList ID="DropDownList_Anrede" runat="server"
                                OnSelectedIndexChanged="DropDownList_Anrede_SelectedIndexChanged"
                                Enabled="False" Height="23px" Width="354px">
                                <asp:ListItem Selected="True" Value="Herr">Herr</asp:ListItem>
                                <asp:ListItem Value="Frau">Frau</asp:ListItem>
                                <asp:ListItem Value="Firma">Firma</asp:ListItem>
                                <asp:ListItem Value="Abteilung">Abteilung</asp:ListItem>
                                <asp:ListItem Value=" "></asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            Name:<br />
                            <asp:TextBox ID="TextBox_BenutzerVerwalten_Nachname" runat="server" Enabled="false"></asp:TextBox><br />
                            Vorname:<br />
                            <asp:TextBox ID="TextBox_BenutzerVerwalten_Vorname" runat="server" Enabled="false"></asp:TextBox><br />
                            <asp:Label ID="Label1" runat="server" Text="Ansprechpartner  "></asp:Label>
                            <br />
                            <asp:DropDownList ID="DropDownList_IstAnsprechpartner" runat="server" Height="23px" Width="354px"
                                OnSelectedIndexChanged="DropDownList_IstAnsprechpartner_SelectedIndexChanged"
                                Enabled="False">
                                <asp:ListItem Value="true">Ja</asp:ListItem>
                                <asp:ListItem Selected="True" Value="false">Nein</asp:ListItem>
                            </asp:DropDownList><br />

                            <asp:Label ID="Label2" runat="server" Text="Internet:  "></asp:Label>
                            <br />

                            <asp:DropDownList ID="DropDownList_Internet" runat="server" Height="23px" Width="354px"
                                OnSelectedIndexChanged="DropDownList_Internet_SelectedIndexChanged"
                                Enabled="False">
                                <asp:ListItem Selected="True" Value="Nein">Nein</asp:ListItem>
                                <asp:ListItem Value="L1">L1</asp:ListItem>
                                <asp:ListItem Value="L2">L2</asp:ListItem>
                                <asp:ListItem Value="FREI">FREI</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Button ID="Button_BenutzerVerwalten_Neu" runat="server" Text="Neu" Enabled="False"
                                OnClick="Button_BenutzerVerwalten_Neu_Click" />
                            <asp:Button ID="Button_BenutzerVerwalten_Speichern" runat="server" Text="Speichern"
                                Enabled="False" OnClick="Button_BenutzerVerwalten_Speichern_Click" />
                            <asp:Button ID="Button_BenutzerVerwalten_Bearbeiten" runat="server" Text="Bearbeiten"
                                Enabled="False" OnClick="Button_BenutzerVerwalten_Bearbeiten_Click" />
                            <asp:Button ID="Button_BenutzerVerwalten_Löschen" runat="server" Text="Benutzer löschen" Enabled="False"
                                OnClick="Button_BenutzerVerwalten_Löschen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />
                            <br />
                            <br />
                            <asp:Button ID="Button_BenutzerVerwalten_Details" runat="server" Text="Details"
                                Enabled="False" OnClick="Button_BenutzerVerwalten_Details_Click" />
                            <asp:Button ID="Button_BenutzerVerwaltung_VerbindungEntfernen" runat="server" Text="Verbindung löschen"
                                Enabled="False" OnClick="Button_BenutzerVerwaltung_VerbindungEntfernen_Click" OnClientClick="if(!confirm('Wirklich löschen?')) return false;" />
                            <br />
                        </div>
                        &nbsp;<fieldset>
                            <legend>Anwendungen</legend>
                            <ext:ExtendedGridView ID="GridView_Anwendungen" runat="server" OnSelectedIndexChanged="GridView_Anwendungen_SelectedIndexChanged"
                                BorderStyle="None" CellPadding="10" GridLines="Horizontal">
                            </ext:ExtendedGridView>
                        </fieldset>
                </div>
                <div style="width: 45%">
                    <fieldset>
                        <legend>Benutzer zuordnen</legend>Nachname:<br />
                        <asp:TextBox ID="TextBox_BenutzerSuchen_Nachname" runat="server" Enabled="false"></asp:TextBox><br />
                        Vorname:<br />
                        <asp:TextBox ID="TextBox_BenutzerSuchen_Vorname" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                        <asp:Button ID="Button_BenutzerSuchen_Suchen" runat="server" Text="Suchen" Enabled="false"
                            OnClick="Button_BenutzerSuchen_Suchen_Click" />
                        <br />
                        <asp:Button ID="Button_Benutzersuchen_Hinzufügen" runat="server" Text="Hinzufügen"
                            Enabled="false" OnClick="Button_Benutzersuchen_Hinzufügen_Click" />
                        <br />
                        <ext:ExtendedGridView ID="GridView_BenutzerSuchen" runat="server"
                            OnSelectedIndexChanged="GridView_BenutzerSuchen_SelectedIndexChanged"
                            BorderStyle="None" OnDataBound="GridView_BenutzerSuchen_OnDataBound" OnPageIndexChanging="GridView_BenutzerSuchen_PageIndexChanging"
                            CellPadding="10" GridLines="Horizontal" AllowPaging="True">
                        </ext:ExtendedGridView>

                    </fieldset>
                </div>
            </fieldset>
        </div>

    </fieldset>
    <asp:Label ID="Status" runat="server" Text=""></asp:Label>
</asp:Content>
