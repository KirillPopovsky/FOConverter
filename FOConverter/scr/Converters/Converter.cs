using System.Collections.Generic;
using FOConverter.scr.Common;
using FOConverter.scr.Records;

namespace FOConverter.scr.Converters.F3
{
    internal class Converter
    {
        private Dictionary<string, Dictionary<string, RecordConverter>> hub;

        public Converter()
        {
            var f3 = new Dictionary<string, RecordConverter>();
            f3.Add("default", new DefaultRecordConverter());

            var fnv = new Dictionary<string, RecordConverter>();
            fnv.Add("default", new DefaultRecordConverter());

            hub = new Dictionary<string, Dictionary<string, RecordConverter>>();
            hub.Add("Fallout3", f3);
            hub.Add("FalloutNV", fnv);

            // add here new converters
        }

        private RecordConverter getConverter(string game, string group)
        {
            if (!hub.ContainsKey(game))
            {
                console.log("There are no converters for game: {0}", game);
                return new DefaultRecordConverter();
            }

            if (!hub[game].ContainsKey(group))
            {
                console.log("There are no converters for group: {0}.{1}", game, group);
                return new DefaultRecordConverter();
            }

            return hub[game][group];
        }

        public BaseRecord Convert(BaseRecord record, string game)
        {
            return getConverter(game, record.Signature).Convert(record);
        }
    }
}