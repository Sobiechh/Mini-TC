using Menu.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        private String _labelContent;
        public String LabelContent
        {
            get => _labelContent;
            set => SetProperty(ref _labelContent, value);
        }

        public MainWindowViewModel()
        {
            LabelContent = "Siemanko";
        }
    }
}
