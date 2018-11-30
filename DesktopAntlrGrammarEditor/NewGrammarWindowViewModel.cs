﻿using AntlrGrammarEditor;
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace DesktopAntlrGrammarEditor
{
    public class NewGrammarWindowViewModel : ReactiveObject
    {
        private Window _window;
        private Grammar _grammar = GrammarFactory.CreateDefault();
        private string _grammarDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DAGE Grammars");

        public NewGrammarWindowViewModel(Window window)
        {
            _window = window;

            OkCommand = ReactiveCommand.Create(async () =>
            {
                string grammarFileName = Path.Combine(GrammarDirectory, _grammar.Name);
                bool success = false;
                if (Directory.Exists(grammarFileName))
                {
                    if (await MessageBox.ShowDialog($"Do you want to replace existed grammar {_grammar.Name}?", "", MessageBoxType.YesNo))
                    {
                        success = true;
                    }
                }
                else
                {
                    success = true;
                }

                if (success)
                {
                    GrammarFactory.FillGrammarFiles(_grammar, grammarFileName, true);
                    _window.Close(_grammar);
                }
            });

            CancelCommand = ReactiveCommand.Create(() =>
            {
                _window.Close(null);
            });

            SelectGrammarDirectory = ReactiveCommand.Create(async () =>
            {
                var openFolderDialog = new OpenFolderDialog
                {
                    InitialDirectory = GrammarDirectory
                };
                var folderName = await openFolderDialog.ShowAsync(_window);
                if (!string.IsNullOrEmpty(folderName))
                {
                    GrammarDirectory = folderName;
                }
            });
        }

        public ReactiveCommand OkCommand { get; }

        public ReactiveCommand CancelCommand { get; }

        public ReactiveCommand SelectGrammarDirectory { get; }

        public string GrammarName
        {
            get => _grammar.Name;
            set
            {
                if (_grammar.Name != value)
                {
                    _grammar.Name = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string GrammarDirectory
        {
            get => _grammarDirectory;
            set
            {
                this.RaiseAndSetIfChanged(ref _grammarDirectory, value);
            }
        }

        public string GrammarRoot
        {
            get => _grammar.Root;
            set
            {
                if (_grammar.Root != value)
                {
                    _grammar.Root = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string FileExtension
        {
            get => _grammar.FileExtension;
            set
            {
                if (_grammar.FileExtension != value)
                {
                    _grammar.FileExtension = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public RuntimeInfo Runtime
        {
            get => RuntimeInfo.InitOrGetRuntimeInfo(_grammar.Runtimes.First());
            set
            {
                if (RuntimeInfo.InitOrGetRuntimeInfo(_grammar.Runtimes.First()) != value)
                {
                    _grammar.Runtimes.Clear();
                    _grammar.Runtimes.Add(value.Runtime);
                    this.RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<RuntimeInfo> Runtimes { get; } = new ObservableCollection<RuntimeInfo>(RuntimeInfo.Runtimes.Select(r => r.Value).ToList());

        public bool SeparatedLexerAndParser
        {
            get => _grammar.SeparatedLexerAndParser;
            set
            {
                if (_grammar.SeparatedLexerAndParser != value)
                {
                    _grammar.SeparatedLexerAndParser = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public CaseInsensitiveType CaseInsensitiveType
        {
            get => _grammar.CaseInsensitiveType;
            set
            {
                if (_grammar.CaseInsensitiveType != value)
                {
                    _grammar.CaseInsensitiveType = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<CaseInsensitiveType> CaseInsensitiveTypes { get; } =
            new ObservableCollection<CaseInsensitiveType>(new List<CaseInsensitiveType>
        {
            CaseInsensitiveType.None,
            CaseInsensitiveType.lower,
            CaseInsensitiveType.UPPER,
        });

        public bool Preprocessor
        {
            get => _grammar.Preprocessor;
            set
            {
                if (_grammar.Preprocessor != value)
                {
                    _grammar.Preprocessor = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public string PreprocessorGrammarRoot
        {
            get => _grammar.PreprocessorRoot;
            set
            {
                if (_grammar.PreprocessorRoot != value)
                {
                    _grammar.PreprocessorRoot = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public bool PreprocessorSeparatedLexerAndParser
        {
            get => _grammar.PreprocessorSeparatedLexerAndParser;
            set
            {
                if (_grammar.PreprocessorSeparatedLexerAndParser != value)
                {
                    _grammar.PreprocessorSeparatedLexerAndParser = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public bool PreprocessorCaseInsensitive
        {
            get => _grammar.PreprocessorCaseInsensitive;
            set
            {
                if (_grammar.PreprocessorCaseInsensitive != value)
                {
                    _grammar.PreprocessorCaseInsensitive = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}
