using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Button[,] bingoButtons = new Button[5, 5];
        private Button[,] bingoButtonsComputer = new Button[5, 5];
        private Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            InitializeBingoBoard();
            InitializeBingoBoardComputer();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void InitializeNumbersGenerated()
        {
           
        }
        private void InitializeBingoBoard()
        {
            const int buttonSize = 50;
            const int padding = 5;
            const int positionY = 100;

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Button button = new Button();
                    button.SetBounds(10, 10, 10, 10);
                    button.Width = button.Height = buttonSize;
                    button.Top = positionY + row * (buttonSize + padding);
                    button.Left = col * (buttonSize + padding);
                    button.Tag = false; // Tag to keep track of whether the number is marked or not
                    button.Click += BingoButton_Click;
                    bingoButtons[row, col] = button;
                    Controls.Add(button);
                }
            }
            GenerateRandomNumbers(bingoButtons);
        }
        private void InitializeBingoBoardComputer()
        {
            const int buttonSize = 50;
            const int padding = 5;
            const int positionY = 100;
            const int positionX = 500;

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Button button = new Button();
                    button.SetBounds(10, 10, 10, 10);
                    button.Width = button.Height = buttonSize;
                    button.Top = positionY + row * (buttonSize + padding);
                    button.Left = positionX + col * (buttonSize + padding);
                    button.Tag = false; // Tag to keep track of whether the number is marked or not
                    button.Enabled = false;
                    button.Click += BingoButton_Click;
                    bingoButtonsComputer[row, col] = button;
                    Controls.Add(button);
                }
            }
            GenerateRandomNumbers(bingoButtonsComputer);
        }

        private void GenerateRandomNumbers(Button[,] bingoButtons)
        {
            int[] numbers = new int[75];
            for (int i = 0; i < 75; i++)
            {
                numbers[i] = i + 1;
            }

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    int index = random.Next(0, 75);
                    int temp = numbers[index];
                    numbers[index] = numbers[row * 5 + col];
                    numbers[row * 5 + col] = temp;
                }
            }

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    bingoButtons[row, col].Text = numbers[row * 5 + col].ToString();

                }
            }
        }

        private void BingoButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            bool marked = (bool)clickedButton.Tag;
            clickedButton.Tag = !marked;
            clickedButton.BackColor = marked ? SystemColors.Control : SystemColors.Highlight;
            clickedButton.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           bool ganhou = verificarFim();
            if (ganhou)
            {
                MessageBox.Show("PARA DE GRITAR EM FALSO");
            }
            else
            {
                MessageBox.Show("PARABENS VOCE GANHOUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU");
            }
        }

        private bool verificarFim()
        {
            
            if (bingoButtons[0,0].Enabled)
            {
                return true;
            }
            return false;
        }
    }
}

