using System;

namespace ChatLibrary.Domain
{
    public class UserSession
    {
        private string _strUserId;
        public string StrUserGuid => _strUserId;
        public UserSession(string strUserId)
        {
            _strUserId = strUserId;
            UserGuid = Guid.Parse(strUserId);
        }

        //public UserSession(Guid userId)
        //{
        //    UserId = userId;
        //}

        public void SetUserName(string userName)
        {
            _userName = userName;
        }

        public Guid UserGuid { get; }

        private string _userName;
        public string UserName => _userName;

        internal bool HasUserName()
        {
            return !string.IsNullOrEmpty(_userName);
        }

        internal static UserSession GetServerUser()
        {
            return new UserSession(Guid.Empty.ToString());
        }
    }
}