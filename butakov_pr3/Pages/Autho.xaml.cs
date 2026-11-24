using butakov_pr3.Models;
using butakov_pr3.Services;
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
    public partial class Page1 : Page
    {
        private int failedAttempts = 0;

        private string currentCaptcha = string.Empty;

        public Page1()
        {
            InitializeComponent();
        }

        private void btnEnterGuests_Click(
            object sender, RoutedEventArgs e)
        {
            ClearFields();

            NavigationService.Navigate(new Client(null));
        }

        private void btnEnter_Click(
            object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text.Trim();

            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните логин и пароль!");

                return;
            }

            if (failedAttempts > 0)
            {
                string enteredCaptcha = txtBoxCaptcha.Text.Trim();

                if (!string.Equals(
                    enteredCaptcha,
                    currentCaptcha,
                    StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Неверная капча!");

                    txtPassword.Clear();

                    txtBoxCaptcha.Clear();

                    GenerateCaptcha();

                    return;
                }
            }

            try
            {
                string passwordHash =
                    Hash.HashPassword(password);

                UserModel user =
                    DatabaseService.Authenticate(
                        login,
                        passwordHash);

                if (user == null)
                {
                    failedAttempts++;

                    txtPassword.Clear();

                    txtBoxCaptcha.Clear();

                    GenerateCaptcha();

                    MessageBox.Show(
                        "Неверный логин или пароль!");

                    return;
                }

                LoadPage(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка авторизации или подключения к базе данных:\n"
                    + ex.Message);
            }
        }

        private void GenerateCaptcha()
        {
            currentCaptcha =
                CaptchaGenerator.GenerateCaptchaText(6);

            txtBlockCaptcha.Text =
                "Капча: " + currentCaptcha;

            txtBlockCaptcha.Visibility =
                Visibility.Visible;

            txtBoxCaptcha.Visibility =
                Visibility.Visible;
        }

        private void LoadPage(UserModel user)
        {
            Page nextPage = null;

            switch (user.RoleName.Trim())
            {
                case "Клиент":

                    nextPage = new Client(user);

                    break;

                case "Сотрудник":

                    nextPage = new EmployeePage(user);

                    break;

                case "Администратор":

                    nextPage = new AdminPage(user);

                    break;

                default:

                    MessageBox.Show(
                        "Для роли " + user.RoleName +
                        " еще не создана страница!");

                    return;
            }

            ClearFields();

            NavigationService.Navigate(nextPage);
        }

        private void ClearFields()
        {
            txtLogin.Clear();

            txtPassword.Clear();

            txtBoxCaptcha.Clear();

            txtBlockCaptcha.Text = string.Empty;

            txtBlockCaptcha.Visibility =
                Visibility.Collapsed;

            txtBoxCaptcha.Visibility =
                Visibility.Collapsed;

            currentCaptcha = string.Empty;

            failedAttempts = 0;
        }
    }
}