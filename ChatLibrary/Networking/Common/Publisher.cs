using System;
using ChatLibrary.Messaging;

namespace ChatLibrary.Networking.Common
{
    /// <summary>
    /// Just a classic "base" publisher
    /// </summary>
    internal abstract class Publisher
    {
        public event EventHandler<EnvelopeEventArgs> RaiseCustomEvent;

        public void Deliver(ChatEnvelope chatEnvelope)
        {
            OnRaiseEnvelopeEvent(new EnvelopeEventArgs(chatEnvelope));
        }

        protected virtual void OnRaiseEnvelopeEvent(EnvelopeEventArgs e)
        {
            // copy to avoid any chance of race condition
            EventHandler<EnvelopeEventArgs> callEvent = RaiseCustomEvent;

            if (callEvent != null)
            {
                callEvent(this, e);
            }
        }
    }
}