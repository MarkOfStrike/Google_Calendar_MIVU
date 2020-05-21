using Google_Calendar_Desktop_App.Properties;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Google_Calendar_Desktop_App
{
    public partial class Autorization : Form
    {
        private Thread mainWin;
        private string name;
        private string pass;

        public Autorization()
        {
            InitializeComponent();

            this.Icon = Resources.Google_Calendar_icon_icons_com_75710;

            if (!File.Exists($@"{Application.StartupPath}\credentials.json"))
            {
                MessageBox.Show($"Файл credentials.json не существует в текущем контексте!", "Ошибка",MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            if (!File.Exists($@"{Application.StartupPath}\Calendar_Events.mdf"))
            {
                MessageBox.Show($"Файл локальной базы данных Calendar_Events.mdf не существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {


                var user = WorkBD.Select_query($"if exists(select Id from Users where UserName = N'{textBox1.Text}') select 1 else select 0");

                if (user.Rows[0].ItemArray[0].ToString() == "1")
                {
                    var userPass = WorkBD.Select_query($"select UserPass from Users where UserName = N'{textBox1.Text}'");

                    try
                    {
                        if (CryptoPass.DecryptStringAES(userPass.Rows[0].ItemArray[0].ToString(), textBox2.Text) == textBox2.Text)
                        {
                            name = textBox1.Text;
                            pass = CryptoPass.EncryptStringAES(textBox2.Text, textBox2.Text);
                            Start();
                        }
                    }
                    catch 
                    {
                        MessageBox.Show("Введен неверный пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    DialogResult result = MessageBox.Show("Такого пользователя не обнаружено, желаете пройти авторизацию?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        name = textBox1.Text;
                        pass = CryptoPass.EncryptStringAES(textBox2.Text, textBox2.Text);
                        Start();
                    }
                }

            }
            else
            {
                MessageBox.Show("Поле пустое!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Запуск следующей формы
        /// </summary>
        private void Start()
        {
            this.Close();
            mainWin = new Thread(RunForm);
            mainWin.SetApartmentState(ApartmentState.STA);
            mainWin.Start();            
        }

        /// <summary>
        /// Старт формы
        /// </summary>
        private void RunForm()
        {
            Application.Run(new MainForm(name, pass));
        }


    }
}
