using ApplicationCore.Common;
using ApplicationCore.Interfaces;
using DocumentFormat.OpenXml.Vml.Office;
using Domain.Entities.EntitiesCad;
using Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.CadNotasCore.RelatorioCLVMs.Commands.Generate
{
    public record GenerateCLVM : IRequest<Result<int>>
    {
        public string? Observacao { get; init; }
        public int IdLVM { get; init; }
        public int? IdRelatorio { get; init; }
        public RelatorioStatus Status { get; init; }
        public int? Assinatura { get; init; }
    }

    public class GenerateCLVMCommandHandler(IRepositoryCad<RelatorioCLVM> repos, IRepositoryCad<CadLVM> reposlvm) : IRequestHandler<GenerateCLVM, Result<int>>
    {
        private readonly IRepositoryCad<RelatorioCLVM> _repository = repos;
        private readonly IRepositoryCad<CadLVM> _repositoryLVM = reposlvm;

        public async Task<Result<int>> Handle(GenerateCLVM req, CancellationToken cancellationToken)
        {
        
            if (req.IdRelatorio == null || req.IdRelatorio.HasValue)
            {
                var relatorio = RelatorioCLVM.Create(req.IdLVM.ToString(), req.Status, 1, req.Observacao, "a");
                await _repository.AddAsync(relatorio, cancellationToken);
                var lvm = await _repositoryLVM.GetByIdAsync(req.IdLVM);
                if (lvm is null) return Result<int>.Failure(new Error("404", "Entity not found."));
                lvm.UpdateCLVMRelatorio(relatorio.Id);
                await _repositoryLVM.UpdateAsync(lvm, cancellationToken);

                return Result<int>.Success(relatorio.Id);
            }
            else
            {
                var relatorioClvm = await _repository.GetByIdAsync(req.IdRelatorio.Value, cancellationToken);
                if (relatorioClvm is null) return Result<int>.Failure(new Error("404", "Entity not found."));
                relatorioClvm.UpdateRelatorio(req.Status, req.Observacao);
                await _repository.UpdateAsync(relatorioClvm, cancellationToken);
                return Result<int>.Success(relatorioClvm.Id);
            }        
        }
    }

}
