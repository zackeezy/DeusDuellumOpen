﻿using UnityEngine;
using System.Collections;

public class Token : MonoBehaviour
{
    //CHANGE TO WORK WITH GAME CORE...

    public int currentX;
    public int currentY;
    public bool isWhite;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //they clicked on the token!
            BoardManager.Instance.TokenClicked(currentX, currentY, this);
        }
    }

    public void SetPosition(int x, int y)
    {
        currentX = x;
        currentY = y;
    }

    //public bool[,] PossibleMove()
    //{
    //    bool[,] r = new bool[8, 8];
    //    Token c;

    //    //White team move
    //    if (isWhite)
    //    {
    //        //Diagonal Left
    //        if (currentX != 0 && currentY != 7)
    //        {
    //            c = BoardManager.Instance.Tokens[currentX - 1, currentY + 1];
    //            //if c!=null
    //            if (c != null && !c.isWhite)
    //            {
    //                r[currentX - 1, currentY + 1] = true;
    //            }
    //            else if (c == null)
    //            {
    //                r[currentX - 1, currentY + 1] = true;
    //            }
    //        }
    //        //Diagonal right
    //        if (currentX != 7 && currentY != 7)
    //        {
    //            c = BoardManager.Instance.Tokens[currentX + 1, currentY + 1];
    //            //if c!=null CHECK THIS LATER DAWG
    //            if (c != null && !c.isWhite)
    //            {
    //                r[currentX + 1, currentY + 1] = true;
    //            }
    //            else if (c == null)
    //            {
    //                r[currentX + 1, currentY + 1] = true;
    //            }
    //        }
    //        //Middle
    //        if (currentY != 7)
    //        {
    //            c = BoardManager.Instance.Tokens[currentX, currentY + 1];
    //            if (c == null)
    //            {
    //                r[currentX, currentY + 1] = true;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        //Diagonal Left
    //        if (currentX != 0 && currentY != 0)
    //        {
    //            c = BoardManager.Instance.Tokens[currentX - 1, currentY - 1];
    //            //if c!=null CHECK THIS LATER DAWG
    //            if (c != null && c.isWhite)
    //            {
    //                r[currentX - 1, currentY - 1] = true;
    //            }
    //            else if (c == null)
    //            {
    //                r[currentX - 1, currentY - 1] = true;
    //            }
    //        }
    //        //Diagonal right
    //        if (currentX != 7 && currentY != 0)
    //        {
    //            c = BoardManager.Instance.Tokens[currentX + 1, currentY - 1];
    //            //if c!=null CHECK THIS LATER DAWG
    //            if (c != null && c.isWhite)
    //            {
    //                r[currentX + 1, currentY - 1] = true;
    //            }
    //            else if (c == null)
    //            {
    //                r[currentX + 1, currentY - 1] = true;
    //            }
    //        }
    //        //Middle
    //        if (currentY != 0)
    //        {
    //            c = BoardManager.Instance.Tokens[currentX, currentY - 1];
    //            if (c == null)
    //            {
    //                r[currentX, currentY - 1] = true;
    //            }
    //        }
    //    }
    //    return r;
    //}
}
