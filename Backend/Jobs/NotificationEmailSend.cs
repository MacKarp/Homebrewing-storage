using System;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Email;
using Quartz;

namespace Backend.Jobs
{
    [DisallowConcurrentExecution]
    public class NotificationEmailSend : IJob
    {
        private readonly IBackendRepo _repository;
        private readonly IEmailService _emailService;

        public NotificationEmailSend(IBackendRepo repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Email notification send job started at: " + DateTime.Now);
            var itemsWithShortExpireTime = _repository.GetAllExpiresByExpirationTimeLeft(7);
            foreach (var expireItem in itemsWithShortExpireTime)
            {
                // tworzenie odbiorcy wiadomości
                EmailAddress toEmailAddress = new EmailAddress
                {
                    Name = expireItem.IdUser.UserName,
                    Address = expireItem.IdUser.UserEmail
                };

                // tworzenie wiadomosci dodawanie nadawcy, odbiorcy, tematu i treści wiadomości
                EmailMessage mail = new EmailMessage
                {
                    ToAddress = toEmailAddress,
                    Subject = "Upływa termin ważności produktu!",
                    Content = "Uwaga! Termin ważności produktu: "
                              + expireItem.IdItem.ItemName + ", znajdującego się w: "
                              + expireItem.IdStorage.StorageName + " upływa w dniu: "
                              + expireItem.ExpirationDate.Date,
                };

                //wysylka emaila
                _emailService.Send(mail);
                Console.WriteLine("Wysłano email do użytkownika: " + expireItem.IdUser.UserName);
            }
            return Task.CompletedTask;
        }
    }
}