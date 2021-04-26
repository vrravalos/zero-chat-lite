using System.Collections.Generic;
using System.Linq;

namespace ChatLibrary.Workflow
{
    public class CommandsInvoker
    {
        private readonly Queue<IConditionalCommand> _commandsQueue = new Queue<IConditionalCommand>();

        public CommandsInvoker(IConditionalCommand command)
        {
            AddCommand(command);
        }

        public CommandsInvoker()
        {
        }

        public void AddCommand(IConditionalCommand command)
        {
            _commandsQueue.Enqueue(command);
        }

        /// <summary>
        /// Invoke las added command
        /// </summary>
        /// <returns></returns>
        public bool Invoke()
        {
            return _commandsQueue.Last().ExecuteConditionalAction();
        }

        /// <summary>
        /// Invoke all commands added
        /// </summary>
        /// <returns></returns>
        public IEnumerable<bool> InvokeAll()
        {
            List<bool> result = new List<bool>();
            foreach (var item in _commandsQueue)
            {
                result.Add(item.ExecuteConditionalAction());
            }

            return result;
        }

        internal void Flush()
        {
            _commandsQueue.Clear();
        }
    }
}