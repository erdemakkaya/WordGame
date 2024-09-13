using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGame.Core.Dto;

namespace WordGame.Application.Validators
{
	public class WordValidator: AbstractValidator<WordDto>
	{
		public WordValidator()
		{
			RuleFor(x => x.WordName).NotEmpty().NotNull();
			RuleFor(x => x.TurkishTranslator).NotEmpty().NotNull();
		}
	}
}
