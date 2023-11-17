using FuseDigital.PdfSnip.Documents.Dto;
using Volo.Abp.Domain.Services;

namespace FuseDigital.PdfSnip.Documents;

public interface IDocumentDomainService : IDomainService
{
    Task<SplitOutput> SplitAsync(SplitInput input);

    Task<JoinOutput> JoinAsync(JoinInput input);

    Task<RotatePageOutput> RotatePageAsync(RotatePageInput input);
}