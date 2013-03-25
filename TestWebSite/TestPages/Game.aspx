<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Game.aspx.cs" Inherits="TestPages_Game" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Name:
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox><br />
            <asp:Button ID="btnStartGameWs" Text="Start Game (Webservice)" runat="server" OnClick="btnStartGameWs_Click" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnGetNextShapeWs" Text="Get Next Shape (Webservice)" runat="server" OnClick="btnGetNextShapeWs_Click" />
            <asp:Table ID="nextShape" runat="server" BorderColor="Black" BorderStyle="Solid"></asp:Table>
            <br />
            <br />
            <br />
            <asp:Button ID="btnGetScoreWs" Text="Get Score (Webservice)" runat="server" OnClick="btnGetScoreWs_Click" />
            <span id="score" runat="server"></span>
            <br />
            <br />
            <br />
            <asp:Button ID="btnSubmitHighScoreWs" Text="Submit High Score (Webservice)" runat="server" OnClick="btnSubmitHighScoreWs_Click" />
            <br />
            <br />
            <br />
            <asp:Button ID="btnCountTestWs" Text="Count Session Test (Webservice)" runat="server" OnClick="btnCountTestWs_Click" />
            <span id="countTest" runat="server"></span>
            <br />
            <br />
            <br />
            <asp:Button ID="btnDownWs" Text="Down (Webservice)" runat="server" OnClick="btnDownWs_Click" />
            <asp:Button ID="btnRightWs" Text="Right (Webservice)" runat="server" OnClick="btnRightWs_Click" />
            <asp:Button ID="btnLeftWs" Text="Left (Webservice)" runat="server" OnClick="btnLeftWs_Click" />
            <asp:Button ID="btnDropWs" Text="Drop (Webservice)" runat="server" OnClick="btnDropWs_Click" />
            <asp:Button ID="btnRotateWs" Text="Rotate (Webservice)" runat="server" OnClick="btnRotateWs_Click" />
            <asp:Button ID="btnGetBoardWs" Text="Get Board (Webservice)" runat="server" OnClick="btnGetBoardWs_Click" />
            <asp:Table ID="blocks" runat="server" BorderColor="Black" BorderStyle="Solid"></asp:Table>
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
