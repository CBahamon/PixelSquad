
namespace Domain.Base.Entities.Models
{
    public class ActionResult<T>
    {
        public bool StateResult = false;

        public string? Message;

        public T? ObjectEmbbeded;

        public ActionResult()
        {
        }

        public ActionResult(bool stateResult, string message, T objectEmbbeded)
        {
            this.StateResult = stateResult;
            this.Message = message;
            this.ObjectEmbbeded = objectEmbbeded;
        }
    }

    public class ActionResult
    {
        public bool StateResult = false;

        public string? Message;
        
        public object? ObjectEmbbeded;

        public ActionResult()
        {
        }

        public ActionResult(bool stateResult, string message,object objectEmbbeded)
        {
            this.StateResult = stateResult;
            this.Message = message;
            this.ObjectEmbbeded = objectEmbbeded;
        }
    }
    
}