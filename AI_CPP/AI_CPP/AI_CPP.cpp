// AI_CPP.cpp : Defines the exported functions for the DLL application.
//
#include "stdafx.h"
#include "Masks.h"
#include "Utils.h"
#include "Analyzer.h"
#include "BitsMagic.h"

extern "C" __declspec(dllexport) void __stdcall GetMove(int whiteCoordinates[], int whiteCount, int blackCoordinates[], int blackCount, int & fromX, int & fromY, int & direction, int color)
{
    //convert coordinates into bitboard
    //call Analyzer::GetMove
    //convert answer to answer variables
    
    BitBoard board;

    for (int i = 0; i < whiteCount; i+=2) 
    {
        int x = whiteCoordinates[i];
        int y = whiteCoordinates[i + 1];
        board.whitePieces |= Masks::OrientationMasks::CoordinatesToUlong[x][y];
    }
    for (int i = 0; i < blackCount; i += 2)
    {
        int x = blackCoordinates[i];
        int y = blackCoordinates[i + 1];
        board.blackPieces |= Masks::OrientationMasks::CoordinatesToUlong[x][y];
    }

    BitBoard move = Analyzer::GetMove(board, color == 0 ? PlayerColor::White : PlayerColor::Black);
    
    unsigned long long movedPiece = 0;
    if (color == 0)
    {
        movedPiece = move.whitePieces ^ board.whitePieces;
        int destination = BitsMagic::BitScanForwardWithReset(movedPiece);
        int start = BitsMagic::BitScanForwardWithReset(movedPiece);
        fromX = Masks::OrientationMasks::CurrentColumn[start] - 1;
        fromY = Masks::OrientationMasks::CurrentRow[start] - 1;
        if (Masks::WhiteMasks::EastAttack[start] & Masks::OrientationMasks::CurrentSquare[destination] != 0)
        {
            direction = 0;
        }
        else if (Masks::WhiteMasks::Forward[start] & Masks::OrientationMasks::CurrentSquare[destination] != 0)
        {
            direction = 1;
        }
        else if (Masks::WhiteMasks::WestAttack[start] & Masks::OrientationMasks::CurrentSquare[destination] != 0)
        {
            direction = 2;
        }
    }
    else if (color == 1) 
    {
        movedPiece = move.blackPieces ^ board.blackPieces;
        int start = BitsMagic::BitScanForwardWithReset(movedPiece);
        int destination = BitsMagic::BitScanForwardWithReset(movedPiece);
        fromX = Masks::OrientationMasks::CurrentColumn[start] - 1;
        fromY = Masks::OrientationMasks::CurrentRow[start] - 1;
        if (Masks::BlackMasks::EastAttack[start] & Masks::OrientationMasks::CurrentSquare[destination] != 0)
        {
            direction = 0;
        }
        else if (Masks::BlackMasks::Forward[start] & Masks::OrientationMasks::CurrentSquare[destination] != 0)
        {
            direction = 1;
        }
        else if (Masks::BlackMasks::WestAttack[start] & Masks::OrientationMasks::CurrentSquare[destination] != 0)
        {
            direction = 2;
        }
    }
}
