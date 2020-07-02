using MediatR;
using Promotion.Engine.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Promotion.Engine.API.Application.Commands
{
    public class ApplyPromotionCommandHandler : IRequestHandler<ApplyPromotionCommand, int>
    {
        private readonly IPromotionRepository _promotionRepository;

        public ApplyPromotionCommandHandler(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository ?? throw new ArgumentNullException(nameof(promotionRepository));
        }

        public async Task<int> Handle(ApplyPromotionCommand request, CancellationToken cancellationToken)
        {
            return 100;
        }
    }
}
