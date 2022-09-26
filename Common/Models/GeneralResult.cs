using Common.Enum;

namespace Common.Models
{
    public class GeneralResult<T, S> where S : System.Enum
    {
        #region Declarations
        private S ResultStatus { get; set; }
        public T Result { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        #endregion

        #region Private Methods
        
        private void SetStatus(S Status)
        {
            var StatusValue = Status.GetAttribute<EnumRepresentation>();
            this.StatusCode = StatusValue.ResponseCode;
            this.Message = StatusValue.ResponseMessage;
        }
        #endregion
        #region Constructor
        public GeneralResult(S resultStatus, T result)
        {
            ResultStatus = resultStatus;
            Result = result;
            SetStatus(resultStatus);
        } 
        #endregion
    }
}
