namespace Common.Enum
{
    public class EnumRepresentation:Attribute
    {
        #region Properties
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; } = string.Empty;
        #endregion

        #region Constructor
        public EnumRepresentation(int Code, string Message)
        {
            this.ResponseMessage = Message;
            this.ResponseCode = Code;
        }
        #endregion
    }
}
