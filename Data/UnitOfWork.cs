using PainAssessment.Areas.Admin.Models;
using PainAssessment.Interfaces;
using PainAssessment.Models;

namespace PainAssessment.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private HospitalContext _context;

        private IGenericRepository<Account> accountRepository;
        private IGenericRepository<Administrator> adminRepository;
        private IGenericRepository<Account> loginRepository;

        private IGenericRepository<TemplateChecklist> templateChecklistRepository;
        private IGenericRepository<DefaultQuestion> defaultQuestionRepository;

        public UnitOfWork(HospitalContext context)
        {
            this._context = context;
        }

        public IGenericRepository<Account> AccountRepository
        {
            get
            {
                if (this.accountRepository == null)
                {
                    this.accountRepository = new GenericRepository<Account>(_context);
                }
                return accountRepository;
            }

        }

        public IGenericRepository<Administrator> AdministratorRepository
        {
            get
            {
                if (this.adminRepository == null)
                {
                    this.adminRepository = new GenericRepository<Administrator>(_context);
                }
                return adminRepository;
            }

        }

        public IGenericRepository<Account> LoginRepository
        {
            get
            {
                if (this.loginRepository == null)
                {
                    this.loginRepository = new GenericRepository<Account>(_context);
                }
                return loginRepository;
            }

        }

        public IGenericRepository<TemplateChecklist> TemplateChecklistRepository
        {
            get
            {
                if (this.templateChecklistRepository == null)
                {
                    this.templateChecklistRepository = new GenericRepository<TemplateChecklist>(_context);
                }
                return templateChecklistRepository;
            }
        }

        public IGenericRepository<DefaultQuestion> DefaultQuestionRepository
        {
            get
            {
                if (this.defaultQuestionRepository == null)
                {
                    this.defaultQuestionRepository = new GenericRepository<DefaultQuestion>(_context);
                }
                return defaultQuestionRepository;
            }
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }
}
