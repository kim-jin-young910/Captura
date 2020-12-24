using System.Collections.Generic;
using System.IO;
using Captura.Loc;
using Captura.Models;

namespace Captura.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileNameFormatViewModel : ViewModelBase
    {
        public FileNameFormatViewModel(Settings Settings, ILocalizationProvider Loc)
            : base(Settings, Loc) { }

        public IEnumerable<FileNameFormatGroup> FormatGroups { get; } = new []
        {
            new FileNameFormatGroup("barcode", new []
            {
                new FileNameFormatItem("%cc%", "number"),                
            }),
            new FileNameFormatGroup("Year", new []
            {
                new FileNameFormatItem("%yyyy%", "Year (2018)"),
                new FileNameFormatItem("%yy%", "Year (18)")
            }),
            new FileNameFormatGroup("Month", new []
            {
                new FileNameFormatItem("%MMMM%", "Month (September)"),
                new FileNameFormatItem("%MMM%", "Month (Sep)"),
                new FileNameFormatItem("%MM%", "Month (09)")
            }),
            new FileNameFormatGroup("Date", new []
            {
                new FileNameFormatItem("%dd%", "Date (22)"),
                new FileNameFormatItem("%ddd%", "Day (Tue)"),
                new FileNameFormatItem("%dddd%", "Day (Tuesday)")
            }),
            new FileNameFormatGroup("Other", new []
            {
                new FileNameFormatItem("%computer%", "Company Name"), 
                new FileNameFormatItem("%user%", "User Name")
            }), 
        };

        public string FilenameFormat
        {
            get => Settings.FilenameFormat;
            set
            {
                var invalidChars = Path.GetInvalidFileNameChars();

                foreach (var invalidChar in invalidChars)
                {
                    if (invalidChar == '/' || invalidChar == '\\')
                        continue;

                    value = value.Replace(invalidChar.ToString(), "");
                }

                Settings.FilenameFormat = value;

                OnPropertyChanged();

                RaisePropertyChanged(nameof(FilenamePreview));
            }
        }

        public string FilenamePreview => Settings.GetFileName(".mp4");
    }
}
