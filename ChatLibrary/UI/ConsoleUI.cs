using System;
using System.Collections.Generic;

namespace ChatLibrary.UI
{
    public enum Phrase
    {
        None,
        AskUsername,
        InvalidUsernameFormat,
        GuestPrefix,
        ThankYouNewUsername,
        UsernamePrefix,
        OtherUserMessage,
        OtherUserPrefix,
        BroadcastOtherUserHasJoined,
        BroadcastOthersUserHasLeft,
        EndSessionUsername
    }

    /// <summary>
    /// Responsible for showing the main dialogues
    /// </summary>
    public static class ConsoleUI
    {
        internal struct TextColorPair
        {
            internal string Text;
            internal ConsoleColor Color;
        }

        private static readonly ConsoleColor _defaultColor = Console.ForegroundColor;

        private static readonly Dictionary<Phrase, TextColorPair> _phrases = new Dictionary<Phrase, TextColorPair>()
        {
            {Phrase.AskUsername,
             new TextColorPair()
             {
                 Text = "#CHATROOM: Please write your 'username' (only letters allowed, don't use numbers, space or special chars):",
                 Color = ConsoleColor.DarkYellow
             }
            },

             {Phrase.ThankYouNewUsername,
             new TextColorPair()
             {
                 Text = "#CHATROOM: Thank you! Your new username is @{0}! Cool right?",
                 Color = ConsoleColor.DarkYellow
             }
            },


              {Phrase.EndSessionUsername,
             new TextColorPair()
             {
                 Text = "#CHATROOM: Your chat session has ended! See you next time!",
                 Color = ConsoleColor.DarkRed
             }
            },

                  {Phrase.BroadcastOthersUserHasLeft,
             new TextColorPair()
             {
                 Text = "#CHATROOM: Bye, bye friend! @{0} has left the conversation!",
                 Color = ConsoleColor.DarkRed
             }
            },

              {Phrase.BroadcastOtherUserHasJoined,
             new TextColorPair()
             {
                 Text = "#CHATROOM: Hurray! @{0} has joined the conversation, say hello!",
                 Color = ConsoleColor.DarkGreen
             }
            },

            {Phrase.InvalidUsernameFormat,
             new TextColorPair()
             {
                 Text = "#CHATROOM: Boom!! [INVALID FORMAT] Please use only letters, don't use numbers, space or special chars",
                 Color = ConsoleColor.DarkRed
             }
            },

            {Phrase.GuestPrefix,
             new TextColorPair()
             {
                 Text = "@{GUEST}: ",
                 Color = ConsoleColor.DarkGray
             }
            },

            {Phrase.UsernamePrefix,
             new TextColorPair()
             {
                 Text = "@{0}: ",
                 Color = ConsoleColor.Yellow
             }
            },

            {Phrase.OtherUserPrefix,
             new TextColorPair()
             {
                 Text = "@{0}: ",
                 Color = ConsoleColor.Magenta
             }
            },

            {Phrase.OtherUserMessage,
             new TextColorPair()
             {
                 Text = "{0}",
                 Color = ConsoleColor.Gray
             }
            },
        };

        public static void ShowTextLine(Phrase phrase, string[] textArgs, ConsoleColor overrideColor)
        {
            WriteLineColor(GetTextColor(phrase, textArgs, overrideColor));
        }

        public static void ShowTextLine(Phrase phrase, string[] textArgs = null)
        {
            WriteLineColor(GetTextColor(phrase, textArgs, null));
        }

        public static void ShowText(Phrase phrase, string[] textArgs, ConsoleColor overrideColor)
        {
            WriteColor(GetTextColor(phrase, textArgs, overrideColor));
        }

        public static void ShowText(Phrase phrase, string[] textArgs = null)
        {
            WriteColor(GetTextColor(phrase, textArgs, null));
        }

        #region Private Methods

        private static (string, ConsoleColor) GetTextColor(Phrase phrase, string[] textArgs = null, ConsoleColor? overrideColor = null)
        {
            var textColorPair = _phrases[phrase];

            string text = textArgs != null ? string.Format(textColorPair.Text, textArgs) : textColorPair.Text;
            var color = overrideColor != null ? overrideColor.Value : textColorPair.Color;

            return (text, color);
        }

        private static void WriteLineColor((string msg, ConsoleColor color) textColor)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor.color;
            Console.WriteLine(textColor.msg);
            Console.ForegroundColor = defaultColor;
        }

        private static void WriteColor((string msg, ConsoleColor color) textColor)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = textColor.color;
            Console.Write(textColor.msg);
            Console.ForegroundColor = defaultColor;
        }

        #endregion Private Methods
    }
}