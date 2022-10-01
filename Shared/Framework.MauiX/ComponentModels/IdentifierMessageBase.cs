using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Framework.MauiX.ComponentModels
{
    public class IdentifierMessageBase<T>: ValueChangedMessage<T>
    {
        public Framework.Models.ViewItemTemplates ItemView { get; private set; }
        public string ReturnPath { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="itemView"></param>
        /// <param name="returnPath">for page navigation only, null when Popup</param>
        public IdentifierMessageBase(T value, Framework.Models.ViewItemTemplates itemView, string returnPath = null)
            : base(value)
        {
            ItemView = itemView;
            ReturnPath = returnPath;
        }
    }
}

