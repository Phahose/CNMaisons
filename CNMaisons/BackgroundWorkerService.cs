using CNMaisons.Controller;
using CNMaisons.TechnicalService;
using System.Threading;
using CNMaisons.Domain;

public class BackgroundWorkerService : BackgroundService
{
    readonly ILogger<BackgroundWorkerService> _logger;

    public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger)
    {
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            List<Tenant> overdueTenants = GetDueTenants();
            string messageBody;
            string messageSubject;
            string mailConfirmation;
            CNMPMS PaymentRequestManager = new CNMPMS();
            foreach (Tenant tenant in overdueTenants)
            {
                if (!IsEmailSent(tenant.TenantID, tenant.NextRentDue))
                {
                    // Send reminder email to the tenant
                    messageSubject = "Rent For CN Maisons Due";
                    messageBody = "Good day," +
                                      "\n Your Rent for CN Maisons Is Due today Please be sure to Pay your Rent before the deadline" +
                                      "\n If you have already paid your rent please disregard this email. " +
                                      "\n If your rent has not been paid please make your payment as soon as possible and add the Payment to your Tenant Account" +
                                      "\n Follow these Steps to Add a Payment to Your Tenant Profile" +
                                      "\n\t 1. Login to Your Account on CN Maisons Website" +
                                      "\n\t 2. Click the Record Rent payment Option at the Top of the Screen" +
                                      "\n\t 3. Fill out the Form on the Page with your payment information and Submit";

                               
                    mailConfirmation = PaymentRequestManager.PostEmail(tenant.Email, messageBody, messageSubject);

                    // Record the email sent in the email history table
                    PaymentRequestManager.AddReminder(tenant.TenantID, messageBody, tenant.NextRentDue);
                }
            }
            _logger.LogInformation($"Worker Running at this Time  {DateTimeOffset.Now}");
            
            await Task.Delay(1000, stoppingToken);
        }
    }

    public List<Tenant> GetDueTenants()
    {
        List<Tenant> DueTenants = new List<Tenant>();
        CNMPMS TenantRequestManager = new CNMPMS();
        DueTenants = TenantRequestManager.FindAllTenants();
        DueTenants = DueTenants.Where(x => x.NextRentDue == DateTime.Today).ToList();

        return DueTenants;
    }


    public bool IsEmailSent(string tenantID, DateTime rentDueDate)
    {
        CNMPMS RequestManager = new CNMPMS();

        List<Reminder> reminderList = new();
        reminderList = RequestManager.GetAllReminder();
        reminderList = reminderList.Where(x => x.TenantID == tenantID && x.DueDateRemindedFor == rentDueDate).ToList()!;

        if (reminderList.Count > 0)
        {
            // A reminder Has Been Sent For this Tenant
            return true;
        }
        else
        {
            // No reminder Has Been Sent For this Tenant
            return false;
        }
       
    }

}