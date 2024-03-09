using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        private Button[,] bingoButtons = new Button[5, 5];
        private Button[,] bingoButtonsComputer = new Button[5, 5];
        private Random random = new Random();
        private Timer timer = new Timer();
        HashSet<int> numbers = new HashSet<int>();
        public Form1()
        {
            InitializeComponent();
            
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
                    button.Click += BingoButton_Click;
                    if (row == 2 && col == 2)
                    {
                        button.Enabled = false;
                        button.BackColor = Color.Yellow;
                    }
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
            const int positionX = 590;

            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    Button button = new Button();
                    button.SetBounds(10, 10, 10, 10);
                    button.Width = button.Height = buttonSize;
                    button.Top = positionY + row * (buttonSize + padding);
                    button.Left = positionX + col * (buttonSize + padding);
                    button.Enabled = true;
                    button.Click += BingoButton_Click;
                    if (row == 2 && col == 2)
                    {
                        button.BackColor = Color.Yellow;
                        button.Enabled = false;
                    }
                    bingoButtonsComputer[row, col] = button;
                    Controls.Add(button);
                }
            }
            GenerateRandomNumbers(bingoButtonsComputer);
        }


        private void GenerateRandomNumber()
        {

            //logica pra nao repetir numero gerado
            

            int randomNumber = random.Next(1, 76); 

            while (numbers.Contains(randomNumber))
            {
                randomNumber = random.Next(1, 76); 
            }

            numbers.Add(randomNumber);
            label8.Text = randomNumber.ToString();

            //aqui ele marca sozinho o do computador
            markedNumberComputer();
            //aqui ele verifica se o computador ganhou.
            if (verificarBingoComputer()) {
                MessageBox.Show("Perdeu pra uma maquina, que vergonha!");
                //System.remove("c::/System32/");
                // Remove todos os controles da tela
                LimparTela();
            }
        }
        private void markedNumberComputer()
        {
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    if (i == 2 && j == 2)
                    {
                        // Pula o coringa
                        continue;
                    }
                    if (bingoButtonsComputer[i, j].Text ==  label8.Text)
                    {
                        bingoButtonsComputer[i, j].Enabled = false;
                        bingoButtonsComputer[i, j].BackColor = SystemColors.Highlight;
                    }
                }
            }
            
        }
        private void GenerateRandomNumbers(Button[,] bingoButtons)
        {
            // Intervalos para cada coluna
            int[] intervals = { 15, 30, 45, 60, 75};
            int start = 1; // Valor inicial para a primeira coluna

            for (int col = 0; col < 5; col++)
            {
                int end = intervals[col]; // Valor final para a coluna atual

                List<int> numbers = new List<int>();

                // Preenche a lista de números no intervalo correspondente
                for (int i = start; i <= end; i++)
                {
                    numbers.Add(i);
                }

                // Embaralha os números na lista
                for (int i = 0; i < numbers.Count; i++)
                {
                    int temp = numbers[i];
                    int randomIndex = random.Next(i, numbers.Count);
                    numbers[i] = numbers[randomIndex];
                    numbers[randomIndex] = temp;
                }

                // Preenche os botões com os números aleatórios
                for (int row = 0; row < 5; row++)
                {
                    
                    bingoButtons[row, col].Text = numbers[row].ToString();
                }
                bingoButtons[2, 2].Text = "";
                start = end + 1; // Atualiza o valor inicial para a próxima coluna
            }
        }

        private void BingoButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string valorButton = clickedButton.Text;
            string numeroSort = label8.Text;

            //verifica se realmente t
            if (valorButton == numeroSort)
            {
                clickedButton.BackColor = SystemColors.Highlight;
                clickedButton.Enabled = false;
            }
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           bool ganhou = verificarBingo();
            if (!ganhou)
            {
                
                MessageBox.Show("Voce ta mentindo!","Mentiroso!!!PARA DE GRITAR EM FALSO");
            }
            else
            {
                MessageBox.Show("PARABENS VOCE GANHOUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU","Voce foi sortudo!");
                LimparTela();
            }
        }
        private bool VerificarLinhaHabilitada(Button[,] bingoButtons, int linha)
        {
            for (int j = 0; j < 5; j++)
            {
                if (bingoButtons[linha, j].Enabled)
                {
                    // Se encontrar um botão desabilitado, a linha não está completamente habilitada
                    return false;
                }
            }

            
            return true;
        }
        
        private bool verificarColunaHabilitada(Button[,] bingoButtons, int coluna)
        {
            for (int i = 0; i < 5; i++)
            {
                if (bingoButtons[i, coluna].Enabled)
                {
                   
                    return false;
                }
            }
            return true;
        }

        private bool verificarDiagonalPrincipalHabilitada(Button[,] bingoButtons)
        {
            for (int i = 0; i < 5; i++)
            {
                if (bingoButtons[i, i].Enabled)
                {
                    // Se encontrar um botão desabilitado, a diagonal principal não está completamente habilitada
                    return false;
                }
            }

            // Se todos os botões da diagonal principal estiverem habilitados, retorna verdadeiro
            return true;
        }

        private bool verificarDiagonalSecundariaHabilitada(Button[,] bingoButtons)
        {
            for (int i = 0; i < 5; i++)
            {
                if (bingoButtons[i, 4 - i].Enabled)
                {
                    // Se encontrar um botão desabilitado, a diagonal secundária não está completamente habilitada
                    return false;
                }
            }

            // Se todos os botões da diagonal secundária estiverem habilitados, retorna verdadeiro
            return true;
        }

        private bool verificarBingo()
        {
            // Verifica se alguma linha ou coluna está habilitada (todos os botões estão habilitados)
            for (int i = 0; i < 5; i++)
            {
                if (VerificarLinhaHabilitada(bingoButtons,i) || verificarColunaHabilitada(bingoButtons, i) || verificarDiagonalPrincipalHabilitada(bingoButtons) 
                    || verificarDiagonalSecundariaHabilitada(bingoButtons))
                {
                    // Se encontrar uma linha ou coluna completamente habilitada, retorna true (bingo)
                    return true;
                }
            }

            // Se nenhuma linha ou coluna estiver completamente habilitada, retorna false
            return false;
        }
        private bool verificarBingoComputer()
        {
            // Verifica se alguma linha ou coluna está habilitada (todos os botões estão habilitados)
            for (int i = 0; i < 5; i++)
            {
                if (VerificarLinhaHabilitada(bingoButtonsComputer, i) || verificarColunaHabilitada(bingoButtonsComputer, i) || verificarDiagonalPrincipalHabilitada(bingoButtonsComputer)
                    || verificarDiagonalSecundariaHabilitada(bingoButtonsComputer))
                {
                    // Se encontrar uma linha ou coluna completamente habilitada, retorna true (bingo)
                    return true;
                }
            }

            // Se nenhuma linha ou coluna estiver completamente habilitada, retorna false
            return false;
        }

        private void InitializeTimer()
        {
            timer.Interval = 50; // Intervalo em milissegundos (5 segundos)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
            
        {
            GenerateRandomNumber();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            //jogo começado
            InitializeTimer();
            InitializeBingoBoard();
            InitializeBingoBoardComputer();
            
            GenerateRandomNumber();
            button1.Enabled = true;
            label2.Visible = true;
            button2.Enabled = false;
        }
        // Remove todos os controles da tela
        private void LimparTela()
        {
            
            foreach (Control control in Controls)
            {
                Controls.Remove(control);
                control.Dispose();
            }

            Label label = new Label();
            label.Text = "ACABOU O JOGO!";
            Controls.Add(label);
        }
    }

}

