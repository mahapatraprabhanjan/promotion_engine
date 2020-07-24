using Moq;
using NUnit.Framework;
using Promotion.Engine.API.Application.Commands;
using Promotion.Engine.Domain.AggregateRoots;
using Promotion.Engine.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Promotion.Engine.Tests
{
    public class Tests
    {
        private Mock<IPromotionRepository> promotionRepositoryMock;
        private Mock<ISKURepository> skuRepositoryMock;

        [SetUp]
        public void Setup()
        {
            promotionRepositoryMock = new Mock<IPromotionRepository>();
            skuRepositoryMock = new Mock<ISKURepository>();
        }

        [Test]
        [TestCaseSource("_sourceLists")]
        public async Task HandleApplyPromotion(ApplyPromotionCommand cart, int total)
        {
            promotionRepositoryMock.Setup(d => d.GetAsync()).ReturnsAsync(FakePromotions());
            skuRepositoryMock.Setup(d => d.GetAsync()).ReturnsAsync(FakeSKU());

            var handler = new ApplyPromotionCommandHandler(promotionRepositoryMock.Object, skuRepositoryMock.Object);
            var cancellationToken = new CancellationToken();
            var result = await handler.Handle(cart, cancellationToken);

            Assert.AreEqual(result, total);
        }

        private List<SKUs> FakeSKU()
        {
            return new List<SKUs>
            {
                new SKUs("A", 50),
                new SKUs("B", 30),
                new SKUs("C", 20),
                new SKUs("D", 15)                
            };
        }

        private List<Promotions> FakePromotions()
        {
            return new List<Promotions>
            {
                new Promotions("3 of A's", 130, PromotionType.Quantity, true),
                new Promotions("2 of B's", 45, PromotionType.Quantity, true),
                new Promotions("C & D", 30, PromotionType.MultiSku, true),
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