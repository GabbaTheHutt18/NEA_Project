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

        public static readonly DependencyProperty ParentProperty =
            DependencyProperty.Register("ParentVM", typeof(MainWindowViewModel), typeof(PairsGamePage),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public MainWindowViewModel ParentVM
        {
            get { return (MainWindowViewModel)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }


        public List<TextBlock> textblocks = new List<TextBlock>();
        public int whichElement;
        List<string[]> test = new List<string[]>();
        public PairsGamePage()
        {
            InitializeComponent();
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
            //Databinding for the text blocks
            PairsGameViewModel myDataObject = new PairsGameViewModel(ParentVM);
            Binding Question = new Binding("Question");
            Question.Source = myDataObject;
            Binding Answer = new Binding("Answer");
            Answer.Source = myDataObject;
            int Pairs = 0;
            bool validNumber = false;
            if (NumOfPairs.Text != null)
            {
                //only happens if the user inputs in the text box
                try
                {
                    Pairs = Int32.Parse(NumOfPairs.Text);
                    validNumber = true;
                    //must be an int
                }
                catch (Exception)
                {

                    MessageBox.Show("Not the right data type!");
                }
            }


            if (Pairs != 0 && validNumber)
            {
                //if the user input more that 0 and it is a valid number
                for (int i = 0; i < Pairs; i++)
                {
                    int height = (int)canvas.Height;
                    int width = (int)canvas.Width;
                    myDataObject.Change = true;
                    int randomNum = random.Next(0, test.Count);
                    int randomLeft = random.Next(0, width);
                    int randomTop = random.Next(0, height);
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
                    Canvas.SetLeft(textblock, randomLeft);
                    Canvas.SetTop(textblock, randomTop);
                    myDataObject.Change = false;
                }

                for (int i = 0; i < textblocks.Count; i++)
                {
                    textblocks[i].MouseMove += MouseMove;
                }
            }


        }

        private void GetElement()
        {
            for (int i = 0; i < textblocks.Count; i++)
            {
                if (textblocks[i].IsMouseOver)
                {
                    //checks which element (textblock) is currently being clicked on
                    whichElement = i;
                }

            }
        }


        //event handler, changes the textblock's position based on the mouse's position
        private void canvas_DragOver(object sender, DragEventArgs e)
        {
            Point dropPosition = e.GetPosition(canvas);
            GetElement();
            Canvas.SetLeft(textblocks[whichElement], dropPosition.X);
            Canvas.SetTop(textblocks[whichElement], dropPosition.Y);
        }


        //event handler, when the left mouse button is pressed, it starts the drag and drop proccess
        private void MouseMove(object sender, MouseEventArgs e)
        {
            GetElement();
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(textblocks[whichElement], textblocks[whichElement], DragDropEffects.Move);

            }
        }

        //triggered when an item is dropped on the canvas, creates a hit box for the dragged item and then iterates through all the other 
        //textblocks, seeing if their hit box intersects with the dragged textblock, if they do the CheckPairCommand is called and if its 
        // a pair, then the textblocks are removed and the canvas is updated. if the canvas count is 1 then the user is displayed a message
        // to let them know all pairs have been found
        private void canvas_Drop(object sender, DragEventArgs e)
        {
            Rect DraggedHitBox = new Rect(Canvas.GetLeft(textblocks[whichElement]), Canvas.GetTop(textblocks[whichElement]), textblocks[whichElement].Width, textblocks[whichElement].Height);

            for (int i = 0; i < textblocks.Count -1; i++)
            {
                if (textblocks[whichElement] != textblocks[i])
                {

                    Rect PairedHitBox = new Rect(Canvas.GetLeft(textblocks[i]), Canvas.GetTop(textblocks[i]), textblocks[i].Width, textblocks[i].Height);
                    if (DraggedHitBox.IntersectsWith(PairedHitBox))
                    {
                        TextBlockContains = textblocks[i].Text;
                        TextBlock2Contains = textblocks[whichElement].Text;
                        CheckPairCommand?.Execute(null);
                        if (PairFound)
                        {
                            TextBlock temp1 = textblocks[i];
                            TextBlock temp2 = textblocks[whichElement];
                            canvas.Children.Remove(temp1);
                            canvas.Children.Remove(temp2);
                            textblocks.Remove(temp1);
                            textblocks.Remove(temp2);
                            this.UpdateLayout();
                        }
                        if (canvas.Children.Count == 1)
                        {
                            MessageBox.Show("Finished :)");
                        }

                    }
                }

            }

        }

        //when the button is pressed, all the text boxes are removed and then new textblocks are created
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = textblocks.Count - 1; i >= 0; i--)
            {
                canvas.Children.Remove(textblocks[i]);
                textblocks.RemoveAt(i);
            }
            CreateTextBlocks();

        }
    }
}
