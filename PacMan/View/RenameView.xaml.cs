using PacMan.ViewModel;

namespace PacMan.View
{
    /// <summary>
    /// Interaction logic for RenameView.xaml
    /// </summary>
    public partial class RenameView 
    {
        public string PlayerName { get; set; }

        public RenameView(string currentName)
        {
            
            RenameVm context = new RenameVm(currentName);
            DataContext = context;
            if (context.OkAction == null)
                context.OkAction = () =>
                {
                    PlayerName = context.Name;
                    DialogResult = true;
                };
            if (context.CancelAction == null)
                context.CancelAction = () =>
                {
                    DialogResult = false;
                };

            InitializeComponent();
        }
        
    }
}
