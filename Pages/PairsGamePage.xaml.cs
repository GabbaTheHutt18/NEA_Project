using NEA_Project.ViewModels;
using NEA_Project.Commands;
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

namespace NEA_Project.Pages
{
    /// <summary>
    /// Interaction logic for PairsGamePage.xaml
    /// </summary>
    public partial class PairsGamePage : UserControl
    {
        public static readonly DependencyProperty CheckPairCommandProperty = DependencyProperty.Register("CheckPairCommand",
            typeof(ICommand), typeof(PairsGamePage), new PropertyMetadata(null));

        public ICommand CheckPairCommand
        {
            get { return (ICommand)GetValue(CheckPairCommandProperty); }
            set { SetValue(CheckPairCommandProperty, value); }
        }

        MainWindowViewModel _parent = new MainWindowViewModel();


        public List<TextBlock> textblocks = new List<TextBlock>();
        public int whichElement;
        List<string[]> test = new List<string[]>();
        public PairsGamePage()
        {
            InitializeComponent();
            var vm = new PairsGameViewModel(_parent);
            this.DataContext = vm;
            CreateTextBlocks();
        }
        public static readonly DependencyProperty TextBlockContainsProperty =
            DependencyProperty.Register("TextBlockContains", typeof(string), typeof(PairsGamePage),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string TextBlockContains
        {
            get { return (string)GetValue(TextBlockContainsProperty); }
            set { SetValue(TextBlockContainsProperty, value); }
        }


        public static readonly DependencyProperty TextBlock2ContainsProperty =
            DependencyProperty.Register("TextBlock2Contains", typeof(string), typeof(PairsGamePage),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string TextBlock2Contains
        {
            get { return (string)GetValue(TextBlock2ContainsProperty); }
            set { SetValue(TextBlock2ContainsProperty, value); }
        }

        public static readonly DependencyProperty PairFoundProperty =
            DependencyProperty.Register("PairFound", typeof(bool), typeof(PairsGamePage),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool PairFound
        {
            get { return (bool)GetValue(PairFoundProperty); }
            set { SetValue(PairFoundProperty, value); }
        }

        private void CreateTextBlocks()
        {

            Random random = new Random();
            PairsGameViewModel myDataObject = new PairsGameViewModel(_parent);
            Binding Question = new Binding("Question");
            Question.Source = myDataObject;
            Binding Answer = new Binding("Answer");
            Answer.Source = myDataObject;

            for (int i = 0; i < 2; i++)
            {
                myDataObject.Change = true;
                int randomNum = random.Next(0, test.Count);
                TextBlock textblock = new TextBlock();
                TextBlock textblock1 = new TextBlock();
                textblock.SetBinding(TextBlock.TextProperty, Question);
                textblock1.SetBinding(TextBlock.TextProperty, Answer);
                textblock.Width = 75;
                textblock1.Width = 75;
                textblock1.Height = 75;
                textblock.Height = 75;
                canvas.Children.Add(textblock);
                canvas.Children.Add(textblock1);
                textblocks.Add(textblock);
                textblocks.Add(textblock1);
                myDataObject.Change = false;
            }



            for (int i = 0; i < textblocks.Count; i++)
            {

                textblocks[i].MouseMove += MouseMove;

            }

        }

        private void GetElement()
        {
            for (int i = 0; i < textblocks.Count; i++)
            {
                if (textblocks[i].IsMouseOver)
                {

                    whichElement = i;

                }

            }
        }



        private void canvas_DragOver(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(canvas);
            GetElement();
            Canvas.SetLeft(textblocks[whichElement], dropPosition.X);
            Canvas.SetTop(textblocks[whichElement], dropPosition.Y);


        }



        private void MouseMove(object sender, MouseEventArgs e)
        {
            GetElement();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(textblocks[whichElement], textblocks[whichElement], DragDropEffects.Move);

            }
        }

        private void canvas_Drop(object sender, DragEventArgs e)
        {
            Rect Circlehitbox = new Rect(Canvas.GetLeft(textblocks[whichElement]), Canvas.GetTop(textblocks[whichElement]), textblocks[whichElement].Width, textblocks[whichElement].Height);

            for (int i = 0; i < textblocks.Count; i++)
            {
                if (textblocks[whichElement] != textblocks[i])
                {

                    Rect Rectanglehitbox = new Rect(Canvas.GetLeft(textblocks[i]), Canvas.GetTop(textblocks[i]), textblocks[i].Width, textblocks[i].Height);
                    if (Circlehitbox.IntersectsWith(Rectanglehitbox))
                    {
                        TextBlockContains = textblocks[i].Text;
                        TextBlock2Contains = textblocks[whichElement].Text;
                        CheckPairCommand?.Execute(null);
                        if (PairFound)
                        {
                            canvas.Children.Remove(textblocks[i]);
                            canvas.Children.Remove(textblocks[whichElement]);
                        }
                        if (canvas.Children.Count == 1)
                        {
                            MessageBox.Show("Hello");
                        }

                    }
                }

            }

        }
    }
}
