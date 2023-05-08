using FluentValidation;
using MovieAdvice.Application.Dtos.Advice;
using MovieAdvice.Application.Interfaces;

namespace MovieAdvice.Application.Validation.FluentValidation.Advice
{
    public class AdviceDtoValidator : AbstractValidator<AdviceDto>
    {
        public AdviceDtoValidator(IMoviesService moviesService)
        {
            RuleFor(x => x.ToMailAddress).EmailAddress().WithMessage("Please enter a valid email address.");
            RuleFor(x=>x.MovieId).NotEmpty().WithMessage("Please select a movie.")
               .MustAsync(async (model, movieId, cancellation) => await moviesService.IsMovieExists(movieId))
               .WithMessage("Movie not found!");
        }
    }
}
