namespace JDC.Common.Enums
{
    /// <summary>
    /// Recipient message type.
    /// </summary>
    public enum RecipientType
    {
        /// <summary>
        /// Default enumeration value, which represents the default recipient type for this user
        /// </summary>
        Deafult,

        /// <summary>
        /// The value of the transfer, which is set by the recipient of the parent chat
        /// </summary>
        Parents,

        /// <summary>
        /// The value of the transfer, which is set by the recipient of the student chat
        /// </summary>
        Students,
    }
}
