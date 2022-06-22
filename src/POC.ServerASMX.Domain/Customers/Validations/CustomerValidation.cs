using POC.ServerASMX.Domain.Core.Notifications;
using POC.ServerASMX.Domain.Customers.Commands.Input;
using POC.ServerASMX.Domain.Customers.Enums;
using System;
using System.Collections.Generic;

namespace POC.ServerASMX.Domain.Customers.Validations
{
    public class CustomerValidation : Notifier
    {
        public IReadOnlyCollection<Notification> ValidateId(long id)
        {
            if (id <= 0)
                AddNotification("Id", "Id must be greater than zero");

            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                AddNotification("Name", "Name is a required field");
            else if (name.Length > 100)
                AddNotification("Name", $"Name must contain a maximum of 100 characters");

            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateBirth(DateTime birth)
        {
            if (birth == DateTime.MinValue)
                AddNotification("Birth", "Birth must contain a valid date");
            else if (birth > DateTime.Now)
                AddNotification("Birth", "Birth must be less than the current date");

            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateGender(EGender gender)
        {
            if (!Enum.IsDefined(typeof(EGender), gender))
                AddNotification("Gender", "Invalid entered gender");

            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateCashBalance(decimal cashBalance)
        {
            if (cashBalance < 0)
                AddNotification("CashBalance", "CashBalance must be greater than or equal to zero");

            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateCommand(CustomerAddCommand command)
        {
            _ = ValidateName(command.Name);
            _ = ValidateBirth(command.Birth);
            _ = ValidateGender(command.Gender);
            _ = ValidateCashBalance(command.CashBalance);
            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateCommand(CustomerUpdateCommand command)
        {
            _ = ValidateId(command.Id);
            _ = ValidateName(command.Name);
            _ = ValidateBirth(command.Birth);
            _ = ValidateGender(command.Gender);
            _ = ValidateCashBalance(command.CashBalance);
            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateCommand(CustomerActivityStateCommand command)
        {
            _ = ValidateId(command.Id);
            return Notifications;
        }

        public IReadOnlyCollection<Notification> ValidateCommand(CustomerDeleteCommand command)
        {
            _ = ValidateId(command.Id);
            return Notifications;
        }
    }
}