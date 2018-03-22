using System.Collections.Generic;
using System.Linq;
using FOConverter.scr;
using FOConverter.scr.Common;
using FOConverter.scr.Converters.F3;
using FOConverter.scr.Esm;

namespace FOConverter.Properties
{
    public class Main
    {
        private Configuration config;
        private Dictionary<string, Dictionary<string, EsmDatabaseFile>> games;

        public Main(Configuration config)
        {
            this.config = config;
        }

        public void ReadFiles()
        {
            games = new Dictionary<string, Dictionary<string, EsmDatabaseFile>>();
            foreach (var game in config.ConvertConfig.games)
            {
                Dictionary<string, EsmDatabaseFile> files = new Dictionary<string, EsmDatabaseFile>();
                foreach (var file in game.files)
                {
                    var esmBase = new EsmDatabaseFile();
                    esmBase.Read(PathBuilder.getPath(config, game.name, file));
                    files.Add(file, esmBase);
                }

                games.Add(game.name, files);
            }
        }

        public void Convert()
        {
            var converter = new Converter();
            foreach (var game in games.Keys)
            {
                foreach (var fileName in games[game].Keys)
                {
                    foreach (var topGrouKey in games[game][fileName].topLevelGroups.Keys)
                    {
                        var file = games[game][fileName];
                        foreach (var record in file.topLevelGroups[topGrouKey].ChildRecords)
                        {
                            var convertedRecord = converter.Convert(record, game);
                        }
                    }
                }
            }
        }
    }
}