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
        private int rollNumber = 0;
        private Player player1, player2;

        private int[] dicesResult;

        private int lblSumSchool1Integer = 0;


        public MainWindow()
        {
            InitializeComponent();
            CreateGameBackend();
            ResetDices();
 

        }

        private void CreateGameBackend()
        {
            dicesImg = new ObservableCollection<Image>() { imgD1 as Image, imgD2 as Image, imgD3 as Image, imgD4 as Image, imgD5 as Image };
            dicesToReroll = new bool[5];
            random = new Random();
            player1 = new Player(random);
            player2 = new Player(random);


        }

        private void btnRollTheDices_Click(object sender, RoutedEventArgs e)
        {
            dicesResult = player1.RollTheDices(dicesToReroll);

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


        }

        private void scoreLabelClick(object sender, MouseButtonEventArgs e)
        {
            int points = 0;
            Label lbl = sender as Label;
            int row = Grid.GetRow(lbl);
            int column = Grid.GetColumn(lbl);






            if (lbl.Content is null)
            {
                switch (row)
                {
                    case 2:
                        //jedynki
                        iterateSchool(1, out points);
                        lbl.Content = points;
                        break;
                    case 3:
                        iterateSchool(2, out points);
                        lbl.Content = points;
                        break;
                    case 4:
                        iterateSchool(3, out points);
                        lbl.Content = points;
                        break;
                    case 5:
                        iterateSchool(4, out points);
                        lbl.Content = points;
                        break;
                    case 6:
                        iterateSchool(5, out points);
                        lbl.Content = points;
                        break;
                    case 7:
                        iterateSchool(6, out points);
                        lbl.Content = points;
                        break;
                    default:
                        break;
                }
            }

            if (column == 1)
            {
                lblSumSchool1Integer += points;
                lblSumSchool1.Content = lblSumSchool1Integer.ToString();
            }

            








        }

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



        private void ResetDices()
        {
            for (int i = 0; i < 5; i++)
            {
                dicesImg[i].Source = new BitmapImage(new Uri("/Dices/0.png", UriKind.Relative));
                dicesToReroll[i] = true;
            }
        }





    }
}
