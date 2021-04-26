using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary.Constants;
using ChatLibrary.Validation;

namespace ChatLibrary.Messaging
{
    /// <summary>
    /// Chat Message (for JSON)
    /// </summary>
    [Serializable]
    public class ChatMessage : MessageHeader
    {
        public Command Command { get; set; }

        public Participant Sender { get; set; }

        public Guid UserGuid { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        #region Public Methods

        public ValidationResult Validate(IValidation<ChatMessage> validation)
        {
            return new ValidationResult
            {
                Errors = validation.Errors(this),
                IsValid = validation.IsValid(this)
            };
        }

        public bool TryValidate(IValidation<ChatMessage> validation, out IEnumerable<string> errors)
        {
            errors = validation.Errors(this);
            return validation.IsValid(this);
        }

        public override string ToString()
        {
            return base.ToString()
                + $"Command:{Command},Participant:{Sender},"
                + $"UserId:{UserGuid},UserName:{UserName},Content:{Content}";
        }

        #endregion
    }
}
