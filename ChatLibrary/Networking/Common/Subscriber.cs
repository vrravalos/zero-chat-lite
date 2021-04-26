using System;
using ChatLibrary.Domain;

namespace ChatLibrary.Networking.Common
{
    /// <summary>
    /// Just a classic "base" subscriber
    /// </summary>
    internal abstract class Subscriber
    {
        protected UserSession userSession;
        protected ChatRoom chatRoom;

        protected readonly Guid guid;

        internal Subscriber(Publisher pub, Guid? id)
        {
            guid = id == null ? Guid.NewGuid() : id.Value;

            pub.RaiseCustomEvent += HandleEnvelope;
        }

        // Handle info from publisher
        internal abstract void HandleEnvelope(object sender, EnvelopeEventArgs e);
    }
}