using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TetrisWebService;

public partial class TestPages_ViewHighScores : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGetHighScoreWs_Click(object sender, EventArgs e)
    {
        TetrisWebService.TetrisWebServiceSoapClient ws = new TetrisWebService.TetrisWebServiceSoapClient();
        DataTable dt = ws.GetHighScores();
    }

    protected void btnGetHighScoreDll_Click(object sender, EventArgs e)
    {
        TetrisHighScores.ScoreController.Instance.setLocation(base.Server.MapPath("~/Scores.xml"));
        DataTable dt = TetrisHighScores.ScoreController.Instance.getScoreTable();
    }
}