using Moq;
using NUnit.Framework;
using Promotion.Engine.API.Application.Commands;
using Promotion.Engine.Domain.AggregateRoots;
using Promotion.Engine.Domain.Repositories;
using System.Collections.Generic;

namespace Promotion.Engine.Tests
{
    public class Tests
    {
        private Mock<IPromotionRepository> promotionRepositoryMock;

        [SetUp]
        public void Setup()
        {
            promotionRepositoryMock = new Mock<IPromotionRepository>();
        }

        [Test]
        [TestCaseSource("_sourceLists")]
        public void HandleApplyPromotion(ApplyPromotionCommand cart, int total)
        {
            promotionRepositoryMock.Setup(d => d.GetAsync()).ReturnsAsync(FakePromotions());

            var handler = new ApplyPromotionCommandHandler();
        }

        private List<Promotions> FakePromotions()
        {
            return new List<Promotions>
            {
                new Promotions("3 of A's for 130", 130, PromotionType.Quantity, true),
                new Promotions("2 of B's for 45", 45, PromotionType.Quantity, true),
                new Promotions("C & D for 30", 30, PromotionType.MultiSku, true),
            };
        }

        private static readonly object[] _sourceLists =
        {
            new object[] { new ApplyPromotionCommand(new List<CartItems>
                {
                    new CartItems("A", 1),
                    new CartItems("B", 1),
                    new CartItems("C", 1)
                }), 100 },
            new object[] { new ApplyPromotionCommand(new List<CartItems>
                {
                    new CartItems("A", 5),
                    new CartItems("B", 5),
                    new CartItems("C", 1)
                }), 370
            },
            new object[] { new ApplyPromotionCommand(new List<CartItems>
                {
                    new CartItems("A", 3),
                    new CartItems("B", 5),
                    new CartItems("C", 1),
                    new CartItems("D", 1)
                }), 280
            }
        };
    }
}