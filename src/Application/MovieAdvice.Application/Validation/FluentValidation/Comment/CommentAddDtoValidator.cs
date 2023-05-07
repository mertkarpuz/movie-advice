using FluentValidation;
using MovieAdvice.Application.Dtos.Comment;
using MovieAdvice.Application.Interfaces;

namespace MovieAdvice.Application.Validation.FluentValidation.Comment
{
    public class CommentAddDtoValidator : AbstractValidator<CommentAddDto>
    {
        public CommentAddDtoValidator(IMoviesService moviesService)
        {
            RuleFor(x => x.Point).NotNull().WithMessage("Please enter a point")
                .InclusiveBetween(1, 10).WithMessage("Please enter a point between 1-10");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a note for selected movie.");
            RuleFor(x => x.MovieId).NotEmpty().WithMessage("Please select a movie.")
               .MustAsync(async (model, movieId, cancellation) => await moviesService.IsMovieExists(movieId))
               .WithMessage("Movie not found!");
        }
    }
}
