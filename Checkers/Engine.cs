using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Engine
    {
        class BitBoards {
            public UInt32 white;
            public UInt32 black;
            public UInt32 kings;
            public UInt32 empty;
        }
        private BitBoards cBoard = new BitBoards();

        // This set of magic numbers is a standard board
        private const UInt32 WHITESTART = 0xfff00000;
        private const UInt32 BLACKSTART = 0x00000fff;
        private const UInt32 KINGSTART  = 0x00000000;
        private const UInt32 RESPONDACK = 0X00000000;


        // Bit board definition of bit positions 
        //  28  29  30  31      //
        //24  25  26  27        // white start
        //  20  21  22  23      //
        //16  17  18  19
        //  12  13  14  15
        //08  09  10  11        //
        //  04  05  06  07      // black start
        //00  01  02  03        //

        // Make bit masks to describe which positions are viable for each shift pattern
        private UInt32 MASK_L3 = 0x0E0E0E0E; //right 3 bits in odd rows
        private UInt32 MASK_L4 = 0xFFFFFFFF; //all
        private UInt32 MASK_L5 = 0x00707070; //left 3 bits in even rows - not top row
        private UInt32 MASK_R3 = 0x70707070; //left 3 bits in even rows
        private UInt32 MASK_R4 = 0xFFFFFFFF; //all
        private UInt32 MASK_R5 = 0x0E0E0E00; //right 3 bits in odd rows - not bottom row

        //MASK_L3 = S[1] | S[2] | S[3] | S[9] | S[10] | S[11] | S[17] | S[18] | S[19] | S[25] | S[26] | S[27];
        //MASK_L5 = S[4] | S[5] | S[6] | S[12] | S[13] | S[14] | S[20] | S[21] | S[22];
        //MASK_R3 = S[28] | S[29] | S[30] | S[20] | S[21] | S[22] | S[12] | S[13] | S[14] | S[4] | S[5] | S[6];
        //MASK_R5 = S[25] | S[26] | S[27] | S[17] | S[18] | S[19] | S[9] | S[10] | S[11];
        //MASK_x4 = all  - really unecessary, but for conceptual completeness it is here documented

    
        // for testing
        public UInt32 testGetBlackStartMask() { return BLACKSTART; } //return the mask used to make the board
        public UInt32 testGetWhiteStartMask() { return WHITESTART; }

        // really - public
        public UInt32 getAllWhitePieces() { return (cBoard.white); }
        public UInt32 getAllBlackPieces() { return (cBoard.black); }
        public UInt32 getWhiteKings()     { return (cBoard.white & cBoard.kings); }
        public UInt32 getBlackKings()     { return (cBoard.black & cBoard.kings); }
        public UInt32 getWhitePieces()    { return (getAllWhitePieces() & ~getWhiteKings()); }
        public UInt32 getBlackPieces()    { return (getAllBlackPieces() & ~getBlackKings()); }

        public UInt32 whiteMoves()
        {
            UInt32 pieces = getWhitePieces();
            UInt32 opensquares = cBoard.empty;
            UInt32 kings = getWhiteKings();
            UInt32 movers = 0;
            movers |= (opensquares << 4);
            movers |= ((opensquares & MASK_L3) << 3);
            movers |= ((opensquares & MASK_L5) << 5);
            movers &= pieces;
            if (kings>0) //kings can move backwards so we have to check that direction
            {
                movers |= (opensquares >> 4) & kings;
                movers |= ((opensquares & MASK_R3) >> 3) & kings;
                movers |= ((opensquares & MASK_R5) >> 5) & kings;
            }
            return movers;
        }
        public UInt32 blackMoves()
        {
            UInt32 pieces = getBlackPieces();
            UInt32 opensquares = cBoard.empty;
            UInt32 kings = getBlackKings();
            UInt32 movers = 0;
            movers |= (opensquares >> 4);
            movers |= ((opensquares & MASK_L3) >> 3);
            movers |= ((opensquares & MASK_L5) >> 5);
            movers &= pieces;
            if (kings > 0) //kings can move backwards so we have to check that direction
            {
                movers |= (opensquares << 4) & kings;
                movers |= ((opensquares & MASK_R3) << 3) & kings;
                movers |= ((opensquares & MASK_R5) << 5) & kings;
            }
            return movers;
        }
        private void setEmpties()
        {
            cBoard.empty = ~(cBoard.white | cBoard.black);
        }
        public UInt32 setBoard(UInt32 White, UInt32 Black, UInt32 Kings)
        {
            if (Kings != ((Kings & White) | (Kings & Black)) { return 1; }
            if ((Black & White) > 0) { return 2; }
            if (((Black & White) | Kings) > (Black & White)) { return 3; }

            cBoard.white = White;
            cBoard.black = Black;
            cBoard.kings = Kings;
            setEmpties();
            return 0;
        }
        public UInt32 newGame()
        {
            cBoard.white = WHITESTART;
            cBoard.black = BLACKSTART;
            cBoard.kings = KINGSTART;
            setEmpties(); 
            //tbd counters etc when implemented
            return RESPONDACK; //I'd do a bool but everything else is int - so old school return 0 =true else #=message
        }

    }
}
