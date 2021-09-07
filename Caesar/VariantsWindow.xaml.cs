using System.Data;
using System.Windows;

namespace Caesar
{
    /// <summary>
    /// Логика взаимодействия для Variants.xaml
    /// </summary>
    public partial class VariantsWindow : Window
    {
        private readonly DataTable dataSource;

        public VariantsWindow(DataTable dataSource)
        {
            InitializeComponent();
            this.dataSource = dataSource;
            dataGrid.DataContext = dataSource;
        }
    }
}
