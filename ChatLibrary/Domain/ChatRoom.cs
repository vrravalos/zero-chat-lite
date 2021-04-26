using System.Collections.Generic;
using ChatLibrary.Logging;
using static ChatLibrary.Logging.Logger;

namespace ChatLibrary.Domain
{
    internal class ChatRoom
    {
        private List<string> _userList = new List<string>();
        internal List<string> ChatUserList => _userList;

        private UserSession _userSession;
        public UserSession UserSession => _userSession;

        public ChatRoom(UserSession userSession)
        {
            SetSession(userSession);
        }

        public void SetSession(UserSession userSession)
        {
            _userSession = userSession;
        }

        internal bool UpdateUsers(string guidUserRaw)
        {
            if (!this.ChatUserList.Contains(guidUserRaw))
            {
                this.ChatUserList.Add(guidUserRaw);
                return true;
            }

            return false;
        }

        internal void ReportUsers()
        {
            LOG(LogLevel.VERBOSE, $"Clients connected: {this.ChatUserList.Count}");
        }
    }
}