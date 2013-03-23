using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class TestPages_Game : System.Web.UI.Page
{

    private const string TETRISWEBSERVICESESSIONINDEX = "TetrisWebService";

    private TetrisWebService.TetrisWebService GetWS()
    {
        ValidateSession();
        return base.Session[TETRISWEBSERVICESESSIONINDEX] as TetrisWebService.TetrisWebService;
    }

    private void ValidateSession()
    {
        if (base.Session[TETRISWEBSERVICESESSIONINDEX] == null)
        {
            // create the proxy
            TetrisWebService.TetrisWebService ws = new TetrisWebService.TetrisWebService();
            // create a container for the SessionID cookie
            ws.CookieContainer = new CookieContainer();
            //Add to session
            base.Session[TETRISWEBSERVICESESSIONINDEX] = ws;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnStartGameWs_Click(object sender, EventArgs e)
    {
        GetWS().StartGame(txtName.Text);
    }

    protected void btnGetBoardWs_Click(object sender, EventArgs e)
    {
        String[][] Board = GetWS().GetBoard();
    }

    protected void btnGetScoreWs_Click(object sender, EventArgs e)
    {
        int iScore = GetWS().GetScore();
        score.InnerText = iScore.ToString();
    }

    protected void btnSubmitHighScoreWs_Click(object sender, EventArgs e)
    {
        GetWS().SubmitScore();
    }

    protected void btnCountTestWs_Click(object sender, EventArgs e)
    {
       String iCount =  GetWS().HelloWorld();
       countTest.InnerText = iCount;
    }

    

}