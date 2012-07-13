<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MollieSample.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mollie iDeal Sample</title>
    <link rel="stylesheet" type="text/css" href="MollieCss.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnGo").hide();
            $("input[name=banks]").click(function () {
                $("#hf").val($(this).attr("id"));
                $("#btnGo").fadeIn();
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField runat="server" id="hf"/>
        <div style="float:left;">
        Selecteer uw bank:</div>
        <div style="float:left;">
            <asp:Repeater runat="server" id="repBanks">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <ItemTemplate>
                    <li><label><input type="radio" name="banks" id='<%# Eval("Id") %>' /><%# Eval("Name") %></label></li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>
        <div style="float:left;">
            <asp:button id="btnGo" runat="server" Text="Afrekenen" ClientIDMode="Static" OnClick="btnGo_OnClick"/>
        </div>
    </div>
        
    </form>
</body>
</html>
