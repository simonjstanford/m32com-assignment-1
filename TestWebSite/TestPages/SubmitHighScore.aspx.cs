using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestPages_SubmitHighScore : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmitHighScoreDll_Click(object sender, EventArgs e)
    {
        TetrisHighScores.ScoreController.Instance.setLocation(base.Server.MapPath("~/Scores.xml"));
        TetrisHighScores.ScoreController.Instance.Add("Rob", 100);   
    }

}