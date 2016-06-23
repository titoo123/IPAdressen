<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suche.aspx.cs" Inherits="IPAdressen.Index" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="ExtendedGridView" Namespace="ExtendedControls" TagPrefix="ext" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        #sTextIPAdresse, #sTextBox_ad1, #sTextBox_ad2, #sTextBox_ad3, #sTextBox_ad4 {
            width: 50px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="SM" runat="server"></asp:ScriptManager>

    <fieldset>
        <legend>Suchparameter</legend>

        <div style="float: right; width: 48%;">
            <fieldset>
                <legend>Person</legend>
                Vorname:<br />
                <asp:TextBox ID="sTextPVorname" runat="server" TextMode="Search"></asp:TextBox><br />
                Nachname:<br />
                <asp:TextBox ID="sTextNachname" runat="server" TextMode="Search"></asp:TextBox><br />
            </fieldset>
            <fieldset>
                <legend>Allgemein</legend>
                <asp:TextBox ID="sTextBoxAllgemein" runat="server" TextMode="Search"></asp:TextBox><br />
                <br />
                <br />
            </fieldset>
        </div>

        <div style="width: 48%;">
            <fieldset>
                <legend>Gerät</legend>

                Name:<br />

                <asp:TextBox ID="sTextName" runat="server" TextMode="Search"></asp:TextBox><br />
                Typ:<br />
                <asp:TextBox ID="sTextTyp" runat="server" TextMode="Search"></asp:TextBox><br />
                Art:<br />
                <asp:TextBox ID="sTextArt" runat="server" TextMode="Search"></asp:TextBox><br />
                Standort:<br />
                <asp:TextBox ID="sTextStandort" runat="server" TextMode="Search"></asp:TextBox><br />
                Alte IP:<br />
                <asp:TextBox ID="sTextAlteIP" runat="server" TextMode="Search"></asp:TextBox><br />
            </fieldset>
        </div>
        <div style="float: right; width: 48%; margin-top: -14px;">
            <fieldset>
                <legend>AS400</legend>
                Workstation ID:<br />
                <asp:TextBox ID="TextBox_DEVRDEPP" runat="server"></asp:TextBox><br />
                <%-- Profil:<br />
        <asp:TextBox ID="TextBox_AS400Profil" runat="server"></asp:TextBox><br /> --%>
                <br />
                <br />
            </fieldset>
            <fieldset>
                <legend>Bereich</legend>
                <p>
                    <asp:DropDownList ID="BereichDropDownList" runat="server">
                    </asp:DropDownList>
                </p>
            </fieldset>
        </div>


        <div style="width: 48%;">
            <fieldset>
                <legend>IP</legend>

                IP-Adresse:<br />

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
                                document.getElementById("<%=sTextBox_ad2.ClientID %>").focus();
                }
                if (n == 3) {
                    document.getElementById("<%=sTextBox_ad3.ClientID %>").focus();
                }
                if (n == 4) {
                    document.getElementById("<%=sTextBox_ad4.ClientID %>").focus();
                }
            }
        }

                </script>

                <asp:TextBox ID="sTextBox_ad1" runat="server" Width="82px" MaxLength="3" onkeyup="moveCursor(this,2)" onkeypress="return isNumberKey(event)"></asp:TextBox>.
                <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="999" TargetControlID="sTextBox_ad1" MaskType="Number" />--%>
                <asp:TextBox ID="sTextBox_ad2" runat="server" Width="82px" MaxLength="3" onkeyup="moveCursor(this,3)" onkeypress="return isNumberKey(event)"></asp:TextBox>.
                <%--<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="999" TargetControlID="sTextBox_ad2" MaskType="Number" />--%>
                <asp:TextBox ID="sTextBox_ad3" runat="server" Width="82px" MaxLength="3" onkeyup="moveCursor(this,4)" onkeypress="return isNumberKey(event)"></asp:TextBox>.
                <%--<cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="999" TargetControlID="sTextBox_ad3" MaskType="Number" />--%>
                <asp:TextBox ID="sTextBox_ad4" runat="server" Width="82px" MaxLength="3" onkeypress="return isNumberKey(event)"></asp:TextBox><br />
                <%--<cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="999" TargetControlID="sTextBox_ad4" MaskType="Number" />--%>

                <script type="text/javascript">

                    function isMAC(a) {
                        var t = document.getElementById("<%=sTextMAC.ClientID %>");

                               if (t.value.length == 2 || t.value.length == 5 || t.value.length == 8 || t.value.length == 11 || t.value.length == 14) {
                                   t.value = t.value + ":";
                               }

                           }


                </script>
                MAC:<br />
                <asp:TextBox ID="sTextMAC" runat="server" Width="371px" MaxLength="17" onkeyup="isMAC(event,this.value);"></asp:TextBox>

                <br />
            </fieldset>
        </div>





        <asp:Button ID="sButton" runat="server" Text="Suchen" OnClick="sButton_Click" />

    </fieldset>

    <fieldset>
        <legend>Übersicht</legend>
        <ext:ExtendedGridView ID="IPGridView" runat="server" OnDataBound="IPGridView_OnDataBound"
            OnSelectedIndexChanged="IPGridView_SelectedIndexChanged"
            BorderStyle="None" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="IPGridView_PageIndexChanging"
            PageSize="50" Width="100%" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle Width="200px" />
            <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="300px" />
        </ext:ExtendedGridView>



        <%-- <asp:Label ID="Zeile" runat="server" Text="Ausgewählte Zeile: "></asp:Label>--%>
    </fieldset>
</asp:Content>
