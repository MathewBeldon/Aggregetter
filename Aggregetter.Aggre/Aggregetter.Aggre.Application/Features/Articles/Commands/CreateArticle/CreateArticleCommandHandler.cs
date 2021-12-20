﻿using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Application.Exceptions;
using Aggregetter.Aggre.Domain.Entities;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Application.Features.Articles.Commands.CreateArticle
{
    public sealed class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, CreateArticleCommandResponse>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public CreateArticleCommandHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<CreateArticleCommandResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var createArticleCommandResponse = new CreateArticleCommandResponse();

            var validator = new CreateArticleCommandValidator(_articleRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                createArticleCommandResponse.Success = false;
                createArticleCommandResponse.ValidationErrors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    createArticleCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }

                return createArticleCommandResponse;
            }

            if (createArticleCommandResponse.Success)
            {
                var article = _mapper.Map<Article>(request);
                var response = await _articleRepository.AddAsync(article, cancellationToken);
                createArticleCommandResponse.Data = _mapper.Map<CreateArticleDto>(response);

                return createArticleCommandResponse;
            }

            throw new CreationExeption($"Could not create new article: \n{request}");
        }
    }
}
