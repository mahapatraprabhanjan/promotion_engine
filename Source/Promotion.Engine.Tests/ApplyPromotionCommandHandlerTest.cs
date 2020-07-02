using NUnit.Framework;
using Promotion.Engine.API.Application.Commands;
using Promotion.Engine.Domain.Repositories;
using System.Collections.Generic;

namespace Promotion.Engine.Tests
{
    public class Tests
    {
        private IPromotionRepository promotionRepositoryMock;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCaseSource("_sourceLists")]
        public void HandleApplyPromotion(List<ApplyPromotionCommand> cart, int total)
        {
            Assert.Pass();
        }

        private static readonly object[] _sourceLists =
        {
            new object[] { new List<ApplyPromotionCommand>
            {
                new ApplyPromotionCommand("A", 1),
                new ApplyPromotionCommand("B", 1),
                new ApplyPromotionCommand("C", 1)
            }, 100},
            new object[] { new List<ApplyPromotionCommand>
            {
                new ApplyPromotionCommand("A", 5),
                new ApplyPromotionCommand("B", 5),
                new ApplyPromotionCommand("C", 1)
            }, 370},
            new object[] { new List<ApplyPromotionCommand>
            {
                new ApplyPromotionCommand("A", 3),
                new ApplyPromotionCommand("B", 5),
                new ApplyPromotionCommand("C", 1),
                new ApplyPromotionCommand("D", 1)
            }, 280}
        };
    }
}