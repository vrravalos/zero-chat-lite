namespace ChatLibrary.Workflow
{
    public interface IConditionalCommand
    {
        // returns also if the action was executed
        // in case of an internal validation
        bool ExecuteConditionalAction();
    }
}