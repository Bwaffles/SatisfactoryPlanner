using Services.SFGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Services.Tests.SFGame
{
    public class SFGameServiceTests
    {
        [Fact]
        public void DoStuffTest()
        {
            var sut = new SFGameService();
            sut.GetGameData();
        }
    }
}
