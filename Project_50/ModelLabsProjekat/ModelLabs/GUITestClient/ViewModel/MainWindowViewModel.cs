using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITestClient.ViewModel
{
    public class MainWindowViewModel : ClassINotifyPropertyChanged
    {
        public ClassICommand<string> NavCommand { get; private set; }
        private ClassINotifyPropertyChanged currentViewModel;

        private GetExtentValuesViewModel getExtentValuesViewModel = new GetExtentValuesViewModel();
        private GetRelatedValuesViewModel getRelatedValuesViewModel = new GetRelatedValuesViewModel();
        private GetValuesViewModel getValuesViewModel = new GetValuesViewModel();

        public ClassINotifyPropertyChanged CurrentViewModel
        {
            get { return currentViewModel; }
            set { SetProperty(ref currentViewModel, value); }
        }
        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "1_GetValues":
                    CurrentViewModel = getValuesViewModel;
                    break;
                case "2_GetExtentValues":
                    CurrentViewModel = getExtentValuesViewModel;
                    break;
                case "3_GetRelatedValues":
                    CurrentViewModel = getRelatedValuesViewModel;
                    break;
            }
        }

        public MainWindowViewModel()
        {
            NavCommand = new ClassICommand<string>(OnNav);
            CurrentViewModel = getValuesViewModel;


        }
    }
}
