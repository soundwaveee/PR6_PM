using butakov_pr3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace butakov_pr3.Pages
{
    public partial class Client : Page
    {
        public Client(UserModel user)
        {
            InitializeComponent();

            if (user == null)
            {
                txtHeading.Text = "Вы вошли как гость";

                txtUser.Text = string.Empty;
            }
            else
            {
                txtHeading.Text = "Страница клиента";

                txtUser.Text =
                    "Пользователь: " +
                    user.FirstName + " " +
                    user.LastName;
            }
        }
    }
}