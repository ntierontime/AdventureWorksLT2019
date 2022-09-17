using Framework.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.MauiX.ComponentModels
{
    public class ValueChangedMessageExt<T>: ValueChangedMessage<T>
    {
        public ViewItemTemplates ItemView { get; private set; }

        public ValueChangedMessageExt(T value, ViewItemTemplates itemView)
            : base(value)
        {
            ItemView = itemView;
        }
    }
}

