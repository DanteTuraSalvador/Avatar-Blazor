using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Web.Services
{
    public class EmailSender : IEmailSender<ApplicationUser>
    {
        public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            // In a real application, you would send an email with the confirmation link
            // For this demo, we'll just return a completed task
            return Task.CompletedTask;
        }

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            // In a real application, you would send an email with the password reset link
            // For this demo, we'll just return a completed task
            return Task.CompletedTask;
        }

        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            // In a real application, you would send an email with the password reset code
            // For this demo, we'll just return a completed task
            return Task.CompletedTask;
        }
    }
}