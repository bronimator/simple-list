using Newtonsoft.Json;

namespace UserListTestApp.Models
{
    public class DataSource
    {
        public int SourceType { get; set; }

        public static int GetCurrentSourceTypeInt ()
        {
            var config = JsonConvert.DeserializeObject<DataSource>(File.ReadAllText("config.json"));

            return config != null ? config.SourceType : 0;
        }

        public static Source GetCurrentSourceType()
        {
            return (Source)GetCurrentSourceTypeInt();
        }

        public static void SetSourceType(int source)
        {
            File.WriteAllText("config.json", JsonConvert.SerializeObject(new DataSource()
            {
                SourceType = source,
            }));
        }
    }

    public enum Source
    {
        DataBase,

        File,
    }
}
