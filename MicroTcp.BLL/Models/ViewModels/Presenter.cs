using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroTcp.BLL.Models.ViewModels
{
    public class Presenter : ObservableObject
    {
        private readonly ObservableCollection<ConversationModel> _conversationModel = new ObservableCollection<ConversationModel>();
        public IEnumerable<ConversationModel> History
        {
            get { return _conversationModel; }
        }
        private void AddToHistory(ConversationModel item)
        {
                _conversationModel.Add(item);
        }

    }
}
