using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {

        int quection_count;    //счетчик вопросов
        int correct_answers;   //количество правильных ответов
        int wrong_answers;     //количество не правильных ответов

        string[] array;        //масив баз данных

        int correct_answer_namber;//номер правильных ответов
        int selected_response; //номер выбраного ответа

        System.IO.StreamReader Read;  //считывает информацию из файла

        public Form1()
        {
            InitializeComponent();
        }

        void start()           //читает файл
        {
            var encoding = System.Text.Encoding.GetEncoding(65001); //кодировка UTF-8  //65001

            try                //если ошибка в считывание файда
            {
                Read = new System.IO.StreamReader(
                    System.IO.Directory.GetCurrentDirectory() + 
                                                               @"\Data.txt", encoding);

                this.Text = Read.ReadLine();   //читаю файл
                quection_count = 0;            //обнуляю даные
                correct_answers = 0;
                wrong_answers = 0;

                array = new string[10];        //задаю значение вопросв (сколько их будет)
            }

            catch(Exception)   
            {
                MessageBox.Show("erro 1");
            }

            вопрос();

        }

        void вопрос()                         //смена вопросов
        {
            label1.Text = Read.ReadLine();                   //вопрос

            radioButton1.Text = Read.ReadLine();             //вариан ответа
            radioButton2.Text = Read.ReadLine();
            radioButton3.Text = Read.ReadLine();

            correct_answers = int.Parse(Read.ReadLine());    // правильный ответ

            radioButton1.Checked = false;                    //выключаю кнопки
            radioButton2.Checked = false;
            radioButton3.Checked = false;

            button1.Enabled = false;

            quection_count++;             //считает сколько правильныхз ответов

            if (Read.EndOfStream == true) 
                button1.Text = "завершить";
        }

        void состояние_переключения(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button1.Focus();
            RadioButton p = (RadioButton)sender;
            var t = p.Name;

            selected_response = int.Parse(t.Substring(11));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(selected_response==correct_answer_namber)//прибовляет количество правильных ответов
            {
                correct_answers++;
            }

            if(selected_response!=correct_answer_namber)//прибовляет количество неправильных ответов
            {
                wrong_answers++;

                array[wrong_answers] = label1.Text;
            }

            if(button1.Text=="начать заново")
            {
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;

                start();
                return;
            }
            if (button1.Text == "завершить")
            {
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;

                //label1.Text=string.Format("тест завершён.\n"+
                  //  "правильных ответов: {0} из {15} .\n"+
                   // "набраных балов: {2:F2}.", correct_answers,
                   // quection_count,(correct_answers * 5.0F)/ quection_count);

                var Str = "error " +
                    ":\n\n";
                for(int i=1;i<= wrong_answers; i++)
                {
                    Str = Str + array[i] + "\n";
                }
                if (wrong_answers != 0)
                {
                    MessageBox.Show(Str, "тест закончился");
                }       
              // if(button1.Text==)
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "следующий вопрос";
            button2.Text = "выйти";

            radioButton1.CheckedChanged += new EventHandler(состояние_переключения);
            radioButton2.CheckedChanged += new EventHandler(состояние_переключения);
            radioButton3.CheckedChanged += new EventHandler(состояние_переключения);

            start();
        }
    }
}
