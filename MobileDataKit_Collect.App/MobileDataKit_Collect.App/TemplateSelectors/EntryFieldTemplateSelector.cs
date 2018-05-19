using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MobileDataKit_Collect.App.TemplateSelectors
{
    public class EntryFieldTemplateSelector : Xamarin.Forms.DataTemplateSelector
    {
        public EntryFieldTemplateSelector()
               {
            this.NoteLabelTemplate = new DataTemplate(typeof(Templates.NoteLabelTemplate));
            this.SingleEntryTemplate = new DataTemplate(typeof(Templates.SingleEntry));
            this.TextEntryTemplate = new DataTemplate(typeof(Templates.TextEntry));
    }
    private readonly DataTemplate NoteLabelTemplate;
    private readonly DataTemplate SingleEntryTemplate;
    private readonly DataTemplate TextEntryTemplate;

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as MobileDataKit_Collect.Common.FormElement;
            if (messageVm == null)
                return null;

            if(messageVm.Type == "note")
            {
                return NoteLabelTemplate;
            }
            if (messageVm.Type.StartsWith("select_one"))
            {
                return SingleEntryTemplate;
            }

            if (messageVm.Type.StartsWith("text"))
            {
                return TextEntryTemplate;
            }

            return null;
        }
    }
}
