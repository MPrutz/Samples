using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Engine
    {
        // This set of magic numbers is a standard board
        private const UInt32 BOARDSIZE  = 0x0008;
        private const UInt32 WHITESTART = 0xfc00;
        private const UInt32 BLACKSTART = 0x00cf;
        private const UInt32 KINGSTART  = 0x0000;
        private const UInt32 RESPONDACK = 0X0000;
        //bitboards (bottom left to top right imagery)
        private UInt32 whitePieces;
        private UInt32 blackPieces;
        private UInt32 kings;

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

        private UInt32 Unoccupied() { return ~(whitePieces | blackPieces); }
    
        // for testing
        public UInt32 testGetBlackStartMask() { return BLACKSTART; } //return the mask used to make the board
        public UInt32 testGetWhiteStartMask() { return WHITESTART; }
        
        // really - public
        public UInt32 getWhitePieces() { return (whitePieces & ~(whitePieces & kings)); }
        public UInt32 getBlackPieces() { return (blackPieces & ~(blackPieces & kings)); }
        public UInt32 getWhiteKings() { return (whitePieces & kings); }
        public UInt32 getBlackKings() { return (blackPieces & kings); }
        public UInt32 getAnyWhitePieces() { return (whitePieces); }
        public UInt32 getAnyBlackPieces() { return (blackPieces); }

        public UInt32 WhiteMoves()
        {
            UInt32 opensquares = Unoccupied();
            UInt32 kings = getWhiteKings();
            UInt32 movers = (opensquares << 4) & whitePieces;
            
                movers = (opensquares << 4) & whitePieces;
                movers |= ((opensquares & MASK_L3) << 3) & whitePieces;
                movers |= ((opensquares & MASK_L5) << 5) & whitePieces;
                if (kings>0) //kings can move backwards so we have to check that direction
                {
                    movers |= (opensquares >> 4) & kings;
                    movers |= ((opensquares & MASK_R3) >> 3) & kings;
                    movers |= ((opensquares & MASK_R5) >> 5) & kings;
                }
                return movers;
        }


        public UInt32 newGame()
        {
            whitePieces = WHITESTART;
            blackPieces = BLACKSTART;
            kings       = KINGSTART;
            //tbd counters etc when implemented
            return RESPONDACK; //I'd do a bool but everything else is int - so old school return 0 =true else #=message
        }

    }
}
