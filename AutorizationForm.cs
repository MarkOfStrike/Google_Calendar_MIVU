using Google_Calendar_Desktop_App.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Google_Calendar_Desktop_App
{
    public partial class AutorizationForm : Form
    {
        private Thread mainWin;
        private string name;

        public AutorizationForm()
        {
            InitializeComponent();

            this.Icon = Resources.Google_Calendar_icon_icons_com_75710;
        }

        private void logIn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                var user = WorkBD.Select_query($"select Id from Users where UserName = N'{textBox1.Text}'");

                name = textBox1.Text;

                if (user.Rows.Count > 0)
                {
                    Start();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Такого пользователя не обнаружено, желаете пройти авторизацию?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Start();
                    }
                }

            }
            else
            {
                MessageBox.Show("Поле пустое!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Start()
        {
            this.Close();
            mainWin = new Thread(RunForm);
            mainWin.SetApartmentState(ApartmentState.STA);
            mainWin.Start();
        }

        private void RunForm()
        {
            Application.Run(new MainForm(name));
        }
    }
}
