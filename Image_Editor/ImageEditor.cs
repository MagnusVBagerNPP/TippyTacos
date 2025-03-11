using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Tmds.DBus.Protocol;

#nullable enable


namespace ImageEditor
{
    public class ImageEditor
    {
        private Window win;
        private int load_state;
        private Button[,] btnGrid; // this is the array, where the bitmap will be stored
        private Button load_bitmap;
        private Button save_bitmap;
        private Button flip_bitmap;
        private NumericUpDown UpDownPx;
        private NumericUpDown UpDownPy;
        private BitGrid initialBitGrid;
        private Cell[,] cell;

        public int image { get; set; }
        private Grid grid_panel;
        public int grid_width { get; set; }
        public int grid_height { get; set; }
        private StackPanel grid_Panel;
        private Grid grid;
        private int margin;
        private int max_grid_height;
        private int max_grid_width;
        private double button_size;
        public int grid_cols;
        public int grid_rows;

        public ImageEditor()
        {
            // setup
            win = new Window
            {
                Title = "Image-Editor",
                Width = 400,
                Height = 800,
                CanResize = false,
            };
            grid_height = 10; // Example for startup of program
            grid_width = 10;
            load_state = 0;

            grid_rows = 5; // Example for startup of program
            grid_cols = 5;
            button_size = 40;
            margin = 10;

            //max grid sizing
            max_grid_height = (int)(win.Height / 2) - (4 * margin);
            max_grid_width = (int)win.Width - (4 * margin);

            // Creating of Gui Layout with alignment
            var mainPanel = CreatePanel("vertical", "White");
            var load_save_flipPanel = CreatePanel("horizontal", "White");
            var edit_PixelPanel = CreatePanel("horizontal", "White");
            //var grid_Panel = CreatePanel("horizontal", "Green");
            grid_Panel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(margin),
                Spacing = 10,
                RenderTransform = new ScaleTransform(1, 1),
                Background = Brushes.White,
            };

            // Add everything to the grid panel
            grid = new Grid
            {
                Height = 360,
                Width = 360,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = Brushes.White,
            };

            // load the smiley to initialize grid
            initialBitGrid = new BitGrid(grid_cols, grid_rows);
            cell = initialBitGrid.Load("./smile.b2img.txt");
            grid_rows = initialBitGrid.Height;
            grid_cols = initialBitGrid.Width;
            load_state = 1;
            Button[,] btnGrid = populateGrid();

            // Button creation, event handling and adding to panel
            load_bitmap = CreateButton("Load");
            save_bitmap = CreateButton("Save");
            flip_bitmap = CreateButton("Flip");
            UpDownPx = CreateNumericUpDown(grid_cols);
            UpDownPy = CreateNumericUpDown(grid_cols);

            load_bitmap.Click += Load_Button_Click;
            save_bitmap.Click += Save_Button_Click;
            flip_bitmap.Click += Flip_Bitmap_Button_Click;
            UpDownPx.ValueChanged += UpDownPx_ValueChanged;
            UpDownPy.ValueChanged += UpDownPy_ValueChanged;

            load_save_flipPanel.Children.Add(load_bitmap);
            load_save_flipPanel.Children.Add(save_bitmap);
            load_save_flipPanel.Children.Add(flip_bitmap);
            edit_PixelPanel.Children.Add(UpDownPx);
            edit_PixelPanel.Children.Add(UpDownPy);
            var gridContainer = new Border
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Child = grid,
            };

            grid_Panel.Children.Add(gridContainer);

            // TextBox for Image name
            var nameLabel = new TextBlock
            {
                Text = "Name of Img", //TODO introduce logic for changing name after loading a file
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            // Add everything to the main panel
            mainPanel.Children.Add(nameLabel);
            mainPanel.Children.Add(grid_Panel);
            //mainPanel.Children.Add(edit_PixelPanel); // doesnt work yet with load and save
            mainPanel.Children.Add(load_save_flipPanel);

            // Set window content
            win.Content = mainPanel;
            win.Show();
        }

        public Button[,] populateGrid()
        {
            grid.Children.Clear(); // Clear old buttons
            grid.ColumnDefinitions.Clear(); // Clear old columns
            grid.RowDefinitions.Clear(); // Clear old rows

            // Update grid size

            btnGrid = new Button[grid_cols, grid_rows];

            // Define new columns and rows

            for (int i = 0; i < grid_cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            }

            for (int j = 0; j < grid_height; j++)
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            //calculating buttuns to rezise
            button_size = Math.Min(max_grid_width / grid_rows, max_grid_height / grid_cols);

            IBrush? background;
            // Add new buttons
            background = Brushes.Gray;
            for (int i = 0; i < grid_cols; i++)
            {
                for (int j = 0; j < grid_rows; j++)
                {
                    //Console.WriteLine(cell[i, j].IsColored);
                    //char cellValue = cell[i, j].IsColored;
                    //Console.WriteLine(cellValue);
                    if (load_state == 1)
                    {
                        if (cell[j, i].IsColored == '1')
                        {
                            background = Brushes.Black;
                            Console.WriteLine("1");
                        }
                        else
                        {
                            background = Brushes.Gray;

                            Console.WriteLine("0");
                        }
                    }
                    btnGrid[i, j] = new Button
                    {
                        Background = background,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = button_size,
                        Height = button_size,
                        BorderThickness = new Thickness(0.05 * button_size),
                    };

                    btnGrid[i, j].Click += Grid_Button_Click;

                    Grid.SetColumn(btnGrid[i, j], i);
                    Grid.SetRow(btnGrid[i, j], j);

                    grid.Children.Add(btnGrid[i, j]);
                }
                Console.Write('\n');
            }
            Console.WriteLine($@"{button_size * grid_cols}");
            return btnGrid;
        }

        private void Grid_Button_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                btn.Background = btn.Background == Brushes.Gray ? Brushes.Black : Brushes.Gray;
                //TODO add logic for changing the representative values in array
            }
        }

        private async void Save_Button_Click(object? sender, RoutedEventArgs e)
        {
            //TODO create logic for saving
            var file = await FileSaverHelper.SaveFilePicker(win);

            grid_cols = initialBitGrid.Width;
            grid_rows = initialBitGrid.Height;

            Console.WriteLine(grid_cols);
            Console.WriteLine(grid_rows);

            for (int i = 0; i < grid_rows; i++)
            {
                for (int j = 0; j < grid_cols; j++)
                {
                    //Console.WriteLine($"{grid_rows},{grid_cols}");

                    if (btnGrid[j, i].Background == Brushes.Black)
                    {
                        cell[i, j].IsColored = '1';
                    }
                    else
                    {
                        cell[i, j].IsColored = '0';
                    }
                }
            }

            Console.WriteLine(btnGrid[0, 0].Background);
            Console.WriteLine(btnGrid[1, 0].Background);
            Console.WriteLine(btnGrid[0, 1].Background);
            Console.WriteLine(btnGrid[1, 1].Background);
            Console.WriteLine(btnGrid[0, 2].Background);
            Console.WriteLine(btnGrid[1, 2].Background);

            initialBitGrid.Height = grid_rows;
            initialBitGrid.Width = grid_cols;
            initialBitGrid.bitmap = cell;

            if (file != null)
            {
                initialBitGrid.Save(file);
            }
            populateGrid();
        }

        private async void Load_Button_Click(object? sender, RoutedEventArgs e)
        {
            //TODO create logic for loading^
            var files = "./smile.b2img.txt";
            if (load_state == 1)
            {
                files = await FilePickerHelper.OpenFilePicker(win);
            }
            if (files.Length > 0)
            {
                cell = initialBitGrid.Load(files);
                load_state = 1;
            }
            grid_rows = initialBitGrid.Height;
            grid_cols = initialBitGrid.Width;

            populateGrid();
        }

        private void Flip_Bitmap_Button_Click(object? sender, RoutedEventArgs e)
        {
            {
                for (int i = 0; i < grid_rows; i++)
                {
                    for (int j = 0; j < grid_cols / 2; j++)
                    {
                        char place_holder = cell[i, j].IsColored;
                        cell[i, j].IsColored = cell[i, grid_cols - 1 - j].IsColored;
                        cell[i, grid_cols - 1 - j].IsColored = place_holder;
                    }
                }
                populateGrid();
            }
        }

        private void UpDownPx_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
        {
            grid_cols = (int)(UpDownPx.Value ?? 10); // Default to 10 if null
            populateGrid();
        }

        private void UpDownPy_ValueChanged(object? sender, NumericUpDownValueChangedEventArgs e)
        {
            grid_rows = (int)(UpDownPy.Value ?? 10); // Default to 10 if null
            populateGrid();
        }

        Button CreateButton(string buttonText)
        {
            var button = new Button
            {
                Background = Brushes.WhiteSmoke,
                Width = 100,
                Height = 50,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Content = new TextBlock
                {
                    Text = buttonText,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                },
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                CornerRadius = new CornerRadius(10),
            };
            return button;
        }

        NumericUpDown CreateNumericUpDown(int value)
        {
            var numericUpDown = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 100,
                Value = value,
            };
            return numericUpDown;
        }

        Panel CreatePanel(string layout, string color)
        {
            // Convert the string layout to Orientation enum
            Orientation orientation =
                layout == "horizontal" ? Orientation.Horizontal : Orientation.Vertical;

            IBrush? background = color == "White" ? Brushes.White : Brushes.Green;

            var panel = new StackPanel()
            {
                Orientation = orientation,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Spacing = 10,
                //RenderTransform = new ScaleTransform(1, 1),
                Background = background,
            };
            return panel;
        }

        public Window Win => win;
    }
}
