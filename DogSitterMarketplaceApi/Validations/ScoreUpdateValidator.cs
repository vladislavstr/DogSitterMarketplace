using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using DogSitterMarketplaceBll.Models.Orders.Request;
using FluentValidation;

namespace DogSitterMarketplaceApi.Validations
{
    public class ScoreUpdateValidator : AbstractValidator<CommentUpdateDto>
    {
        public ScoreUpdateValidator()
        {
            RuleFor(request => request.Score)
                .ExclusiveBetween(0, 6)
                .WithMessage("Score should be from 1 to 5");
        }
    }
}