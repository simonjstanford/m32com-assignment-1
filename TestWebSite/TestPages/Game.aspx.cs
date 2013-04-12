using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using TetrisWebService;

public partial class TestPages_Game : System.Web.UI.Page
{

    private const string TETRISWEBSERVICESESSIONINDEX = "TetrisWebService";

    private TetrisWebService.TetrisWebServiceSoapClient GetWS()
    {
        ValidateSession();
        return base.Session[TETRISWEBSERVICESESSIONINDEX] as TetrisWebService.TetrisWebServiceSoapClient;
    }

    private void ValidateSession()
    {
        if (base.Session[TETRISWEBSERVICESESSIONINDEX] == null)
        {
            // create the proxy
            TetrisWebService.TetrisWebServiceSoapClient ws = new TetrisWebService.TetrisWebServiceSoapClient();
            // create a container for the SessionID cookie
            //ws.CookieContainer = new CookieContainer();
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
                tc.BorderStyle = BorderStyle.Solid;
                tc.BorderColor = System.Drawing.Color.Black;

                if (String.IsNullOrEmpty(Board[i][j]))
                {
                    tc.Text = Board[i][j];
                }
                else 
                {
                    tc.Text = Board[i][j];
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
                    tc.Text = fred[i][j];
                }
                else 
                {
                    tc.Text = fred[i][j];
                }
                tr.Cells.Add(tc);
            }
            nextShape.Rows.Add(tr);
        }

    }
    protected void btnDownWs_Click(object sender, EventArgs e)
    {
        GetWS().MoveBlockDown();
        btnGetBoardWs_Click(sender, e);
    }
    protected void btnRightWs_Click(object sender, EventArgs e)
    {
        GetWS().MoveBlockRight();
        btnGetBoardWs_Click(sender, e);
    }
    protected void btnLeftWs_Click(object sender, EventArgs e)
    {
        GetWS().MoveBlockLeft();
        btnGetBoardWs_Click(sender, e);
    }
    protected void btnDropWs_Click(object sender, EventArgs e)
    {
        GetWS().DropBlock();
        btnGetBoardWs_Click(sender, e);
    }
    protected void btnRotateWs_Click(object sender, EventArgs e)
    {
        GetWS().RotateBlock();
        btnGetBoardWs_Click(sender, e);
    }
}