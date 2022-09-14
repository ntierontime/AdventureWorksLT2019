using SQLite;

namespace Framework.MauiX.SQLite;

/// <summary>
/// TODO: Encrypt SQLite database in C#
/// https://stackoverflow.com/questions/1259561/encrypt-sqlite-database-in-c-sharp
/// </summary>
public class SQLiteService
{
    private SQLiteConnection _database;

    public bool Init()
    {
        return true;
    }

    public SQLiteConnection GetDatabase(string databasePath)
    {
        return _database ?? (_database = new SQLiteConnection(databasePath));
    }

    public void DeleteDatabase()
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        DeleteDatabase(Path.Combine(path, "LocalSQLite.db3"));
    }

    public void DeleteDatabase(string databasePath)
    {
        //try
        //{
        //    _database.Close();
        //}
        //catch //(Exception ex)
        //{

        //}

        try
        {
            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }

            GetDatabase(databasePath);
        }
        catch //(Exception ex)
        {

        }
    }
}

