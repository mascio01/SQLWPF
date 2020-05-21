using SQLite;
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

namespace mascella.nicolo._5g.SQLWPF
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }
        SQLiteConnection cn1 = new SQLiteConnection("chinook.db");
        private void tutti_gli_album_Click(object sender, RoutedEventArgs e)
        {


            List<Record> retVal = cn1.Query<Record>("select * from albums");
            dt_grid.ItemsSource = retVal;
        }

        private void order_of_album_Click(object sender, RoutedEventArgs e)
        {
            List<Record> retVal = cn1.Query<Record>("select Name from artists INNER JOIN Albums ON Albums.ArtistId == artists.ArtistId order By albums.Title");

            dt_grid.ItemsSource = retVal;
        }

        private void number_of_tracks_Click(object sender, RoutedEventArgs e)
        {
            List<Record> retVal = cn1.Query<Record>("select Name from artists INNER JOIN Albums ON Albums.ArtistId == artists.ArtistId order By artists.Name");
            dt_grid.ItemsSource = retVal;
        }

        private void order_of_artist_Click(object sender, RoutedEventArgs e)
        {
            List<Record> retVal = cn1.Query<Record>("select count(tracks.AlbumId) as nTracks, albums.Title from albums INNER JOIN tracks ON albums.AlbumId == tracks.AlbumId GROUP by tracks.AlbumId order by nTracks desc");
            dt_grid.ItemsSource = retVal;
        }

        private void artist_and_album_Click(object sender, RoutedEventArgs e)
        {
            List<Record> retVal = cn1.Query<Record>("select name, count(albums.ArtistId) as nAlbums from artists inner join albums ON albums.ArtistId == artists.ArtistId GROUP by artists.Name order by nAlbums Desc");
            dt_grid.ItemsSource = retVal;
        }
        public class Record
        {
            public int AlbumId { get; set; }
            public string Title { get; set; }
            public int ArtistId { get; set; }
            public string Name { get; set; }
            public string NTracks { get; set; }
            public string NAlbums { get; set; }
        }
    }
}
