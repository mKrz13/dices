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
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace X_dice_X
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<Image> dicesImg;
        private bool[] dicesToReroll;
        private Random random;
        private Player player1, player2;

        private int playerTurn = 1;
        
        
        private int roundNumber = 1;
        private int turnNumber = 1;
        //public int rollNumber = 0;

        //public int rollNumber { get {} set { rollNumber = value;  lblRoll.Content = value.ToString(); } }
        private int RollNumber= 0;

        private Label p1SumSchool;
        private Label p2SumSchool;
        private Label p1SumOther;
        private Label p2SumOther;
        private Label p1Total;
        private Label p2Total;

        private Label p1Premium;
        private Label p2Premium;

        private bool enteredMouse = false;
        private Brush tempBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFACCFEE");
        MediaPlayer mediaPlayer = new MediaPlayer();





        public int rollNumber
        {
            get { return RollNumber; }
            set { RollNumber = value;
                lblRoll.Content = "Rzut nr: "+ rollNumber.ToString();
            }
        }

        private int[] dicesResult;

        public MainWindow()
        {
            InitializeComponent();
            CreateGameBackend();
            resetDices();
            this.DataContext = this;



 

        }

        private void CreateGameBackend()
        {
            dicesImg = new ObservableCollection<Image>() { imgD1 as Image, imgD2 as Image, imgD3 as Image, imgD4 as Image, imgD5 as Image };
            dicesToReroll = new bool[5];
            random = new Random();
            player1 = new Player(random);
            player2 = new Player(random);
            lblChooseDicesToReroll.Visibility = Visibility.Hidden;


            p1SumSchool = p1SumSchool1;
            p2SumSchool = p2SumSchool1;


            p1SumOther = p1SumOther1;
            p2SumOther = p2SumOther1;


            p1Total = p1Total1;
            p2Total = p2Total1;

            p1Premium = p1Premiun1;
            p2Premium = p2Premiun1;


        }

        private void btnRollTheDices_Click(object sender, RoutedEventArgs e)
        {

            if (rollNumber==3)
            {
                lblChooseDicesToReroll.Visibility = Visibility.Hidden;
            }
            else
            {
                lblChooseDicesToReroll.Visibility = Visibility.Visible;
            }


            if (rollNumber>2)
            {
                return;
            }

            if (playerTurn==1)
            {
                dicesResult = player1.RollTheDices(dicesToReroll, dicesResult);
            }
            else
            {
                dicesResult = player2.RollTheDices(dicesToReroll, dicesResult);
            }


            //check for no cicked dices to reroll


            bool shouldIreroll = false;


            for (int i = 0; i < 5; i++)
            {
                if (dicesToReroll[i])
                {
                    shouldIreroll = true;
                    break;
                }

            }

            if (shouldIreroll==false)
            {
                MessageBox.Show("Wybierz kości do wymiany", "Uwaga", MessageBoxButton.OK ,MessageBoxImage.Information);
                return;

            }








            for (int i = 0; i < 5; i++)
            {
                switch (dicesResult[i])
                {
                    case 1:
                        dicesImg[i].Source = new BitmapImage(new Uri("/Dices/1.png", UriKind.Relative));
                        break;
                    case 2:
                        dicesImg[i].Source = new BitmapImage(new Uri("/Dices/2.png", UriKind.Relative));
                        break;
                    case 3:
                        dicesImg[i].Source = new BitmapImage(new Uri("/Dices/3.png", UriKind.Relative));
                        break;
                    case 4:
                        dicesImg[i].Source = new BitmapImage(new Uri("/Dices/4.png", UriKind.Relative));
                        break;
                    case 5:
                        dicesImg[i].Source = new BitmapImage(new Uri("/Dices/5.png", UriKind.Relative));
                        break;
                    case 6:
                        dicesImg[i].Source = new BitmapImage(new Uri("/Dices/6.png", UriKind.Relative));
                        break;
                    default:
                        break;
                }
            }


            for (int i = 0; i < 5; i++)
            {
                dicesToReroll[i] = false;
            }

            rollNumber++;

            if (rollNumber == 3)
            {
                lblChooseDicesToReroll.Visibility = Visibility.Hidden;
            }
            else
            {
                lblChooseDicesToReroll.Visibility = Visibility.Visible;
            }






        }

        private void scoreLabelClick(object sender, MouseButtonEventArgs e)
        {

            if (rollNumber==0)
            {
                return;
            }


            int points = 0;
            Label lbl = sender as Label;
            int row = Grid.GetRow(lbl);
            int column = Grid.GetColumn(lbl);

            int desiredColumn = 1;

            //MessageBox.Show(turnNumber.ToString());

            

            if (turnNumber == 31)
            {
                roundNumber++;
                turnNumber = 1;


                player1.OthersCount = 0;
                player2.OthersCount = 0;
                player1.SchoolCount = 0;
                player2.SchoolCount = 0;




            }




            if (playerTurn==1)
            {
                if (roundNumber==1)
                {
                    desiredColumn = 1;
                }
                else if (roundNumber==2)
                {
                    desiredColumn = 2;
                    p1SumSchool = p1SumSchool2;
                    p1SumOther = p1SumOther2;
                    p1Total = p1Total2;
                    p1Premium = p1Premiun2;
                }
                else
                {
                    desiredColumn = 3;
                    p1SumSchool = p1SumSchool3;
                    p1SumOther = p1SumOther3;
                    p1Total = p1Total3;
                    p1Premium = p1Premiun3;
                }
            }
            else
            {
                if (roundNumber == 1)
                {
                    desiredColumn = 4;
                }
                else if (roundNumber==2)
                {
                    desiredColumn = 5;
                    p2SumSchool = p2SumSchool2;
                    p2SumOther = p2SumOther2;
                    p2Total = p2Total2;
                    p2Premium = p2Premiun2;
                }
                else
                {
                    desiredColumn = 6;
                    p2SumSchool = p2SumSchool3;
                    p2SumOther = p2SumOther3;
                    p2Total = p2Total3;
                    p2Premium = p2Premiun3;
                }

            }




            if (lbl.Content is null &&column==desiredColumn)
            {
                switch (row)
                {
                    case 2:
                        iterateSchool(1, out points);
                        if (points ==5)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 3:
                        iterateSchool(2, out points);
                        if (points == 10)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 4:
                        iterateSchool(3, out points);
                        if (points == 15)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 5:
                        iterateSchool(4, out points);
                        if (points == 20)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 6:
                        iterateSchool(5, out points);
                        if (points == 25)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 7:
                        iterateSchool(6, out points);
                        if (points == 30)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 10:
                        threeOrFourOfAKind(3, out points);
                        if (points == 18)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 11:
                        threeOrFourOfAKind(4, out points);
                        if (points == 24)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 12:
                        fullHouse(out points);
                        if (points == 25)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 13:
                        smallStreet(out points);
                        if (points == 30)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 14:
                        grandStreet(out points);
                        if (points == 40)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 15:
                        general(out points);
                        if (points == 50)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 16:
                        chance(out points);
                        if (points == 30)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 17:
                        equal(out points);
                        if (points == 30)
                        {
                            paintGold(sender);
                        }
                        break;
                    case 18:
                        odd(out points);
                        if (points == 25)
                        {
                            paintGold(sender);
                        }
                        break;
                    default:
                        return;
                }


                lbl.Content = points;
                lblChooseDicesToReroll.Visibility = Visibility.Hidden;

                
                //end player turn
                if (playerTurn==1)
                {

                    player1.Bank += points;

                    if (row<=7)
                    {
                        player1.SchoolCount += points;
                        p1SumSchool.Content = player1.SchoolCount.ToString();
                    }
                    else
                    {
                        player1.OthersCount += points;
                        p1SumOther.Content = player1.OthersCount;
                    }

                    if (p1Premium.Content is null)
                    {
                        p1Total.Content = (player1.SchoolCount + player1.OthersCount).ToString();
                    }
                    else
                    {
                        p1Total.Content = (player1.SchoolCount + player1.OthersCount+35).ToString();
                    }
                    

                    lblMove.Content = "Ruch gracz 2";
                    playerTurn = 2;
                }
                else
                {
                    player2.Bank += points;
                    if (row <=7)
                    {
                        player2.SchoolCount += points;
                        p2SumSchool.Content = player2.SchoolCount.ToString();

                    }
                    else
                    {
                        player2.OthersCount += points;
                        p2SumOther.Content = player2.OthersCount;
                    }

                    if (p2Premium.Content is null)
                    {
                        p2Total.Content = (player2.SchoolCount + player2.OthersCount).ToString();
                    }
                    else
                    {
                        p2Total.Content = (player2.SchoolCount + player2.OthersCount+ 35).ToString();

                    }

                    lblMove.Content = "Ruch gracz 1";
                    playerTurn = 1;
                }

                turnNumber++;
                rollNumber = 0;


                checkForPremium();
                p1Score.Content = player1.Bank + " pkt.";
                p2Score.Content = player2.Bank + " pkt.";
                resetDices();

                if (roundNumber == 3 && turnNumber == 31)
                {
                    MessageBox.Show("koniec gry!\n gracz 1 zdobył: " + player1.Bank +"\n gracz 2 zdobył: "+player2.Bank);
                    btnRollTheDices.IsEnabled = false;
                    return;
                }


            }

        }


        private void checkForPremium()
        {

                if (player1.SchoolCount >= 63 && p1Premium.Content is null)
                {
                    p1Premium.Content = "35";
                    player1.Bank += 35;
                    p1Total.Content = (player1.SchoolCount + player1.OthersCount+35).ToString();
                }
                if (player2.SchoolCount >= 63 && p2Premium.Content is null)
                {
                    p2Premium.Content = "35";
                    player2.Bank += 35;
                    p2Total.Content = (player2.SchoolCount + player2.OthersCount+35).ToString();
            }


        }





        //school backend
        private void iterateSchool(int desiredNumber,out int points)
        {
            points = 0;
            for (int i = 0; i < 5; i++)
            {
                if (dicesResult[i] == desiredNumber)
                {
                    points += desiredNumber;
                }
            }
        }
        //3-jednakowe backend
        private void threeOrFourOfAKind(int min, out int points)
        {
            points = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < 5; i++)
            {
                if(dic.ContainsKey(dicesResult[i]))
                {
                    dic[dicesResult[i]]++;
                }
                else
                {
                    dic.Add((dicesResult[i]), 1);
                }
            }

            foreach (var item in dic)
            {
                if (item.Value>=min)
                {
                    points = item.Key * min;
                }
            }
        }

        private void fullHouse(out int points)
        {
            points = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < 5; i++)
            {
                if (dic.ContainsKey(dicesResult[i]))
                {
                    dic[dicesResult[i]]++;
                }
                else
                {
                    dic.Add((dicesResult[i]), 1);
                }
            }

            foreach (var item in dic)
            {
                if (item.Value == 3)
                {
                    foreach (var item2 in dic)
                    {
                        if (item2.Value == 2)
                        {
                            points = 25;
                            break;
                        }
                    }
                }
            }
        }

        private void smallStreet(out int points)
        {
            points = 0;

            if (dicesResult[0]==1&&dicesResult[1]==2&&dicesResult[2]==3&&dicesResult[3]==4)
            {
                points = 30;
                return;
            }

            if (dicesResult[0] == 2 && dicesResult[1] == 3 && dicesResult[2] == 4 && dicesResult[3] == 5)
            {
                points = 30;
                return;
            }

            if (dicesResult[0] == 3 && dicesResult[1] == 4 && dicesResult[2] == 5 && dicesResult[3] == 6)
            {
                points = 30;
                return;
            }


            if (dicesResult[1] == 1 && dicesResult[2] == 2 && dicesResult[3] == 3 && dicesResult[4] == 4)
            {
                points = 30;
                return;
            }
            if (dicesResult[1] == 2 && dicesResult[2] == 3 && dicesResult[3] == 4 && dicesResult[4] == 5)
            {
                points = 30;
                return;
            }
            if (dicesResult[1] == 3 && dicesResult[2] == 4 && dicesResult[3] == 5 && dicesResult[4] == 6)
            {
                points = 30;
                return;
            }
        }

        private void grandStreet(out int points)
        {
            points = 0;


            if (dicesResult[0] == 1 && dicesResult[1] == 2 && dicesResult[2] == 3 && dicesResult[3] == 4&& dicesResult[4]==5)
            {
                points = 40;
                return;
            }

            if (dicesResult[0] == 2 && dicesResult[1] == 3 && dicesResult[2] == 4 && dicesResult[3] == 5 && dicesResult[4] == 6)
            {
                points = 40;
                return;
            }






        }

        private void general(out int points)
        {
            points = 0;

            for (int i = 1; i < 5; i++)
            {
                if (dicesResult[i]!=dicesResult[0])
                {
                    return;
                }
            }

            points = 50;

        }

        private void chance(out int points)
        {
            points = 0;
            foreach (var item in dicesResult)
            {
                points += item;
            }
        }

        private void equal(out int points)
        {
            points = 0;

            for (int i = 0; i < 5; i++)
            {
                if (dicesResult[i]%2==0)
                {
                    points += dicesResult[i];
                }
            }
        }
        private void odd(out int points)
        {
            points = 0;
            for (int i = 0; i < 5; i++)
            {
                if (dicesResult[i] == 1 || dicesResult[i] == 3 || dicesResult[i] == 5)
                {
                    points += dicesResult[i];
                }
            }

        }


        //Action performed att the begining of turn

        private void setDicesToReroll(int diceNumber, Image img)
        {
            if (dicesToReroll[diceNumber] == false)
            {
                dicesToReroll[diceNumber] = true;

                switch (dicesResult[diceNumber])
                {
                    case 1:
                        img.Source = new BitmapImage(new Uri("/Dices/1s.png", UriKind.Relative));
                        break;
                    case 2:
                        img.Source = new BitmapImage(new Uri("/Dices/2s.png", UriKind.Relative));
                        break;
                    case 3:
                        img.Source = new BitmapImage(new Uri("/Dices/3s.png", UriKind.Relative));
                        break;
                    case 4:
                        img.Source = new BitmapImage(new Uri("/Dices/4s.png", UriKind.Relative));
                        break;
                    case 5:
                        img.Source = new BitmapImage(new Uri("/Dices/5s.png", UriKind.Relative));
                        break;
                    case 6:
                        img.Source = new BitmapImage(new Uri("/Dices/6s.png", UriKind.Relative));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                dicesToReroll[diceNumber] = false;
                switch (dicesResult[diceNumber])
                {
                    case 1:
                        img.Source = new BitmapImage(new Uri("/Dices/1.png", UriKind.Relative));
                        break;
                    case 2:
                        img.Source = new BitmapImage(new Uri("/Dices/2.png", UriKind.Relative));
                        break;
                    case 3:
                        img.Source = new BitmapImage(new Uri("/Dices/3.png", UriKind.Relative));
                        break;
                    case 4:
                        img.Source = new BitmapImage(new Uri("/Dices/4.png", UriKind.Relative));
                        break;
                    case 5:
                        img.Source = new BitmapImage(new Uri("/Dices/5.png", UriKind.Relative));
                        break;
                    case 6:
                        img.Source = new BitmapImage(new Uri("/Dices/6.png", UriKind.Relative));
                        break;
                    default:
                        break;
                }
            }
        }
        private void imgD1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rollNumber==0||rollNumber==3)
            {
                return;
            }
            Image img = sender as Image;
            setDicesToReroll(0, img);
        }

        private void imgD2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rollNumber == 0 || rollNumber == 3)
            {
                return;
            }
            Image img = sender as Image;
            setDicesToReroll(1, img);
        }

        private void imgD3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rollNumber == 0 || rollNumber == 3)
            {
                return;
            }
            Image img = sender as Image;
            setDicesToReroll(2, img);
        }

        private void imgD4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rollNumber == 0 || rollNumber == 3)
            {
                return;
            }
            Image img = sender as Image;
            setDicesToReroll(3, img);
        }

        private void imgD5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (rollNumber == 0 || rollNumber == 3)
            {
                return;
            }
            Image img = sender as Image;
            setDicesToReroll(4, img);
        }

        private void resetDices()
        {
            for (int i = 0; i < 5; i++)
            {
                dicesImg[i].Source = new BitmapImage(new Uri("/Dices/0.png", UriKind.Relative));
                dicesToReroll[i] = true;
            }
        }
        private void mouseEnter(object sender, System.EventArgs e)
        {
            if (enteredMouse == false && ((sender as Label).Content is null)&& (sender as Label).Background != (SolidColorBrush)new BrushConverter().ConvertFromString("Gold"))
            {
                Label sndr = sender as Label;
                //tempBrush = sndr.Background;
                sndr.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFAAAFEE");
                enteredMouse = true;
            }

        }

        private void mouseLeave(object sender, System.EventArgs e)
        {
            if ((sender as Label).Background != (SolidColorBrush)new BrushConverter().ConvertFromString("Gold"))
            {
                enteredMouse = false;
                Label sndr = sender as Label;
                sndr.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFACCFEE");
            }

        }

        private void paintGold(object sender)
        {
            Label sndr = sender as Label;
            sndr.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("Gold");

            //  mediaPlayer.Open(new Uri(@"/Sounds/coins.m4a", UriKind.Relative));

            //string path = ;
            mediaPlayer.Open(new Uri(System.Environment.CurrentDirectory + @"\Sounds\coins.m4a", UriKind.Relative));
            //mediaPlayer.Open(new Uri("/Dices/coins.m4a", UriKind.Relative));
            mediaPlayer.Play();
            
            /*
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(openFileDialog.FileName));
                mediaPlayer.Play();
            }*/
        }

    }
}
