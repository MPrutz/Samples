using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class EngineTesting
    {
        [TestMethod]
        public void NewBoard()
        {
            Checkers.Engine e = new Checkers.Engine();
            System.UInt32 pass = 0; // it is important for bitboards to be unsigned 32 bit integers, so we enforce it here

            Assert.AreEqual(pass, e.newGame()); //initialization process returns 0 (no error)
            Assert.AreEqual(pass, e.getBlackKings()); //new board should have no kings
            Assert.AreEqual(pass, e.getWhiteKings());
            Assert.AreNotEqual(pass, e.getBlackPieces()); //not checking the magic #, just that it is non-zero on a new board
            Assert.AreNotEqual(pass, e.getWhitePieces());

            // self referential tests where information from the engine should match what the engine built for us
            System.UInt32 whiteStart = e.testGetWhiteStartMask();
            System.UInt32 blackStart = e.testGetBlackStartMask();
            System.UInt32 whitePieces = e.getWhitePieces();
            System.UInt32 blackPieces = e.getBlackPieces();
            //----------
            Assert.AreEqual(whiteStart, whitePieces);
            Assert.AreEqual(blackStart, blackPieces);
            //temp hack test
            Assert.AreEqual(System.Convert.ToUInt32(0x00f00000), e.WhiteMoves());
            Assert.AreEqual(System.Convert.ToUInt32(0x000f0000), e.BlackMoves());
            //==========
        }
    }
}
