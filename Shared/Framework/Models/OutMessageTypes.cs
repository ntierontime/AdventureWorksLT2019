namespace Framework.Models
{
    /// <summary>
    /// This enum is to controll message sending via following 3 types, maybe more
    /// When an Action taken. used in Server Side Services layer,
    /// Together with the standard/regular Create/Update/Delete/ViewDetails actions, and customized DataOperation,
    /// </summary>
    public enum OutMessageTypes
    {
        InternalNotification,
        Email,
        TextMessage,
    }
}

