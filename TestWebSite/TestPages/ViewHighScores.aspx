<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewHighScores.aspx.cs" Inherits="TestPages_ViewHighScores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

            <asp:Button ID="btnGetHighScoreWs" Text="GetHighScore (Webservice)" runat="server" 
            onclick="btnGetHighScoreWs_Click" />
            <br />
                    <asp:Button ID="btnGetHighScoreDll" Text="GetHighScore (DLL)" runat="server" 
            onclick="btnGetHighScoreDll_Click" />

    <div id="Scores" runat="server">
    </div>
    </form>
</body>
</html>
