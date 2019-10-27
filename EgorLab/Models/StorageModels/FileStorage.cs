using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EgorLab.Models.StorageModels
{
    public class FileStorage : MemStorage, IStorage<Person>
    {
        private Timer _timer;
        public string FileName { get; }
        public int FlushPeriod { get; }

        public FileStorage(string fileName, int flushPeriod)
        {
            FileName = fileName;
            FlushPeriod = flushPeriod;

            Load();

            _timer = new Timer((x) => Flush(), null, flushPeriod, flushPeriod);
        }

        private void Load()
        {
            if (File.Exists(FileName))
            {
                var allLines = File.ReadAllText(FileName);

                try
                {
                    var deserialized = JsonConvert.DeserializeObject<List<Person>>(allLines);

                    if (deserialized != null)
                    {
                        foreach (var labData in deserialized)
                        {
                            try
                            {
                                base[labData.Id] = labData;
                            }
                            catch { }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new FileLoadException($"Cannot load data from file {FileName}:\r\n{ex.Message}");
                }
            }
        }

        private void Flush()
        {
            var serializedContents = JsonConvert.SerializeObject(All);

            File.WriteAllText(FileName, serializedContents);
        }

    }
}
