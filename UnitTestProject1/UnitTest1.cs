using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    // There are a lot of tests in each method. It makes debug harder, increases readablilty (self doc) and minimizes maintenance.
    // This is a style discussion. I would put it context of labor costs should you venture down that thought path.
    //mp jul-2016 

    [TestClass]
    public class EngineTesting
    {
        [TestMethod]
        public void NewBoard()
        {
            Checkers.Engine e = new Checkers.Engine();
            UInt32 pass = 0; // it is important for bitboards to be unsigned 32 bit integers, so we enforce it here

            Assert.AreEqual(pass, e.newGame()); //initialization process returns 0 (no error)
            Assert.AreEqual(pass, e.getBlackKings()); //new board should have no kings
            Assert.AreEqual(pass, e.getWhiteKings());
            Assert.AreNotEqual(pass, e.getBlackPieces()); //not checking the magic #, just that it is non-zero on a new board
            Assert.AreNotEqual(pass, e.getWhitePieces());

            // self referential tests where information from the engine should match what the engine built for us
            UInt32 whiteStart = e.testGetWhiteStartMask();
            UInt32 blackStart = e.testGetBlackStartMask();
            UInt32 whitePieces = e.getWhitePieces();
            UInt32 blackPieces = e.getBlackPieces();
            Assert.AreEqual(whiteStart, whitePieces);
            Assert.AreEqual(blackStart, blackPieces);

            //Initial moves on a new board can only be from the first of each row
            Assert.AreEqual(Convert.ToUInt32(0x00f00000), e.moversForWhite());
            Assert.AreEqual(Convert.ToUInt32(0x00000f00), e.moversForBlack());
            //==========

        }

        [TestMethod]
        public void MoveSimplexWhite()
        {
            //white - simple center of the board movers
            UInt32 passedInput = 0x0;
            UInt32 whitePiece = 0x00040000;
            UInt32 blockingWhite = 0x00606000;
            UInt32 blockingLeft = 0x00202000;
            UInt32 blockingRight = 0x00404000;
            UInt32 blockingLeftRear = 0x00206000;
            UInt32 blockingRightRear = 0x00406000;
            UInt32 noMove = 0x0;
            Checkers.Engine e = new Checkers.Engine();

            //one white in center unblocked
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, 0x0, 0x0));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked by enemy
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingWhite, 0x0));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked by friendly
            Assert.AreEqual(passedInput, e.setBoard(whitePiece & blockingWhite, 0x0, 0x0));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingLeft, 0x0));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingRight, 0x0));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));

            //one white in center unblocked and kinged
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, 0x0, whitePiece));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked by enemy
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingWhite, whitePiece));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked by friendly (other friendly can move but not the blocked one)
            Assert.AreEqual(passedInput, e.setBoard(whitePiece | blockingWhite, 0x0, whitePiece));
            Assert.AreEqual(blockingWhite, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingLeft, whitePiece));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingRight, whitePiece));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));

            //Kinged - special checks
            //same job blocked forward and has king blocked
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingWhite, whitePiece));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked forward but has king left back
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingLeftRear, whitePiece));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));
            //same job blocked forward but has king right back
            Assert.AreEqual(passedInput, e.setBoard(whitePiece, blockingRightRear, whitePiece));
            Assert.AreEqual(whitePiece, Convert.ToUInt32(e.moversForWhite()));

        }

        [TestMethod]
        public void MoveSimplexBlack()
        {
            //black - simple center of the board movers
            UInt32 passedInput = 0x0;
            UInt32 blackPiece = 0x00040000;
            UInt32 blockingBlack = 0x00606000;
            UInt32 blockingLeft = 0x00202000;
            UInt32 blockingRight = 0x00404000;
            UInt32 blockingLeftRear = 0x00602000;
            UInt32 blockingRightRear = 0x00604000;
            UInt32 noMove = 0x0;
            Checkers.Engine e = new Checkers.Engine();

            //one white in center unblocked
            Assert.AreEqual(passedInput, e.setBoard( 0x0, blackPiece, 0x0));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked by enemy
            Assert.AreEqual(passedInput, e.setBoard( blockingBlack, blackPiece, 0x0));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked by friendly
            Assert.AreEqual(passedInput, e.setBoard(0x0, blackPiece & blockingBlack, 0x0));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard( blockingLeft, blackPiece, 0x0));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard(blockingRight, blackPiece, 0x0));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));

            //one white in center unblocked and kinged
            Assert.AreEqual(passedInput, e.setBoard(0x0, blackPiece, blackPiece));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked by enemy
            Assert.AreEqual(passedInput, e.setBoard(blockingBlack, blackPiece, blackPiece));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked by friendly (other friendly can move but not the blocked one)
            Assert.AreEqual(passedInput, e.setBoard(0x0, blackPiece | blockingBlack, blackPiece));
            Assert.AreEqual(blockingBlack, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard(blockingLeft, blackPiece, blackPiece));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked in one of two places
            Assert.AreEqual(passedInput, e.setBoard(blockingRight, blackPiece, blackPiece));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));

            //Kinged - special checks
            //same job blocked forward and has king blocked
            Assert.AreEqual(passedInput, e.setBoard(blockingBlack, blackPiece, blackPiece));
            Assert.AreEqual(noMove, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked forward but has king left back
            Assert.AreEqual(passedInput, e.setBoard(blockingLeftRear, blackPiece, blackPiece));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));
            //same job blocked forward but has king right back
            Assert.AreEqual(passedInput, e.setBoard(blockingRightRear, blackPiece, blackPiece));
            Assert.AreEqual(blackPiece, Convert.ToUInt32(e.moversForBlack()));

        }

        [TestMethod]
        public void MoveEdgeChecks()
        {
            //tbd
        }

        [TestMethod]
        public void MoveCornerChecks()
        {
            //tbd
        }

        [TestMethod]
        public void JumpSimplex()
        {
            //tbd
        }

        [TestMethod]
        public void JumpMultiple()
        {
            //tbd
        }

        //force jump checks
        //king me? Is that a function provided and the control is somewhere else?
        //locomotion tests (actually move pieces)
        //removing pieces tests (especially if the board is counting pieces later)
        //evaluation information (data only, not an assessment)
    }
}
