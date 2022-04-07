using PainAssessment.Areas.Admin.Models;
using PainAssessment.Models;
using System;

namespace PainAssessment.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<Administrator> AdministratorRepository { get; }
        IGenericRepository<Account> LoginRepository { get; }
        IGenericRepository<TemplateChecklist> TemplateChecklistRepository { get; }
        IGenericRepository<DefaultQuestion> DefaultQuestionRepository { get; }
        void Save();

    }
}
