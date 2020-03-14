using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Regression.Domain.Interfaces;
using Regression.Domain.Models;

namespace Regression.DB.FileSystem.Repositories
{
    public class DatasetRepository : IDatasetRepository
    {
        private char _inputSeparationSymbol = ',';
        private char _outputSeparationSymbol = ';';
        private string _path;

        public DatasetRepository UseInputSeparationSymbol(char symbol)
        {
            _inputSeparationSymbol = symbol;
            return this;
        }
        
        public DatasetRepository UseOutputSeparationSymbol(char symbol)
        {
            _outputSeparationSymbol = symbol;
            return this;
        }
        
        public DatasetRepository UseStoragePath(string path)
        {
            _path = path;
            return this;
        }
        
        public async Task<int> CountDatasets()
        {
            return await Task.Run(() =>
            {
                return Directory.Exists(_path) ? 
                    Directory
                    .EnumerateFiles(_path)
                    .Select(Path.GetFileName)
                    .Count(file => Regex.IsMatch(file, @"\.csv$")) : 0;
            });
        }

        public async Task<IEnumerable<Dataset>> FetchDatasetsAsync(int limit, int offset)
        {
            if (Directory.Exists(_path))
            {
                var ids = Directory.EnumerateFiles(_path).Select(Path.GetFileName)
                    .Where(file => Regex.IsMatch(file, @"\.csv$"))
                    .Select(file => Guid.Parse(file.Replace(".csv", "")))
                    .Skip(offset)
                    .Take(limit);

                var datasetsRetrievingTasks = new List<Task<Dataset>>();
                
                foreach (var id in ids)
                    datasetsRetrievingTasks.Add(GetDatasetByIdAsync(id));

                await Task.WhenAll(datasetsRetrievingTasks);

                return datasetsRetrievingTasks.Select(task => task.Result);
            }
            else
                return await Task.Run(Enumerable.Empty<Dataset>);
        }

        public async Task<Dataset> GetDatasetByIdAsync(Guid id)
        {
            var dataset = new Dataset { Id = id };
            var datasetFilePath = GetFilePathOfDataset(dataset);
            
            if (!File.Exists(datasetFilePath))
                throw new ArgumentException($"Dataset with ID {dataset.Id} doesn't exist!");

            await using var fs = new FileStream(GetFilePathOfDataset(dataset), FileMode.Open);
            var content = new byte[fs.Length];
            await fs.ReadAsync(content, 0, (int)fs.Length);

            var rows = Encoding.ASCII.GetString(content).Split("\n");
            dataset.Name = rows.Take(1).FirstOrDefault();
            dataset.Records = new Collection<Record>();

            foreach (var row in rows.Skip(1))
            {
                dataset.Records.Add(new Record
                {
                    Inputs = row.Split(_outputSeparationSymbol)
                        .First().Split(_inputSeparationSymbol).Select(Convert.ToDouble).ToArray(),
                    Output = row.Split(_outputSeparationSymbol)
                        .Select(Convert.ToDouble).Last()
                });
            }
            
            return dataset;
        }

        public async Task StoreDatasetAsync(Dataset dataset)
        {
            var datasetFilePath = GetFilePathOfDataset(dataset);
            
            if (File.Exists(datasetFilePath))
                throw new ArgumentException($"Dataset with ID {dataset.Id} already exists!");
            
            await WriteDatasetIntoFileAsync(dataset, FileMode.Create);
        }

        public async Task UpdateDatasetAsync(Dataset dataset)
        {
            var datasetFilePath = GetFilePathOfDataset(dataset);
            
            if (!File.Exists(datasetFilePath))
                throw new ArgumentException($"Dataset with ID {dataset.Id} doesn't exist!");
            
            await WriteDatasetIntoFileAsync(dataset, FileMode.Truncate);
        }

        public async Task DropDatasetAsync(Dataset dataset)
        {
            var datasetToDrop = new FileInfo(GetFilePathOfDataset(dataset));
            if (datasetToDrop.Exists)
                await Task.Run(() => datasetToDrop.Delete());
        }

        private async Task WriteDatasetIntoFileAsync(Dataset dataset, FileMode mode)
        {
            var recordsToWrite = dataset.Records;
            await using var fs = new FileStream(GetFilePathOfDataset(dataset), mode);
            await WriteRowToFileAsync(fs, dataset.Name);
            
            foreach (var record in recordsToWrite.SkipLast(1))
                await WriteRowToFileAsync(fs, BuildRow(record));

            await WriteRowToFileAsync(fs, BuildRow(recordsToWrite.TakeLast(1).First()), "");
        }

        private string BuildRow(Record record)
        {
            var rowBuilder = new StringBuilder();
            rowBuilder.Append(string.Join(_inputSeparationSymbol, record.Inputs));
            rowBuilder.Append(_outputSeparationSymbol);
            rowBuilder.Append(record.Output);

            return rowBuilder.ToString();
        }

        private async Task WriteRowToFileAsync(FileStream fs, string row, string endSymbol = "\n")
        {
            byte[] info = new UTF8Encoding(true).GetBytes(row + endSymbol);
            await fs.WriteAsync(info, 0, info.Length);
        }

        private string GetFilePathOfDataset(Dataset dataset)
        {
            var fileName = $"{dataset.Id}.csv";
            return Path.Combine(_path, fileName);
        }
    }
}