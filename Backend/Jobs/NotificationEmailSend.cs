using System;
using System.Threading.Tasks;
using Backend.Data;
using Quartz;

namespace Backend.Jobs
{
    [DisallowConcurrentExecution]
    public class NotificationEmailSend : IJob
    {
        private readonly IBackendRepo _repository;

        public NotificationEmailSend(IBackendRepo repository)
        {
            _repository = repository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Email notification send job started at: " + DateTime.Now);
            var itemsWithShortExpireTime = _repository.GetAllExpiresByExpirationTimeLeft(7);
            foreach (var expireItem in itemsWithShortExpireTime)
            {
                Console.WriteLine("Need to implement to send emails instead of console write");
                Console.WriteLine("Item name: " + expireItem.IdItem.ItemName.ToString() + ", from storage: " + expireItem.IdStorage.StorageName + ", have short expire time: " + expireItem.ExpirationDate.Date);
            }
            return Task.CompletedTask;
        }
    }
}