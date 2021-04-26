using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatLibrary.Constants;
using ChatLibrary.Messaging;

namespace ChatLibrary.Validation
{
    
    [Flags]
    public enum CheckList
    {
        Default = 0,
        UserId = 1,
        Content = 2,
        ValidUserName = 4,
        ValidRegistration = 8,
        ValidExit = 16,
    }

    public class ChatValidator : IValidation<ChatMessage>
    {
        private CheckList _checkList;

        public ChatValidator(CheckList checkList)
        {
            _checkList = checkList;
        }

        public bool IsValid(ChatMessage chat)
        {
            return Errors(chat).Count() == 0;
        }

        public IEnumerable<string> Errors(ChatMessage chat)
        {
            if (_checkList.HasFlag(CheckList.UserId) && chat.UserGuid != default)
                yield return "UserId should exist.";

            if (_checkList.HasFlag(CheckList.Content) && string.IsNullOrEmpty(chat.Content))
                yield return "Content can't be empty or null.";

            if (_checkList.HasFlag(CheckList.ValidUserName) && !IsUsernameValid(chat.UserName))
                yield return "Invalid username format.";

            if (_checkList.HasFlag(CheckList.ValidRegistration))
            {
                bool isValidRegistration = chat.Command == Command.REQ_REGISTER_USERNAME
                                            && IsUsernameValid(chat.UserName);

                if (!isValidRegistration)
                    yield return "Invalid registration command";
            }

            if(_checkList.HasFlag(CheckList.ValidExit))
            {
                bool isValidExit = chat.Command == Command.REQ_END_SESSION
                                            && IsUsernameValid(chat.UserName);

                if (!isValidExit)
                    yield return "Invalid exit command";
            }

            yield break;
        }

        private static bool IsUsernameValid(string username)
        {
            bool isValid = username.All(Char.IsLetter);
            return isValid;
        }
    }
}
