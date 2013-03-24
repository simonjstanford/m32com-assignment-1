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
        
        for (int i = 0; i < Board.Length; i++)
        {
            //each row
            TableRow tr = new TableRow();
            for (int j = 0; j < Board[i].Length; j++)
            { 
                //each coloumn
                TableCell tc = new TableCell();
                if (String.IsNullOrEmpty(Board[i][j]))
                {
                    tc.Text = "0";
                }
                else 
                {
                    tc.Text = "1";
                }
                
                tr.Cells.Add(tc);
            }
            blocks.Rows.Add(tr);
        }
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

    protected void btnGetNextShapeWs_Click(object sender, EventArgs e)
    {
        string[][] fred = GetWS().GetNextShape();

        for (int i = 0; i < fred.Length; i++)
        {
            //each row
            TableRow tr = new TableRow();
            for (int j = 0; j < fred[i].Length; j++)
            { 
                //each coloumn
                TableCell tc = new TableCell();
                if (String.IsNullOrEmpty(fred[i][j]))
                {
                    tc.Text = "0";
                }
                else 
                {
                    tc.Text = "1";
                }
                tr.Cells.Add(tc);
            }
            nextShape.Rows.Add(tr);
        }

    }
    protected void btnDownWs_Click(object sender, EventArgs e)
    {
        GetWS().MoveBlockDown();
    }
    protected void btnRightWs_Click(object sender, EventArgs e)
    {
        GetWS().MoveBlockRight();
    }
    protected void btnLeftWs_Click(object sender, EventArgs e)
    {
        GetWS().MoveBlockLeft();
    }
    protected void btnDropWs_Click(object sender, EventArgs e)
    {
        GetWS().DropBlock();
    }
}