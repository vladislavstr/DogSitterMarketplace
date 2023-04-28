using DogSitterMarketplaceApi.Models.OrdersDto.Request;
using FluentValidation;

namespace DogSitterMarketplaceApi.Validations
{
    public class ScoreValidator : AbstractValidator<CommentRequestDto>
    {
        public ScoreValidator()
        {
            RuleFor(request => request.Score)
                .ExclusiveBetween(0, 6)
                .WithMessage("Score should be from 1 to 5");
        }
    }
}
